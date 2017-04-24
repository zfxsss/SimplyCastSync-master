using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SimplyCastSync.CompareEngine.Strategy.Utility
{
    /// <summary>
    /// 
    /// </summary>
    public class QueryStringResolver
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="providername"></param>
        /// <param name="paramsarray"></param>
        /// <returns></returns>
        public static string GetQueryString(string providername, string template, object[] paramsarray)
        {
            if (providername == "CommonRegex")
            {
                return CommonRegex(template, paramsarray);
            }
            else
                throw new Exception("");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="template"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        private static string CommonRegex(string template, object[] array)
        {
            string pat = @"{[0-9]+}";
            Regex regex = new Regex(pat);
            var collection = regex.Matches(template);

            int i = 0;
            foreach (Match item in collection)
            {
                string replacement = "";
                if (array[i].GetType() == typeof(JProperty))
                {
                    replacement = ((JProperty)array[i]).Value.ToString();
                }
                else if (array[i].GetType() == typeof(JValue))
                {
                    replacement = ((JValue)array[i]).ToString();
                }

                template = template.Replace(item.Value, replacement);
                i++;
            }

            return template;
        }
    }
}
