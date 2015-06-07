using net.openstack.Core.Domain;
using net.openstack.Providers.Rackspace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudFilesMonitor.CloudProviders
{
    class RackspaceCloudProvider : ICloudProvider
    {
        public string FriendlyName
        {
            get
            {
                return "Rackspace Cloud Files";
            }           
        }

        public string CodeName
        {
            get
            {
                return "RackspaceCloudFiles";
            }
        }

        public Dictionary<string, string> AuthDetails
        {
            get;
            set;
        }


        public IEnumerable<MD5Result> GetFiles(string container)
        {
            return GetMD5ResultsForCloudContainer(container);
        }

        private MD5Result[] GetMD5ResultsForCloudContainer(string containerName)
        {

            var username = this.AuthDetails["Username"];
            var apiKey = this.AuthDetails["APIKey"];

            var cloudIdentity = new CloudIdentity() { Username = username, APIKey = apiKey };
            var cloudFilesProvider = new CloudFilesProvider(cloudIdentity);

            List<MD5Result> results = new List<MD5Result>();
            var cloudObjects = cloudFilesProvider.ListObjects(containerName);
            foreach (var cloudObject in cloudObjects)
            {
                var result = new MD5Result() { MD5 = cloudObject.Hash, CloudPath = cloudObject.Name, LocalPath = null };
                results.Add(result);
            }

            return results.OrderBy(x => x.CloudPath).ToArray();
        }


    }
}
