using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;

namespace PublicIPLogger
{
    public partial class PublicIPLogger : ServiceBase
    {
        public PublicIPLogger()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            if (!EventLog.Exists("PublicIPLogger"))
            {
                EventLog.CreateEventSource("Public IP Logger", "PublicIPLogger");
            }
            while (0 != 1)
            {
                Helper helper = new Helper();
                helper.getPublicIP();
                Thread.Sleep(3600000);
            }

        }

        protected override void OnStop()
        {
        }
    }
}
