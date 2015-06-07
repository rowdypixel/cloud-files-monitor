using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudFilesMonitor
{
    class SiteManager
    {
        public static Site[] AllSites { get; private set; }

        public static void AddSite(Site site)
        {
            throw new NotImplementedException();
        }

        public static void DeleteSite(Site site)
        {
            throw new NotImplementedException();
        }

        public static void Save()
        {
            var path = System.Configuration.ConfigurationManager.AppSettings["SitesConfigFile"];

            System.IO.File.WriteAllText(path,
                JsonConvert.SerializeObject(AllSites));
        }

        public static void Load()
        {
            var path = System.Configuration.ConfigurationManager.AppSettings["SitesConfigFile"];
            var json = System.IO.File.ReadAllText(path);
            AllSites = JsonConvert.DeserializeObject<Site[]>(json);
        }
    }
}
