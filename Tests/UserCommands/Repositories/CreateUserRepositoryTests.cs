using System.Collections.Generic;
using System.Threading;
using AutoFixture.Xunit2;
using DAL.DbModels;
using DAL.Interfaces;
using DAL.Repositories;
using DAL.ResultModels.Enums;
using FluentAssertions;
using Moq;
using TestProject.Tools;
using TestProject.Tools.Attributes;
using Xunit;

namespace TestProject.UserCommands.Repositories
{
	public class CreateUserRepositoryTests
	{
		//[Theory, AutoMoqData]
		//public async void ExecuteAsync_WhenCommandNotNull_ThenReturnedStatusSuccess(
		//	[Frozen] Mock<IMainContext> mainContext,
		//	CreateUserRepository repository,
		//	List<User> users,
		//	User user)
		//{
		//	var mockDbSet = users.AsDbSetMock();
		//	mainContext.SetupGet(x => x.Users).Returns(mockDbSet.Object);

		//	var returnedId = await repository.ExecuteAsync(user);

		//	returnedId.Status.Should().Be(DatabaseStatus.Success);
		//}

		//[Theory, AutoMoqData]
		//public async void ExecuteAsync_WhenCommandNotNull_ThenReturnedUserId(
		//	[Frozen] Mock<IMainContext> mainContext,
		//	CreateUserRepository repository,
		//	List<User> users,
		//	User user)
		//{
		//	var mockDbSet = users.AsDbSetMock();
		//	mainContext.SetupGet(x => x.Users).Returns(mockDbSet.Object);

		//	var returnedId = await repository.ExecuteAsync(user);

		//	returnedId.Data.Should().Be(user.Id);
		//}

		//[Theory, AutoMoqData]
		//public async void ExecuteAsync_WhenCommandNotNull_ThenReturned(
		//	[Frozen] Mock<IMainContext> mainContext,
		//	CreateUserRepository repository,
		//	List<User> users,
		//	User user)
		//{
		//	var mockDbSet = users.AsDbSetMock();
		//	mainContext.SetupGet(x => x.Users).Returns(mockDbSet.Object);

		//	var returnedId = await repository.ExecuteAsync(user);

		//	returnedId.Error.Should().BeEmpty();
		//}

		[Theory, AutoMoqData]
		public async void ExecuteAsync_WhenWhenCommandNotNull_ThenExecutedSaveChangesAsync(
			[Frozen] Mock<IMainContext> mainContext,
			CreateUserRepository repository,
			List<User> users,
			User user)
		{
			var mockDbSet = users.AsDbSetMock();
			mainContext.SetupGet(x => x.Users).Returns(mockDbSet.Object);

			await repository.ExecuteAsync(user);

			mainContext.Verify(x => x.SaveChangesAsync(CancellationToken.None));
		}


		[Theory, AutoMoqData]
		public async void ExecuteAsync_WhenCommandNotNull_ThenExecutedAddDbModel(
			[Frozen] Mock<IMainContext> mainContext,
			CreateUserRepository repository,
			List<User> users,
			User user)
		{
			//Arrange
			var mockDbSet = users.AsDbSetMock();
			mainContext.SetupGet(x => x.Users).Returns(mockDbSet.Object);

			//Act
			await repository.ExecuteAsync(user);

			//Assert
			mockDbSet.Verify(x => x.Add(user));
		}
	}
}
