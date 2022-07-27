using BLL.Interfaces;
using BLL.Options;
using Microsoft.Extensions.Options;
using Serilog;

namespace BLL.UserNotifications.Handlers
{
    public class SendMailNotifyHandler : INotifyHandler<SendMailNotify>
    {
		private ILogger logger { get; set; }
		private readonly IOptions<EmailOptions> emailOptions;

		public SendMailNotifyHandler(ILogger logger, IOptions<EmailOptions> emailOptions)
		{
			this.logger = logger;
			this.emailOptions = emailOptions;
		}

		public Task<bool> Handle(SendMailNotify notification, CancellationToken cancellationToken)
	    {
		    logger.Information("Send mail to: {@0}", notification);

			logger.Debug("Email settings: {@0}", emailOptions.Value);

			// Sending mail
			return Task.FromResult(true);
	    }
	}
}
