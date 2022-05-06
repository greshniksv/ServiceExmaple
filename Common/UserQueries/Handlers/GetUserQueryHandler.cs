using Common.Factories.Interfaces;
using Common.Interfaces;
using Common.ViewModels;

namespace Common.UserQueries.Handlers
{
	public class GetUserQueryHandler : IQueryHandler<GetUserQuery, UserModel>
	{
		private readonly IRepositoryFactory repositoryFactory;

		public GetUserQueryHandler(IRepositoryFactory repositoryFactory)
		{
			this.repositoryFactory = repositoryFactory;
		}

		public async Task<UserModel> Handle(GetUserQuery request, CancellationToken cancellationToken)
		{
			IRepository<GetUserQuery, UserModel> repo = repositoryFactory.GetRepository<GetUserQuery, UserModel>();
			UserModel user = await repo.ExecuteAsync(request);

			return user;
		}
	}
}
