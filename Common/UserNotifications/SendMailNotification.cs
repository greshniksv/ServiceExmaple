using MediatR;

namespace Common.UserNotifications
{
    public class SendMailNotification : INotification
    {
        public string Email { get; set; }
    }
}
