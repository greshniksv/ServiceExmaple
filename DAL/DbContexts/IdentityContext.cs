using DAL.DbModels;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.DbContexts
{
	public class IdentityContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>,
		IPersistedGrantDbContext, IConfigurationDbContext
	{
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
	}
}
