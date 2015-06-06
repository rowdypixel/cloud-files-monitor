using System;
using System.Collections.Generic;
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
                var bytes = Encoding.UTF8.GetBytes(req.QueryString["site"]);
                _context.Response.OutputStream.Write(bytes, 0, bytes.Length);
                _context.Response.Close();
            }

        }
    }
}