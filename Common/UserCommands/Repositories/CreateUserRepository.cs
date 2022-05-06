using AutoMapper;
using Common.Interfaces;
using Database.DbContexts;
using Database.DbModels;
using Database.Interfaces;

namespace Common.UserCommands.Repositories
{
	public class CreateUserRepository : IRepository<CreateUserCommand, int>
	{
		private readonly IMainContext context;
		private readonly IMapper mapper;

		public CreateUserRepository(MainContext context, IMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
		}

		public async Task<int> ExecuteAsync(CreateUserCommand parameter)
		{
			User user = mapper.Map<CreateUserCommand, User>(parameter);

			context.Users.Add(user);
			await context.SaveChangesAsync();

			return user.Id;
		}
	}
}
