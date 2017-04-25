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
        private Task task;

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
            if (task == null)
            {
                task = Task.Run(() =>
                 {
                     while (!Exit)
                     {
                         //RuntimeTimer
                         if (RTT.Timeout())
                         {
                             doit();
                         }
                         Thread.Sleep(1000);
                     }

                     Log.AddExceptionLog(new ExceptionBody { }, LogType.Console_File);
                 });
            }

            return task.Status;
        }

        /// <summary>
        /// 
        /// </summary>
        private EngineRuntime()
        {
            mutex = new object();
            exit = false;
            task = null;
        }
    }
}
