using System;
using System.IO;
using Newtonsoft.Json.Linq;

using static SimplyCastSync.PubLib.Log.Log;

namespace SimplyCastSync.Config
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfigRepository
    {
        private static JObject _content = null;
        /// <summary>
        /// 
        /// </summary>
        public static JObject Content
        {
            get
            {
                try
                {
                    if (_content == null)
                    {
                        //_content = JObject.Parse(File.ReadAllText("process_config.json"));
                        _content = JObject.Parse(File.ReadAllText("process_config_simple.json"));
                    }
                    return _content;
                }
                catch (Exception ex)
                {
                    AddExceptionLog(new PubLib.Log.ExceptionBody { }, PubLib.Log.LogType.Console_File);
                    return null;
                }
            }

        }
    }
}
