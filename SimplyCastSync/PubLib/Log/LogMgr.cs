using System;
using System.Threading.Tasks;

using static SimplyCastSync.PubLib.Log.Log;
using SimplyCastSync.Exceptions;

namespace SimplyCastSync.PubLib.Log
{
    public class LogMgr
    {
        /// <summary>
        /// 
        /// </summary>
        private static Task _consolelogtask = null;

        /// <summary>
        /// 
        /// </summary>
        private static Task _filelogtask = null;

        /// <summary>
        /// 
        /// </summary>
        public static Task ConsoleLogTask
        {
            get
            {
                if ((_consolelogtask == null) || (_consolelogtask.Status == TaskStatus.RanToCompletion))
                {
                    _consolelogtask = LogExportAsync.ExportConsoleLogAsync();
                }
                return _consolelogtask;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Task FileLogTask
        {
            get
            {
                if ((_filelogtask == null) || (_filelogtask.Status == TaskStatus.RanToCompletion))
                {
                    _filelogtask = LogExportAsync.ExportFileLogAsync();
                }
                return _filelogtask;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static void ExitConsoleLogTask()
        {
            new DomainException("Exiting Console Logging Task", ExceptionSrc.Exit, ExceptionType.Notification, LogType.Console);
            //AddExceptionLog(
            //    new ExceptionBody
            //    {
            //        es = ExceptionSrc.Exit,
            //        et = ExceptionType.Notification,
            //        info = "Exiting Console's Logging Task",
            //        ts = DateTime.Now
            //    }, LogType.Console_File);
        }

        /// <summary>
        /// 
        /// </summary>
        public static void ExitFileLogTask()
        {
            new DomainException("Exiting File Logging Task", ExceptionSrc.Exit, ExceptionType.Notification, LogType.File);
            //AddExceptionLog(
            //    new ExceptionBody
            //    {
            //        es = ExceptionSrc.Exit,
            //        et = ExceptionType.Notification,
            //        info = "Exiting File's Logging Task",
            //        ts = DateTime.Now
            //    }, LogType.Console_File);
        }
    }
}
