using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using System.Configuration;

namespace Litdev.Service
{
    public partial class PollingService : ServiceBase
    {
        private IDisposable apiserver = null;
        
        public PollingService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            
            Trace.Listeners.Clear();
            Trace.Listeners.Add(new LitdevTraceListener());//添加自定义监听器
            
            //Services URI
            string serveruri = "http://localhost:" + ServiceConfig.WebAPIPort + "/";
            // Start OWIN host
            apiserver = WebApp.Start<Startup>(url: serveruri);


            //Trace.WriteLine("服务启动...");


        }

        protected override void OnStop()
        {
            //Trace.WriteLine("服务关闭");
        }
    }
}
