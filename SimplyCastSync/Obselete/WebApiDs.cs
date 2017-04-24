using Newtonsoft.Json.Linq;
using SimplyCastSync.Config;
using SimplyCastSync.DBAccess;
using System.Collections.Generic;

namespace SimplyCastSync.DataSource
{
    /// <summary>
    /// 
    /// </summary>
    public class WebApiDs : RdHandler<JArray, JToken>, IDataSrc<JArray>
    {
        /// <summary>
        /// 
        /// </summary>
        public IQuery<JArray> Q { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public ConfigItems<KeyValuePair<string, string>> Config { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public JArray Ds { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dsname"></param>
        public WebApiDs(IQuery<JArray> q, ConfigItems<KeyValuePair<string, string>> config) : base(null, null, null)
        {
            //Q = new SimplyCastWebApiQuery();
            Q = q;
            Config = config;

            //Ds = q.GetData("");

            //Q.UpdateData(Ds);
        }


    }
}
