using Common.Factories.Interfaces;
using Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Factories
{
	public class RepositoryFactory : IRepositoryFactory
	{
		private readonly IServiceProvider serviceProvider;

		public RepositoryFactory(IServiceProvider serviceProvider)
		{
			this.serviceProvider = serviceProvider;
		}

		public IRepository<TModel, TResponse> GetRepository<TModel, TResponse>()
			where TModel : class
		{
			List<Type>? allTypesOfIRepository =
				(from x in AppDomain.CurrentDomain.GetAssemblies()
					.SelectMany(s => s.GetTypes())
				 where !x.IsAbstract && !x.IsInterface &&
					   x.GetInterfaces().FirstOrDefault() == typeof(IRepository<TModel, TResponse>)
				 select x).ToList();

			if (allTypesOfIRepository.Count > 1)
			{
				throw new Exception("Too many repositories");
			}

			if (allTypesOfIRepository == null || allTypesOfIRepository.Count == 0)
			{
				throw new Exception("Not found repository");
			}

			return (IRepository<TModel, TResponse>)ActivatorUtilities.CreateInstance(serviceProvider, allTypesOfIRepository[0]);
		}
	}
}
