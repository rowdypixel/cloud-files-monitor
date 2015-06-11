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
            SiteManager.Load();

            //Set up the timer.
            var timerCheckInterval = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["TimerCheckIntervalSeconds"]) * 1000;
            Timer t = new Timer(Timer_Tick, SiteManager.AllSites, 0, timerCheckInterval);

            // Start the server to let users respond.
            ResponseServer.Server.Init();
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

                    var host = System.Configuration.ConfigurationManager.AppSettings["ServerHost"];
                    var port = System.Configuration.ConfigurationManager.AppSettings["ServerPort"];

                    var url = string.Format("http://{0}:{1}", host, port);

                    string body = string.Format(@"Warning - the following files have changed on site {0}
<br />
{1}
<br />
<a href='{2}/?ok?site={0}'>Approve</a>
<a href='{2}/restore?site={0}'>Restore</a>", site.Name, string.Join(",", changes.ToList()), url);

                    Email.Send(body);
#if DEBUG
                    Console.WriteLine("\tEmails don't send in debug mode.");
#else
                    Console.WriteLine("\t !!!!!!CHANGES DETECTED!!!!!! Mail Sent !!!!!!CHANGES DETECTED!!!!!!");

#endif
                }
            }
        }
    }
}
