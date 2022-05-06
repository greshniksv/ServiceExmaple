using Common.Interfaces;
using FluentValidation;

namespace Common.UserCommands.Decorators
{
	public class CreateUserDecorator : AbstractValidator<CreateUserCommand>, ICommandDecorator<CreateUserCommand, int>
	{
		public CreateUserDecorator()
		{
			RuleFor(x => x.Name).NotNull();
			RuleFor(x => x.Name).MaximumLength(50);

			/* And other */
		}

		public Task Process(CreateUserCommand request, CancellationToken cancellationToken)
		{
			this.ValidateAndThrow(request);
			return Task.CompletedTask;
		}
	}
}