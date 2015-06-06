using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace CloudFilesMonitor
{
    class Program
    {
        static void Main(string[] args)
        {

            // Initialize the database.
            var databasePath = System.Configuration.ConfigurationManager.AppSettings["FileTrackerDatabasePath"];
            Database.Disk.OpenFile(databasePath);


            // Initialize sites.
            var sites = new Site[] 
            {
                new Site() 
                {
                    ContainerName = "SampleContainerName", 
                    Name="SampleSite", 
                    Provider = new CloudProviders.RackspaceCloudProvider() 
                    {
                        AuthDetails = new Dictionary<string,string>() 
                        {
                            { "APIKey", "rackspace_api_key"},
                            { "Username", "rackspace_username"}

                        }
                    }
                }
            };
                        
            //Set up the timer.
            var timerCheckInterval = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["TimerCheckIntervalSeconds"]) * 1000;
            Timer t = new Timer(Timer_Tick, sites, 0, timerCheckInterval);

            Console.ReadLine();

        }

        private static void Timer_Tick(Object o)
        {
            var sites = o as Site[];
            foreach (var site in sites)
            {
                var changes = site.Compare();
                Console.WriteLine("{0}: Changes on site: {1}: {2}", DateTime.Now, site.Name, changes.Count());

                if (changes.Count() > 0)
                {

                    string body = string.Format(@"Warning - the following files have changed for your site {0}
{1}", site.Name, string.Join(",", changes.ToList()));

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

                    message.SetSendAt(DateTime.Now);
                }
            }
        }
    }
}
