using SimplyCastSync.Config;
using SimplyCastSync.DBAccess;
using System.Collections.Generic;
using System.Data;

namespace SimplyCastSync.DataSource
{
    public class DbSrc : RdHandler<DataSet, DataRow>, IDataSrc<DataSet>
    {
        /// <summary>
        /// 
        /// </summary>
        public IQuery<DataSet> Q { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public ConfigItems<KeyValuePair<string, string>> Config { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public DataSet Ds { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            //Ds.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        public DbSrc(IQuery<DataSet> q, ConfigItems<KeyValuePair<string, string>> config) : base(null, null, null)
        {
            //Q = new FoxproDataQuery();
            Q = q;
            Config = config;

            //Ds = Q.GetData("");
        }


    }
}
