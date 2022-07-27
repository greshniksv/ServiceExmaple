using BLL.Interfaces;
using BLL.ViewModels;
using FluentValidation;

namespace BLL.UserQueries.Validators
{
	public class GetUserQueryValidator : AbstractValidator<GetUserQuery>, IQueryValidator<GetUserQuery, UserModel?>
	{
		public GetUserQueryValidator()
		{
			RuleFor(query => query.UserId).GreaterThan(0);
		}

		public async Task Process(GetUserQuery request, CancellationToken cancellationToken)
		{
			await this.ValidateAndThrowAsync(request, cancellationToken);
		}
	}
}
