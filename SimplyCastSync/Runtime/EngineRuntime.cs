using SimplyCastSync.Exceptions;
using SimplyCastSync.PubLib.Log;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using static SimplyCastSync.Runtime.RuntimeTimer;

namespace SimplyCastSync.Runtime
{
    /// <summary>
    /// 
    /// </summary>
    public class EngineRuntime : IEngine
    {
        /// <summary>
        /// 
        /// </summary>
        private static IEngine runtime;

        /// <summary>
        /// 
        /// </summary>
        public static IEngine Runtime
        {
            get
            {
                if (runtime == null)
                {
                    runtime = new EngineRuntime();
                }
                return runtime;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private object mutex;

        /// <summary>
        /// 
        /// </summary>
        private bool exit;

        /// <summary>
        /// 
        /// </summary>
        public Task T { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Exit
        {
            get
            {
                bool temp;
                lock (mutex)
                {
                    temp = exit;
                }
                return temp;
            }
            set
            {
                lock (mutex)
                {
                    exit = value;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public TaskStatus Run(Action doit)
        {
            if (T == null)
            {
                T = Task.Run(() =>
                 {
                     var t_consolelog = LogMgr.ConsoleLogTask;
                     var t_filelog = LogMgr.FileLogTask;

                     new DomainException("Sync Worker Launched", ExceptionSrc.Init, ExceptionType.Notification, LogType.Console);
                     while (!Exit)
                     {
                         //RuntimeTimer
                         if (RTT.Timeout())
                         {
                             try
                             {
                                 doit();
                             }
                             catch (Exception ex)
                             {

                             }
                         }
                         Thread.Sleep(1000);
                     }

                     LogMgr.ExitConsoleLogTask();
                     LogMgr.ExitFileLogTask();

                     t_consolelog.Wait();
                     t_filelog.Wait();

                     new DomainException("Sync Worker Exited", ExceptionSrc.Cleanup, ExceptionType.Notification, LogType.Console);

                     //Log.AddExceptionLog(new ExceptionBody { }, LogType.Console_File);
                 });
            }

            return T.Status;
        }

        /// <summary>
        /// 
        /// </summary>
        private EngineRuntime()
        {
            mutex = new object();
            exit = false;
            T = null;
        }
    }
}
