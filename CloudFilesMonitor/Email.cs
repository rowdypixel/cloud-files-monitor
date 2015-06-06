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
        public static void Send(string body)
        {
            var sender = System.Configuration.ConfigurationManager.AppSettings["NotificationSenderAddress"];
            var recipient = System.Configuration.ConfigurationManager.AppSettings["NotificationEmailAddress"];
            var sendgridApiUser = System.Configuration.ConfigurationManager.AppSettings["SendgridAPIUser"];
            var sendgridApiKey = System.Configuration.ConfigurationManager.AppSettings["SendgridAPIKey"];

            SendGrid.SendGridMessage message = new SendGrid.SendGridMessage();
            message.To = new System.Net.Mail.MailAddress[] {
                        new System.Net.Mail.MailAddress(recipient)
                    };
            message.From = new System.Net.Mail.MailAddress(sender);
            message.Text = body;

            var credentials = new NetworkCredential(sendgridApiUser, sendgridApiKey);
            var transport = new SendGrid.Web(credentials);
            transport.DeliverAsync(message);
        }
    }
}
