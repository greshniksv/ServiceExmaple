using Common.Interfaces;
using Common.ViewModels;
using FluentValidation;

namespace Common.UserQueries.Validators
{
	public class GetUserQueryValidator : AbstractValidator<GetUserQuery>, IQueryValidator<GetUserQuery, UserModel>
	{
		public GetUserQueryValidator()
		{
			RuleFor(query => query.Id).GreaterThan(0);
		}

		public async Task Process(GetUserQuery request, CancellationToken cancellationToken)
		{
			await this.ValidateAndThrowAsync(request, cancellationToken);
		}
	}
}
