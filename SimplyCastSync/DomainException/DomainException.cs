using static SimplyCastSync.PubLib.Log.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplyCastSync.PubLib.Log;
using SimplyCastSync.Config;

namespace SimplyCastSync.DomainException
{
    /// <summary>
    /// 
    /// </summary>
    public class DomainException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="domain"></param>
        public DomainException(Exception ex, string domain)
        {
            AddExceptionLog(new ExceptionBody
            {

            },
            GetLogInputEnum(ConfigRepository.Content["running"]["loginput"].ToString()));
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
