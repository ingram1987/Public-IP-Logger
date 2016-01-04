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
using System.Timers;


namespace PublicIPLogger
{
    public partial class PublicIPLogger : ServiceBase
    {
        public PublicIPLogger()
        {
            InitializeComponent();
        }
        //private Thread executeThread;
        Helper helper = new Helper();
        //private Thread thread = new Thread(new ThreadStart(helper.getPublicIP));
        private Thread thread;
        protected override void OnStart(string[] args)
        {
            if (!EventLog.Exists("PublicIPLogger"))
            {
                EventLog.CreateEventSource("PublicIPLogger", "PublicIPLogger");
            }
            /*
            while (0 != 1)
            {
                Helper helper = new Helper();
                helper.getPublicIP();
                Thread.Sleep(10000);
            }*/
            thread = new Thread(new ThreadStart(helper.getPublicIP));
            thread.Start();
            


        }



        protected override void OnStop()
        {
            thread.Abort();
        }
    }
}
