using MediatR.Pipeline;

namespace Common.Interfaces
{
    public interface ICommandValidator<TModel, TResponce> : IRequestPreProcessor<TModel>
    where TModel : ICommand<TResponce>
    {
    }
}
