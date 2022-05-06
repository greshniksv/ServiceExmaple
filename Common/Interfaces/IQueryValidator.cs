using MediatR.Pipeline;

namespace Common.Interfaces
{
	public interface IQueryValidator<TModel, TResponse> : IRequestPreProcessor<TModel>
	where TModel : IQuery<TResponse>
	{
	}
}
