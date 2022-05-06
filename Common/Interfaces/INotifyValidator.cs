using MediatR.Pipeline;

namespace Common.Interfaces
{
	public interface INotifyValidator<TNotify> : IRequestPreProcessor<TNotify>
		where TNotify : INotify
	{
	}
}
