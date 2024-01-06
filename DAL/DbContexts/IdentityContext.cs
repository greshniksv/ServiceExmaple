using DAL.DbModels;
using DAL.Interfaces;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;

namespace DAL.DbContexts
{
	public class IdentityContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>,
		IPersistedGrantDbContext, IConfigurationDbContext, IIdentityContext
	{
		public IdentityContext(DbContextOptions<IdentityContext> options)
			: base(options)
		{
		}

		public async Task<int> SaveChangesAsync()
		{
			return await base.SaveChangesAsync(CancellationToken.None);
		}

		public DbSet<PersistedGrant> PersistedGrants { get; set; }
		public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }
		public DbSet<Client> Clients { get; set; }
		public DbSet<ClientCorsOrigin> ClientCorsOrigins { get; set; }
		public DbSet<IdentityResource> IdentityResources { get; set; }
		public DbSet<ApiResource> ApiResources { get; set; }
		public DbSet<ApiScope> ApiScopes { get; set; }

		override protected void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder
				.Entity<DeviceFlowCodes>(typeBuilder =>
				{
					typeBuilder.HasNoKey();
				});
			builder
				.Entity<PersistedGrant>(typeBuilder =>
				{
					typeBuilder.HasNoKey();
				});
		}

		public DbContext GetDbContext()
		{
			return this;
		}

		public DatabaseFacade GetDatabase()
		{
			return Database;
		}

		public IModel GetModel()
		{
			return Model;
		}

		public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
		{
			return GetDatabase().BeginTransactionAsync(cancellationToken);
		}
	}
}
