using AutoMapper;
using BLL.Exceptions.CreateUser;
using BLL.Interfaces;
using DAL.DbModels;
using DAL.Exceptions;
using DAL.Interfaces;

namespace BLL.UserCommands.Handlers
{
    public class CreateUserHandler : ICommandHandler<CreateUserCommand, int>
    {
	    private readonly IRepository<User, int> repository;
	    private readonly IMapper mapper;

	    public CreateUserHandler(IRepository<User, int> repository, IMapper mapper)
	    {
		    this.repository = repository;
			this.mapper = mapper;
	    }

	    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
	    {
		    try
		    {
			    User user = mapper.Map<CreateUserCommand, User>(request);
			    return await repository.ExecuteAsync(user);
			}
		    catch (Exception e) when (!(e is RepositoryException))
			{
			    throw new CreateUserException("Create user", e);
		    }
		}
    }
}
