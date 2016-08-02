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
        Helper helper = new Helper();
        private Thread thread;
        protected override void OnStart(string[] args)
        {
            //System.Diagnostics.Debugger.Launch();
            //Create eventlog if it does not exist
            if (!EventLog.Exists("PublicIPLogger"))
            {
                EventLog.CreateEventSource("PublicIPLogger", "PublicIPLogger");
            }
            thread = new Thread(new ThreadStart(helper.ProcessData));
            thread.Start();
            


        }



        protected override void OnStop()
        {
            thread.Abort();
        }
    }
}
