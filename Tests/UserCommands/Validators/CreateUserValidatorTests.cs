using BLL.UserCommands;
using BLL.UserCommands.Validators;
using FluentAssertions;
using TestProject.Tools.Attributes;
using Xunit;

namespace TestProject.UserCommands.Validators
{
	public class CreateUserValidatorTests
	{
		[Theory, AutoMoqData]
		public async void Process_WhenNameIsNull_ThenThrowsValidationException(
			CreateUserValidator validator,
			CreateUserCommand command)
		{
			command.Name = null;

			var result = await validator.ValidateAsync(command);

			result.IsValid.Should().BeFalse();
		}

		/*And other */
	}
}
