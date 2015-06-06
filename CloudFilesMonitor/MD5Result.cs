using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudFilesMonitor
{
    public class MD5Result
    {
        /// <summary>
        /// The MD5 Hash of the file.
        /// </summary>
        public string MD5 { get; set; }

        /// <summary>
        /// The local path of the file (to restore if desired)
        /// </summary>
        public string LocalPath { get; set; }

        /// <summary>
        /// The path of the file on the cloud server.
        /// </summary>
        public string CloudPath { get; set; }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            MD5Result theObject = obj as MD5Result;
            if (theObject == null)
                return base.Equals(obj);
            else
                return (theObject.CloudPath.ToLower() == this.CloudPath.ToLower() && theObject.MD5.ToLower() == this.MD5.ToLower());
        }

        public override string ToString()
        {
            return CloudPath;
        }
    }
}
