using DAL.DbModels;
using DAL.Exceptions.GetUserQueryRepository;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace DAL.Repositories
{
	public class GetUserQueryRepository : IRepository<int, User?>
	{
		private readonly IMainContext context;

		public GetUserQueryRepository(IMainContext context)
		{
			this.context = context;
		}

		public async Task<User?> ExecuteAsync(int parameter)
		{
			try
			{
				return await context.Users.FirstOrDefaultAsync(x => x.Id == parameter);
			}
			catch (NpgsqlException e)
			{
				throw new GetUserQueryRepositorySqlException($"Extract user by ID: {parameter.ToString()}", e);
			}
			catch (Exception e)
			{
				throw new GetUserQueryRepositoryException($"Extract user by ID: {parameter.ToString()}", e);
			}
		}
	}
}
