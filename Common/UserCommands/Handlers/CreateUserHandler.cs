using Common.Factories.Interfaces;
using Common.Interfaces;

namespace Common.UserCommands.Handlers
{
    public class CreateUserHandler : ICommandHandler<CreateUserCommand, int>
    {
	    private readonly IRepositoryFactory repositoryFactory;

	    public CreateUserHandler(IRepositoryFactory repositoryFactory)
	    {
		    this.repositoryFactory = repositoryFactory;
	    }

	    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
	    {
		    IRepository<CreateUserCommand, int> repo =
			    repositoryFactory.GetRepository<CreateUserCommand, int>();

			return await repo.ExecuteAsync(request);
		}
    }
}
