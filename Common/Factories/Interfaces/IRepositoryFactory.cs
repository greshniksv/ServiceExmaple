using Common.Interfaces;

namespace Common.Factories.Interfaces
{
	public interface IRepositoryFactory
	{
		IRepository<TRequest, TResponse> GetRepository<TRequest, TResponse>()
			where TRequest : class;
	}
}
