using Common.Interfaces;
using FluentValidation;

namespace Common.UserNotifications.Validators
{
	public class SendMailNotifyValidation : AbstractValidator<SendMailNotify>, INotifyValidator<SendMailNotify>
	{
		public SendMailNotifyValidation()
		{
			RuleFor(x => x.Email).NotNull();
			RuleFor(x => x.Email).NotEmpty();
			RuleFor(x => x.Email).MaximumLength(150);
			RuleFor(x => x.Email).EmailAddress();
		}

		public async Task Process(SendMailNotify request, CancellationToken cancellationToken)
		{
			 await this.ValidateAndThrowAsync(request, cancellationToken);
		}
	}
}
