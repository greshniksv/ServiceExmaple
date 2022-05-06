using Database.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Database.Interfaces
{
	public interface IMainContext: IDisposable
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
