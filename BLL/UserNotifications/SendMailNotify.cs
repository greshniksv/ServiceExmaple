using BLL.Interfaces;

namespace BLL.UserNotifications
{
    public class SendMailNotify : INotify
    {
	    public SendMailNotify(string email)
	    {
		    Email = email;
	    }

	    public string Email { get; set; }
    }
}
