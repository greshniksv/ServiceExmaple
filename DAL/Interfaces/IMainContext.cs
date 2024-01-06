using DAL.DbModels;
using Microsoft.EntityFrameworkCore;

namespace DAL.Interfaces
{
	public interface IMainContext: IDbContext, IDisposable
	{
		DbSet<User> Users { get; set; }

		int SaveChanges();

		int SaveChanges(bool acceptAllChangesOnSuccess);

		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

		Task<int> SaveChangesAsync(
			bool acceptAllChangesOnSuccess,
			CancellationToken cancellationToken = default);
	}
}
