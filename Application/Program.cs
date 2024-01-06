using System.Text.Json.Serialization;
using Application;
using Application.Extensions;
using BLL.AutoMapperProfiles;
using BLL.Interfaces;
using BLL.Options;
using BLL.UserQueries;
using DAL;
using DAL.DbContexts;
using DAL.DbModels;
using DAL.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using IdentityServer4.AspNetIdentity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

IConfigurationRoot configuration = new ConfigurationBuilder()
	.SetBasePath(Directory.GetCurrentDirectory())
	.AddJsonFile("appsettings.json")
	.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
	.Build();

bool isProduction = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production";
Log.Logger = new LoggerConfiguration()
	.ReadFrom.Configuration(configuration)
	.MinimumLevel.Verbose()
	.CreateLogger();

Log.Logger.Debug("Start");
builder.WebHost.UseUrls(configuration["UseUrls"]);

// Add services to the container.
builder.Services.AddValidatorsFromAssembly(typeof(GetUserQuery).Assembly);

// Register service provider
builder.Services.AddDbContext<IdentityContext>(opt =>
	{
		opt.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"))
			.UseSnakeCaseNamingConvention();
	})
	.AddIdentity<ApplicationUser, ApplicationRole>(config =>
	{
		config.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789\\";
		config.Password.RequireDigit = false;
		config.Password.RequiredLength = 6;
		config.Password.RequireLowercase = false;
		config.Password.RequireUppercase = false;
		config.Password.RequireNonAlphanumeric = false;
	})
	.AddEntityFrameworkStores<IdentityContext>()
	.AddSignInManager()
	.AddDefaultTokenProviders();

builder.Services.AddDbContext<MainContext>(opt =>
{
	opt.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"))
		.UseSnakeCaseNamingConvention();
});

builder.Services.AddIdentityServer(options =>
	{
		options.Authentication.CookieAuthenticationScheme = "authCookie";
		options.IssuerUri = configuration["Jwt:Issuer"];
	})
	.AddAspNetIdentity<ApplicationUser>()
	.AddOperationalStore(options =>
	{
		options.ConfigureDbContext = b =>
			b.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"),
					sql => sql.MigrationsAssembly("DAL"))
				.UseSnakeCaseNamingConvention();

		// this enables automatic token cleanup. this is optional.
		options.EnableTokenCleanup = true;
		options.TokenCleanupInterval = 3600; // interval in seconds (default is 3600)
		options.DefaultSchema = DbSchema.IdentityServer;
	})
	.AddConfigurationStore(options =>
	{
		options.ConfigureDbContext = b =>
			b.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"),
					sql => sql.MigrationsAssembly("DAL"))
				.UseSnakeCaseNamingConvention();
		options.DefaultSchema = DbSchema.IdentityServer;
	})
	.AddJwtBearerClientAuthentication()
	.AddProfileService<ProfileService<ApplicationUser>>()
	.AddDeveloperSigningCredential()
	.AddInMemoryApiScopes(Config.ApiScopes)
	.AddInMemoryClients(Config.GetClients());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR((config) =>
{
	config.RegisterServicesFromAssembly(typeof(GetUserQuery).Assembly);
});
//builder.Services.AddDbContext<MainContext>(opt => opt.UseInMemoryDatabase("TestDb"));
builder.Services.AddAutoMapper(typeof(DbToViewModelProfile), typeof(CommandToDbModelProfile));
builder.Services.AddRepositories();

// Register logger
builder.Services.AddSingleton<Serilog.ILogger>(Log.Logger);

// Register DB context
builder.Services.AddScoped<IMainContext, MainContext>();
builder.Services.AddScoped<IIdentityContext, IdentityContext>();

// Register service provider
//IServiceProvider serviceProvider = builder.Services.BuildServiceProvider();
//builder.Services.AddSingleton<IServiceProvider>(serviceProvider);

// Register settings
builder.Services.Configure<EmailOptions>(builder.Configuration.GetSection(EmailOptions.Name));

// Modules
// Add services to the container.
builder.Services.AddControllers(options =>
{
	//options.Filters.Add<ActionFilter>();
	//options.Filters.Add<ElsaProcessChecker>();
	//options.Filters.Add<FixDateActionFilter>();
	//options.Conventions.Add(new NamespaceRoutingConvention());
}).AddJsonOptions(x =>
	{
		x.JsonSerializerOptions.WriteIndented = !isProduction;
		x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
	}
); //.AddNewtonsoftJson(x => x.SerializerSettings.Converters.Add(new StringEnumConverter()));

builder.Services.AddSwaggerGen(options =>
{
	options.AddSecurityDefinition("oauth2",
		new OpenApiSecurityScheme {
			Type = SecuritySchemeType.OAuth2,
			Flows = new OpenApiOAuthFlows {
				Password = new OpenApiOAuthFlow {
					TokenUrl = new Uri($"{builder.Configuration["BaseApplicationUrl"]}/connect/token"),
				}
			}
		});
});

builder.Services.AddAuthentication(options =>
	{
		options.DefaultAuthenticateScheme = "Bearer";
		options.DefaultChallengeScheme = "Bearer";
		options.DefaultScheme = "Bearer";
	})
	.AddJwtBearer(o =>
	{
		o.Authority = builder.Configuration["BaseApplicationUrl"];
		o.RequireHttpsMetadata = Convert.ToBoolean(configuration["Jwt:RequireHttpsMetadata"]);
		o.TokenValidationParameters = new TokenValidationParameters {
			ValidIssuer = builder.Configuration["Jwt:Issuer"],
			ValidAudience = builder.Configuration["Jwt:Audience"],
			//IssuerSigningKey = builder.Configuration.GetSecurityKey(Log.Logger),
			ValidateIssuer = false,
			ValidateAudience = false,
			ValidateLifetime = false,
			ValidateIssuerSigningKey = true
		};
		o.BackchannelHttpHandler = new HttpClientHandler {
			ServerCertificateCustomValidationCallback =
				HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
		};
	})
	.AddCookie("authCookie");
builder.Services.AddAuthorization();

WebApplication app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.ConfigureExceptionHandler(Log.Logger);

app.UseAuthentication();
app.UseIdentityServer();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
