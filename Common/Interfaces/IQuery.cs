using MediatR;

namespace Common.Interfaces
{
    public interface IQuery<T> : IRequest<T>
    {
    }
}
