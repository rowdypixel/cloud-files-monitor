using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;

namespace CloudFilesMonitor.ResponseServer
{
    class Server
    {
        private static HttpListener _http = new HttpListener();

        public static void Init()
        {
            var host = "localhost";
            var port = "1485";
            _http.Prefixes.Add(string.Format("http://{0}:{1}/", host, port));
            _http.Start();

            while(_http.IsListening)
            {
                HttpListenerContext context = _http.GetContext();
                new Thread(new Worker(context).ProcessRequest).Start();
            }
        }

        public static void Shutdown()
        {
            _http.Stop();
        }
    }
}
