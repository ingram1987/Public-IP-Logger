using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;
using System.Threading;
namespace PublicIPLogger
{
    class Helper
    {
        public void getPublicIP()
        {
            while (0 != 1)
            {
                //Helper helper = new Helper();
                //helper.getPublicIP();
                
                string url = "http://checkip.dyndns.org";
                WebRequest req = WebRequest.Create(url);
                WebResponse resp = req.GetResponse();
                EventLog myLogger = new EventLog("PublicIPLogger", ".", "PublicIPLogger");

                System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
                string response = sr.ReadToEnd().Trim();
                string[] a = response.Split(':');
                string a2 = a[1].Substring(1);
                string[] a3 = a2.Split('<');
                string a4 = a3[0];

                myLogger.WriteEntry(Convert.ToString(a4));
                Thread.Sleep(10000);
            }

        }

        public void OnTimedEvent()
        {
            Execute();
        }
        public void Execute()
        {
            Console.WriteLine("Executing");
        }
    }
}
