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
            WebRequest req;
            WebResponse resp;
            EventLog myLogger = new EventLog("PublicIPLogger", ".", "PublicIPLogger");
            int i = 0;
            while (0 != 1)
            {
                Thread.Sleep(10000);
                string url = "http://checkip.dyndns.org";
                try
                {
                    req = WebRequest.Create(url);
                    resp = req.GetResponse();
                    System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
                    string response = sr.ReadToEnd().Trim();
                    string[] a = response.Split(':');
                    string a2 = a[1].Substring(1);
                    string[] a3 = a2.Split('<');
                    string a4 = a3[0];

                    myLogger.WriteEntry(Convert.ToString(a4), EventLogEntryType.Information, 1010, 1);
                    i++;

                }
                catch (Exception e)
                {
                    myLogger.WriteEntry(e.ToString(), EventLogEntryType.Error, 1011, 1);
                    i++;
                }


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
