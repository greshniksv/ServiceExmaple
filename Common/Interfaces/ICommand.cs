using MediatR;

namespace Common.Interfaces
{
    public interface ICommand<T> : IRequest<T>
    {
    }
}
