using System;
using System.Threading;
using System.Threading.Tasks;
using Common.UserCommands;
using Common.UserCommands.Validators;
using FluentAssertions;
using FluentValidation;
using TestProject1.Attributes;
using Xunit;

namespace TestProject1.UserCommands.Validators
{
	public class CreateUserValidatorTests
	{
		[Theory, AutoMoqData]
		public async void Process_WhenNameIsNull_ThenThrowsValidationException(
			CreateUserValidator validator,
			CreateUserCommand command)
		{
			command.Name = null;

			Func<Task> tt = async () => await validator.Process(command, CancellationToken.None);

			await tt.Should().ThrowAsync<ValidationException>();
		}

		/*And other */
	}
}
