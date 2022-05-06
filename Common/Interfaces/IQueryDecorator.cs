using MediatR.Pipeline;

namespace Common.Interfaces
{
	public interface IQueryDecorator<TModel, TResponse> : IRequestPreProcessor<TModel>
	where TModel : IQuery<TResponse>
	{
	}
}
