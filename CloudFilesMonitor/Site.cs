using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudFilesMonitor
{
    class Site
    {
        public string Name { get; set; }
        public string ContainerName { get; set; }

        public MD5Result[] Compare()
        {
            return Provider.GetFiles(ContainerName).ToArray();
        }

        public ICloudProvider Provider { get; set; }
    }
}
