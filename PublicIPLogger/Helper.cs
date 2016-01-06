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
        string url = "http://checkip.dyndns.org";
        
        public string GetPublicIP()
        {
            WebRequest req = WebRequest.Create(url);
            WebResponse resp = req.GetResponse();
            System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
            string response = sr.ReadToEnd().Trim();
            string[] a = response.Split(':');
            string a2 = a[1].Substring(1);
            string[] a3 = a2.Split('<');
            return Convert.ToString(a3[0]);
        }
        public void ProcessData()
        {
            string lastIP = "";
            string currentIP = "";
            int timeBetweenRequestsMS = 100000;
            EventLog myLogger = new EventLog("PublicIPLogger", ".", "PublicIPLogger");
            int i = 0;
            while (0 != 1)
            {
                Thread.Sleep(timeBetweenRequestsMS);
                try
                {
                    currentIP = GetPublicIP();
                    if (i > 0)
                    {
                        if (currentIP != lastIP)
                        {
                            myLogger.WriteEntry(currentIP, EventLogEntryType.Information, 1012, 1);
                        }
                    }
                    else
                    {
                        myLogger.WriteEntry(currentIP, EventLogEntryType.Information, 1010, 1);
                    }
                    lastIP = currentIP;
                    i++;

                }
                catch (Exception e)
                {
                    myLogger.WriteEntry(e.ToString(), EventLogEntryType.Error, 1011, 1);
                    i++;
                }


            }
        }
    }
}
