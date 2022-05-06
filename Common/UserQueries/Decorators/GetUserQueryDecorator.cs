using Common.Interfaces;
using Common.ViewModels;
using FluentValidation;

namespace Common.UserQueries.Decorators
{
	public class GetUserQueryDecorator : AbstractValidator<GetUserQuery>, IQueryDecorator<GetUserQuery, UserModel>
	{
		public GetUserQueryDecorator()
		{
			RuleFor(query => query.Id).GreaterThan(0);
		}

		public Task Process(GetUserQuery request, CancellationToken cancellationToken)
		{
			this.ValidateAndThrow(request);
			return Task.CompletedTask;
		}
	}
}
