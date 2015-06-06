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

            // Start the server to let users respond.
            ResponseServer.Server.Init();

            // Initialize sites.
            SiteManager.Load();
                        
            //Set up the timer.
            var timerCheckInterval = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["TimerCheckIntervalSeconds"]) * 1000;
            Timer t = new Timer(Timer_Tick, SiteManager.AllSites, 0, timerCheckInterval);

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

                    Email.Send(body); 
                    
                    Console.WriteLine("\t !!!!!!CHANGES DETECTED!!!!!! Mail Sent !!!!!!CHANGES DETECTED!!!!!!");
                }
            }
        }
    }
}
