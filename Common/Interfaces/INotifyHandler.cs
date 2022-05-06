using MediatR;

namespace Common.Interfaces
{
	public interface INotifyHandler<TNotify> : IRequestHandler<TNotify, bool>
		where TNotify: INotify
	{
	}
}
