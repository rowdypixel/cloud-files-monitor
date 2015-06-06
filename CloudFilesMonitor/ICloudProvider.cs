using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudFilesMonitor
{
    public interface ICloudProvider
    {
        /// <summary>
        /// The name of this cloud provider (e.g., Amazon S3, Rackspace Cloud Files)
        /// </summary>
        string FriendlyName { get; }

        /// <summary>
        /// Any authorization details needed. Username/Password, API Key, etc
        /// </summary>
        Dictionary<string, string> AuthDetails { get; set; }

        /// <summary>
        /// Get a list of all the files on the cloud server.
        /// </summary>
        /// <returns></returns>
        IEnumerable<MD5Result> GetFiles(string container);

    }
}
