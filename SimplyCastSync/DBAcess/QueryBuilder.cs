using static SimplyCastSync.Config.ConfigRepository;
using SimplyCastSync.DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyCastSync.DBAccess
{
    public class QueryBuilder : IDsBuilder
    {
        public static QueryBuilder DsBuilder { get; } = new QueryBuilder();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dsname"></param>
        /// <returns></returns>
        public IQuery<T> GetQuery<T>(string queryname, string connstr)
        {
            if (queryname == "FoxproData")
            {
                return new FoxproDataQuery(connstr) as IQuery<T>;
            }
            else if (queryname == "MysqlData")
            {
                return new MysqlDataQuery(connstr) as IQuery<T>;
            }
            else if (queryname == "SimplyCastWebApi")
            {
                return new SimplyCastWebApiQuery(connstr) as IQuery<T>;
            }
            else
                throw new Exception("");

        }

        /// <summary>
        /// 
        /// </summary>
        private QueryBuilder()
        {

        }
    }
}
