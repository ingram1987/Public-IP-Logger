using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;
using System.Threading;
using System.Configuration;
namespace PublicIPLogger
{
    class Helper
    {
        string url = "http://checkip.dyndns.org";
        string lastIP = "a";
        string currentIP = "b";
        public string GetPublicIP()
        {
            WebRequest req = WebRequest.Create(url);
            WebResponse resp = req.GetResponse();
            System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
            string response = sr.ReadToEnd().Trim();
            string[] a = response.Split(':');
            string a2 = a[1].Substring(1);
            string[] a3 = a2.Split('<');
            resp.Close();
            return Convert.ToString(a3[0]);
        }
        public void ProcessData()
        {
            int timeBetweenRequestsMS = 900000;
            EventLog myLogger = new EventLog("PublicIPLogger", ".", "PublicIPLogger");
            int i = 0;
            //Infinte loop for service to run in
            while (true)
            {
                try
                {
                    currentIP = GetPublicIP();
                    //If service just started, do not mark Changed IP event
                    if (i == 0)
                    {
                        lastIP = currentIP;
                        myLogger.WriteEntry(currentIP, EventLogEntryType.Information, 1010, 1);
                    }
                    if (i > 0)
                    {
                        if (currentIP != lastIP)
                        {
                            myLogger.WriteEntry(currentIP, EventLogEntryType.Information, 1012, 1);
                        }
                        else
                        {
                            myLogger.WriteEntry(currentIP, EventLogEntryType.Information, 1010, 1);
                        }
                    }
                    lastIP = currentIP;
                    i = 1;

                }
                catch (Exception e)
                {
                    myLogger.WriteEntry(e.ToString(), EventLogEntryType.Error, 1011, 1);
                    i = 2;
                }
                Thread.Sleep(timeBetweenRequestsMS);

            }
        }
    }
}
