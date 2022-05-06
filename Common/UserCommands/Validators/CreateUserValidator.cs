using Common.Interfaces;
using FluentValidation;

namespace Common.UserCommands.Validators
{
	public class CreateUserValidator : AbstractValidator<CreateUserCommand>, ICommandValidator<CreateUserCommand, int>
	{
		public CreateUserValidator()
		{
			RuleFor(x => x.Name).NotNull();
			RuleFor(x => x.Name).MaximumLength(50);

			/* And other */
		}

		public async Task Process(CreateUserCommand request, CancellationToken cancellationToken)
		{
			await this.ValidateAndThrowAsync(request, cancellationToken);
		}
	}
}