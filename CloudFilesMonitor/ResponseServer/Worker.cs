using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CloudFilesMonitor.ResponseServer
{
    class Worker
    {
        private HttpListenerContext _context;
        public Worker(HttpListenerContext context)
        {
            _context = context;
        }

        public void ProcessRequest()
        {
            var req = _context.Request;
            var path = req.Url.AbsolutePath;

            if (path == "/ok")
            {
                var siteName = req.QueryString["site"];
                var msg = string.Format("Error updating site {0}", siteName);

                var site = SiteManager.AllSites.Where(x => x.Name == siteName).FirstOrDefault();
                if (site != null)
                {
                    site.SetCurrentAsValid();
                    msg = string.Format("Changes to {0} have been approved. You will be notified the next time this site changes.", siteName);
                }

               WriteString(msg);
            }
            else
            {
                WriteString("Not found");
            }

        }

        private void WriteString(string msg)
        {
            var bytes = Encoding.UTF8.GetBytes(msg);

            _context.Response.OutputStream.Write(bytes, 0, bytes.Length);
            _context.Response.Close();
        }
    }
}