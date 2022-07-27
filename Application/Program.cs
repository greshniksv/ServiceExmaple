using Application.Extensions;
using BLL.AutoMapperProfiles;
using BLL.Interfaces;
using BLL.Options;
using BLL.UserQueries;
using DAL.DbContexts;
using DAL.Interfaces;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

IConfigurationRoot configuration = new ConfigurationBuilder()
	.SetBasePath(Directory.GetCurrentDirectory())
	.AddJsonFile("appsettings.json")
	.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
	.Build();

Log.Logger = new LoggerConfiguration()
	.ReadFrom.Configuration(configuration)
	.CreateLogger();

builder.WebHost.UseUrls(configuration["UseUrls"]);

// Add services to the container.
builder.Services.AddControllers().AddFluentValidation(
	options =>
	{
		options.AutomaticValidationEnabled = false;
		options.RegisterValidatorsFromAssembly(typeof(ICommand<>).Assembly);
	});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(GetUserQuery).Assembly);
builder.Services.AddDbContext<MainContext>(opt => opt.UseInMemoryDatabase("TestDb"));
builder.Services.AddAutoMapper(typeof(DbToViewModelProfile), typeof(CommandToDbModelProfile));
builder.Services.AddRepositories();

// Register logger
builder.Services.AddSingleton<Serilog.ILogger>(Log.Logger);

// Register DB context
builder.Services.AddScoped<IMainContext, MainContext>();

// Register service provider
IServiceProvider serviceProvider = builder.Services.BuildServiceProvider();
builder.Services.AddSingleton<IServiceProvider>(serviceProvider);

// Register settings
builder.Services.Configure<EmailOptions>(builder.Configuration.GetSection(EmailOptions.Name));

WebApplication app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.ConfigureExceptionHandler(Log.Logger);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
