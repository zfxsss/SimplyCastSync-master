using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyCastSync.CompareEngine.FieldOperation
{
    /// <summary>
    /// 
    /// </summary>
    public class JsonFieldEquatable : IEquatable<JToken>
    {
        /// <summary>
        /// 
        /// </summary>
        private JToken self;

        /// <summary>
        /// 
        /// </summary>
        private JObject fields;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Equals(JToken t)
        {
            if (self == t)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="kvps"></param>
        public JsonFieldEquatable(JToken t, JObject config)
        {
            self = t;
            fields = config;
        }

    }
}
