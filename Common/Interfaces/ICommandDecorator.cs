using MediatR.Pipeline;

namespace Common.Interfaces
{
    public interface ICommandDecorator<TModel, TResponce> : IRequestPreProcessor<TModel>
    where TModel : ICommand<TResponce>
    {
    }
}
