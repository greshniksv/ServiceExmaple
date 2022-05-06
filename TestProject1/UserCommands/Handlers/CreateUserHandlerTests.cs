using System.Threading;
using AutoFixture.Xunit2;
using Common.Factories.Interfaces;
using Common.Interfaces;
using Common.UserCommands;
using Common.UserCommands.Handlers;
using Moq;
using TestProject1.Attributes;
using Xunit;

namespace TestProject1.UserCommands.Handlers
{
	public class CreateUserHandlerTests
	{
		[Theory, AutoMoqData]
		public async void Handle_WhenRepositoryExist_ThenExecuteRepository(
			[Frozen] Mock<IRepositoryFactory> repositoryFactory,
			[Frozen] Mock<IRepository<CreateUserCommand, int>> repository,
			CreateUserHandler handler,
			CreateUserCommand command)
		{
			repositoryFactory
				.Setup(x => x.GetRepository<CreateUserCommand, int>())
				.Returns(repository.Object);

			await handler.Handle(command, CancellationToken.None);

			repository.Verify(x=>x.ExecuteAsync(command), Times.Once);
		}

		/* And other */
	}
}
