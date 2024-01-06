using DAL.DbModels;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;

namespace DAL.DbContexts
{
	public class MainContext : DbContext, IMainContext
	{
		//public MainContext(DbContextOptions options)
		//	: base(options)
		//{
		//}

		public MainContext(DbContextOptions<MainContext> options)
			: base(options)
		{
		}

		public DbSet<User> Users { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<User>()
				.Property(f => f.Id)
				.ValueGeneratedOnAdd();
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
