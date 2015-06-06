using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace CloudFilesMonitor
{
    class Email
    {
        public static async void Send(string body)
        {
            var sender = System.Configuration.ConfigurationManager.AppSettings["NotificationSenderAddress"];
            var recipient = System.Configuration.ConfigurationManager.AppSettings["NotificationEmailAddress"];
            var sendgridApiUser = System.Configuration.ConfigurationManager.AppSettings["SendgridAPIUser"];
            var sendgridApiKey = System.Configuration.ConfigurationManager.AppSettings["SendgridAPIKey"];

            SendGrid.SendGridMessage message = new SendGrid.SendGridMessage();
            message.AddTo(recipient);
            message.Subject = string.Format("ALERT! Site Changed");
            message.From = new System.Net.Mail.MailAddress(sender);
            message.Text = body;
            message.Html = body;

            var credentials = new NetworkCredential(sendgridApiUser, sendgridApiKey);
            var transport = new SendGrid.Web(credentials);

            try
            {
#if DEBUG
                Console.WriteLine("Emails don't send in debug mode.");
#else
                await transport.DeliverAsync(message);
#endif
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
