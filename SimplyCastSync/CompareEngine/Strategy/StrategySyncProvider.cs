using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SimplyCastSync.CompareEngine.Strategy.Attributes;

namespace SimplyCastSync.CompareEngine.Strategy
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="S"></typeparam>
    /// <typeparam name="D"></typeparam>
    public class StrategySyncProvider<S, D>
    {
        /// <summary>
        /// 
        /// </summary>
        public static StrategySyncProvider<S, D> SSP = new StrategySyncProvider<S, D>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strategyname"></param>
        /// <returns></returns>
        public Action<IComparerT<S, D>> GetStrategySync(string strategyname)
        {
            if (string.IsNullOrEmpty(strategyname))
            {
                return DefaultStrategy;
            }
            else if (strategyname == "DataSetToSimplyCastAPI")
            {
                return DataSet_SimplyCastAPIStrategy;
            }
            else if (strategyname == "SimplyCastAPIToDataSet")
            {
                return SimplyCastAPIToDataSetStrategy;
            }
            else
                throw new Exception("");
        }

        /// <summary>
        /// 
        /// </summary>
        private StrategySyncProvider()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        private Action<IComparerT<S, D>> DataSet_SimplyCastAPIStrategy = comparer =>
        {
            if (comparer.GetType() == typeof(JsonComparer<S, D>))
            {
                #region not used
                //var attrs = comparer.GetType().GetCustomAttributes(typeof(QueryStringProvider), false);
                //if (attrs != null && attrs.Length > 0)
                //{
                //    ((QueryStringProvider)attrs[0]).ProviderName = "SimplyCastQuery";
                //}
                #endregion

                //may introduce different strategies
                comparer.InitializeS("FromQueryString_Static");

                if (typeof(S) == typeof(DataSet))
                {
                    var src = JsonConvert.SerializeObject(comparer.Source as DataSet, Formatting.Indented);
                    var jsrc = JObject.Parse(src);

                    //this method has knowledge of json structure
                    foreach (var srd in jsrc.First.Values())
                    {
                        // keyparams is keycolumn properties pairs
                        var keyparams = new List<JToken>();
                        int i = 0;

                        //foreach(var param in comparer.Config["source"]["keyfields"])
                        foreach (var param in comparer.Config["source"]["fields"]["keyfields"])
                        {
                            #region comment
                            // use "ToLower" to get property value
                            //use [destination][keyfields] as property name
                            //((JArray)comparer.Config["destination"]["keyfields"])[i].ToString()
                            //keyparams.Add(new JProperty(param.Value<string>(), srd[param.Value<string>().ToLower()]));
                            #endregion

                            keyparams.Add(new JProperty(((JArray)comparer.Config["destination"]["fields"]["keyfieldsname"])[i].ToString(), srd[param.Value<string>().ToLower()]));
                            i++;
                        }

                        comparer.InitializeD("SeeParams_Single", keyparams.ToArray());

                        var valueparams = new List<JToken>();
                        int j = 0;

                        foreach (var param in comparer.Config["source"]["fields"]["valuefields"])
                        {
                            valueparams.Add(new JProperty(((JArray)comparer.Config["destination"]["fields"]["otherfieldsname"])[j].ToString(), srd[param.Value<string>().ToLower()]));
                            j++;
                        }

                        comparer.Mark("SeeParams_Add_UpdateAnyway", keyparams.Union(valueparams).ToArray());

                        comparer.Commit();
                    }
                }
                //
                else if (typeof(S) == typeof(JObject))
                {

                }
            }
        };

        /// <summary>
        /// 
        /// </summary>
        private Action<IComparerT<S, D>> SimplyCastAPIToDataSetStrategy = comparer =>
         {

         };

        /// <summary>
        /// 
        /// </summary>
        private Action<IComparerT<S, D>> DefaultStrategy = comparer =>
        {


        };


    }
}
