using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyCastSync.CompareEngine.Strategy.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class QueryStringProvider : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="providername"></param>
        public QueryStringProvider(string providername)
        {
            ProviderName = providername;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ProviderName { get; private set; }
    }
}
