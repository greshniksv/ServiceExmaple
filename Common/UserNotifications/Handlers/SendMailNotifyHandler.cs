using Common.Interfaces;
using MediatR;

namespace Common.UserNotifications.Handlers
{
    public class SendMailNotifyHandler : INotifyHandler<SendMailNotify>
    {
        public Task<bool> Handle(SendMailNotify notification, CancellationToken cancellationToken)
        {
            // Sending mail
            return Task.FromResult(true);
        }
    }
}
