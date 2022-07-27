using DAL.DbModels;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.DbContexts
{
	public class MainContext : DbContext, IMainContext
	{
		public MainContext(DbContextOptions options)
			: base(options)
		{
		}

		public DbSet<User> Users { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>()
				.Property(f => f.Id)
				.ValueGeneratedOnAdd();
		}
	}
}
