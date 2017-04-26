using SimplyCastSync.Config;
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
        public Task Run(Action doit)
        {
            Exit = false;
            RTT.Clear();
            ConfigRepository.Clear();


            if ((T == null) || (T.Status == TaskStatus.RanToCompletion))
            {
                T = Task.Run(() =>
                 {
                     var t_consolelog = LogMgr.ConsoleLogTask;
                     var t_filelog = LogMgr.FileLogTask;

                     //might fail
                     new DomainException("Sync Worker Launched", ExceptionSrc.Init, ExceptionType.Notification);

                     while (!Exit)
                     {
                         try
                         {
                             //RuntimeTimer
                             if (RTT.Timeout())
                             {
                                 //main task
                                 doit();
                             }
                             Thread.Sleep(1000);
                         }
                         catch (Exception ex)
                         {
                             if (ex.GetType() == typeof(DomainException))
                             {
                                 if ((ex as DomainException).EB.et == ExceptionType.System)
                                 {
                                     //System Error
                                     throw ex;
                                 }
                             }
                             else
                             {
                                 new DomainException(ex.Message + " Catched in EngineRuntime", ExceptionSrc.Processing, ExceptionType.Error);
                             }
                         }
                     }

                     new DomainException("Sync Worker Exiting", ExceptionSrc.Cleanup, ExceptionType.Notification, LogType.Console);

                     LogMgr.ExitConsoleLogTask();
                     LogMgr.ExitFileLogTask();

                     t_consolelog.Wait();
                     t_filelog.Wait();

                     //Log.AddExceptionLog(new ExceptionBody { }, LogType.Console_File);
                 });
            }

            return T;
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
