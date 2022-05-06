using MediatR;

namespace Common.Interfaces
{
	public interface IRepository<TModel, TResponce>
		//where TModel : IBaseRequest
	{
		public Task<TResponce> ExecuteAsync(TModel parameter);
	}
}
