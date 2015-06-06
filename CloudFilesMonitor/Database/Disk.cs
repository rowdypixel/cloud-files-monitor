using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudFilesMonitor.Database
{
    class Disk
    {
        public static void OpenFile(string fileName)
        {
            Database.Helper.CurrentHelper = new Helper(fileName);
        }
    }
}
