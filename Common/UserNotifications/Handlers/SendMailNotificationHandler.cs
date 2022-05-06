using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Common.UserNotifications.Handlers
{
    public class SendMailNotificationHandler : INotificationHandler<SendMailNotification>
    {
        public Task Handle(SendMailNotification notification, CancellationToken cancellationToken)
        {
            // Sending mail
            return Task.CompletedTask;
        }
    }
}
