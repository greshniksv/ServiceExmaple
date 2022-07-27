using AutoMapper;
using BLL.Exceptions.GetUserQuery;
using BLL.Interfaces;
using BLL.ViewModels;
using DAL.DbModels;
using DAL.Exceptions;
using DAL.Interfaces;

namespace BLL.UserQueries.Handlers
{
	public class GetUserQueryHandler : IQueryHandler<GetUserQuery, UserModel?>
	{
		private readonly IRepository<int, User?> repository;
		private readonly IMapper mapper;

		public GetUserQueryHandler(IRepository<int, User?> repository, IMapper mapper)
		{
			this.mapper = mapper;
			this.repository = repository;
		}

		public async Task<UserModel?> Handle(GetUserQuery request, CancellationToken cancellationToken)
		{
			try
			{
				User? result = await repository.ExecuteAsync(request.UserId);

				if (result == null)
				{
					return null;
				}

				return mapper.Map<User, UserModel>(result);
			}
			catch (Exception e) when (!(e is RepositoryException))
			{
				throw new GetUserQueryException($"Get user by id: {request.UserId.ToString()}", e);
			}
		}
	}
}
