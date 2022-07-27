using DAL.DbModels;
using DAL.Exceptions.CreateUserRepository;
using DAL.Interfaces;
using Npgsql;

namespace DAL.Repositories
{
	public class CreateUserRepository : IRepository<User, int>
	{
		private readonly IMainContext context;

		public CreateUserRepository(IMainContext context)
		{
			this.context = context;
		}

		public async Task<int> ExecuteAsync(User user)
		{
			try
			{
				context.Users.Add(user);
				await context.SaveChangesAsync();
			}
			catch (NpgsqlException ex)
			{
				throw new CreateUserRepositorySqlException("Add new user", ex);
			}
			catch (Exception e)
			{
				throw new CreateUserRepositoryException("Add new user", e);
			}

			return user.Id;
		}
	}
}
