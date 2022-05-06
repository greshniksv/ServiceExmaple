using Database.DbModels;
using Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Database.DbContexts
{
	public class MainContext : DbContext, IMainContext
	{
		public MainContext(DbContextOptions options)
			: base(options)
		{
		}

		public DbSet<User> Users { get; set; }
	}
}
