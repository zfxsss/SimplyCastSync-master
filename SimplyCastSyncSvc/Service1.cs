using SimplyCastSync.SimplyCast_Sunix;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

using static SimplyCastSync.Runtime.EngineRuntime;

namespace SimplyCastSyncSvc
{
    public partial class Service1 : ServiceBase
    {

        /// <summary>
        /// 
        /// </summary>
        private Task background_sync;

        /// <summary>
        /// 
        /// </summary>
        public Service1()
        {
            InitializeComponent();
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            background_sync = Runtime.Run(Comparer.Sync);
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnStop()
        {
            Runtime.Exit = true;
            //if (background_sync != null)
            //    background_sync.Wait();
        }
    }
}
