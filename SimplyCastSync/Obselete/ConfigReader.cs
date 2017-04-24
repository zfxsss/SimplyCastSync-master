using System.Collections.Generic;
using static SimplyCastSync.Config.ConfigRepository;

namespace SimplyCastSync.Config
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfigReader : IConfigReader<ConfigItems<KeyValuePair<string, string>>>
    {
        /// <summary>
        /// 
        /// </summary>
        public ConfigItems<KeyValuePair<string, string>> ConfigInfo { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configname"></param>
        /// <returns></returns>
        public ConfigItems<KeyValuePair<string, string>> GetConfiguration(string configname)
        {
            //Content;
            ConfigInfo = null;
            return ConfigInfo;
        }
    }
}
