using SimplyCastSync.Config;
using SimplyCastSync.DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyCastSync.DataSource
{
    public class DataSrcProvider : ConfigReader, IDsProvider
    {
        /// <summary>
        /// 
        /// </summary>
        public static DataSrcProvider DsProvider { get; } = new DataSrcProvider();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dsname"></param>
        /// <returns></returns>
        public IDataSrc<T> GetDs<T>(string dsname)
        {
            var dsconfig = GetConfiguration(dsname);

            if (1 == 1)
            {
                //return new DbSrc();
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        private DataSrcProvider()
        {

        }
    }
}
