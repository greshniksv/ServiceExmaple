using Common.Interfaces;

namespace Common.UserNotifications
{
    public class SendMailNotify : INotify
    {
        public string Email { get; set; }
    }
}
