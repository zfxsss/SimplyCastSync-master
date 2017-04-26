using static SimplyCastSync.PubLib.Log.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplyCastSync.PubLib.Log;
using SimplyCastSync.Config;

namespace SimplyCastSync.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class DomainException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public ExceptionBody EB { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="domain"></param>
        public DomainException(string msg, ExceptionSrc excepsrc, ExceptionType exceptype) : base(msg)
        {
            EB = new ExceptionBody
            {
                es = excepsrc,
                et = exceptype,
                info = msg,
                ts = DateTime.Now
            };

            AddExceptionLog(EB, GetLogInputEnum(ConfigRepository.Content["running"]["loginput"].ToString()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="excepsrc"></param>
        /// <param name="exceptype"></param>
        /// <param name="lt"></param>
        public DomainException(string msg, ExceptionSrc excepsrc, ExceptionType exceptype, LogType lt) : base(msg)
        {
            EB = new ExceptionBody
            {
                es = excepsrc,
                et = exceptype,
                info = msg,
                ts = DateTime.Now
            };
            AddExceptionLog(EB, lt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        private LogType GetLogInputEnum(string config)
        {
            switch (config)
            {
                case "Console_File":
                    return LogType.Console_File;
                case "File":
                    return LogType.File;
                case "Console":
                    return LogType.Console;
                default:
                    throw new Exception("No Log Input Type Specified");
            }

        }
    }
}
