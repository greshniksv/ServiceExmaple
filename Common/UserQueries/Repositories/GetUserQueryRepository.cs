using AutoMapper;
using Common.Interfaces;
using Common.ViewModels;
using Database.DbModels;
using Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Common.UserQueries.Repositories
{
	public class GetUserQueryRepository : IRepository<GetUserQuery, UserModel>
	{
		private readonly IMainContext context;
		private readonly IMapper mapper;

		public GetUserQueryRepository(IMainContext context, IMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
		}

		public async Task<UserModel> ExecuteAsync(GetUserQuery parameter)
		{
			User user = await context.Users.FirstOrDefaultAsync(x => x.Id == parameter.Id);
			if (user == null)
			{
				throw new Exception($"User {parameter.Id.ToString()} not found");
			}

			return mapper.Map<User, UserModel>(user);
		}
	}
}
