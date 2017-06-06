using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static SimplyCastSync.Config.ConfigRepository;
using Newtonsoft.Json.Linq;
using System.Data;
using SimplyCastSync.DBAccess;
using Newtonsoft.Json;
using SimplyCastSync.CompareEngine.Strategy.Utility;
using SimplyCastSync.Exceptions;

namespace SimplyCastSync.SimplyCast_Sunix
{
    /// <summary>
    /// 
    /// </summary>
    public class Comparer
    {
        /// <summary>
        /// 
        /// </summary>
        public static Action Sync = () =>
        {

            var pairsconfig = Content["pairs"];
            if ((pairsconfig != null) && pairsconfig.GetType() == typeof(JArray))
            {
                var src = default(JObject);
                var dest = default(JObject);
                //string syncstrategy = "";
                foreach (var pair in pairsconfig)
                {
                    try
                    {
                        src = pair["source"] as JObject;
                        dest = pair["destination"] as JObject;

                        var src_ds_config = Content["datasource"].First(x => x["name"].ToString() == pair["source"]["ds"].ToString());
                        var dest_ds_config = Content["datasource"].First(x => x["name"].ToString() == pair["destination"]["ds"].ToString());

                        // DataSet => JObject
                        if ((src["dstype"].ToString() == "DataSet") && (dest["dstype"].ToString() == "JObject"))
                        {
                            IQuery<DataSet> q_src = QueryBuilder.DsBuilder.GetQuery<DataSet>(src_ds_config["queryname"].ToString(), src_ds_config["connstr"].ToString());
                            IQuery<JObject> q_dest = QueryBuilder.DsBuilder.GetQuery<JObject>(dest_ds_config["queryname"].ToString(), dest_ds_config["connstr"].ToString());

                            SunixToSimplyCast(q_src, q_dest, (JObject)pair);
                        }
                        // JObject => DataSet
                        else if ((src["dstype"].ToString() == "JObject") && (dest["dstype"].ToString() == "DataSet"))
                        {
                            IQuery<JObject> q_src = QueryBuilder.DsBuilder.GetQuery<JObject>(src_ds_config["queryname"].ToString(), src_ds_config["connstr"].ToString());
                            IQuery<DataSet> q_dest = QueryBuilder.DsBuilder.GetQuery<DataSet>(dest_ds_config["queryname"].ToString(), dest_ds_config["connstr"].ToString());

                            SimplyCastToSunix(q_src, q_dest, (JObject)pair);
                        }
                        // not supported
                        else
                            throw new DomainException("Sync Pair Not Supported", PubLib.Log.ExceptionSrc.Processing, PubLib.Log.ExceptionType.Error);
                    }
                    catch (Exception)
                    {
                        new DomainException("Synchronization might Have Problem", PubLib.Log.ExceptionSrc.Processing, PubLib.Log.ExceptionType.Warning);
                    }

                }
            }
            else
            {
                throw new DomainException("Synchronization Configuration Not Found", PubLib.Log.ExceptionSrc.Init, PubLib.Log.ExceptionType.System, PubLib.Log.LogType.Console);
            }


        };

        /// <summary>
        /// 
        /// </summary>
        private static Action<IQuery<DataSet>, IQuery<JObject>, JObject> SunixToSimplyCast = (srcquery, destquery, config) =>
        {
            DataSet source = srcquery.GetData(config["source"]["querystring"].ToString());

            var src = JsonConvert.SerializeObject(source, Formatting.Indented);
            var jsrc = JObject.Parse(src);
            source.Dispose();

            foreach (var srd in jsrc.First.Values())
            {
                var keyparams = new List<JToken>();
                int i = 0;

                foreach (var param in config["source"]["fields"]["keyfields"])
                {
                    //keyparams.Add(new JProperty(((JArray)config["destination"]["fields"]["keyfieldsname"])[i].ToString(), srd[param.Value<string>().ToLower()]));
                    keyparams.Add(new JProperty(((JArray)config["destination"]["fields"]["keyfieldsindex"])[i].ToString(), srd[param.Value<string>().ToLower()]));
                    i++;
                }

                string querystr = QueryStringResolver.GetQueryString("CommonRegex", config["destination"]["querystring"].ToString(), keyparams.ToArray());
                JObject destination = destquery.GetData(querystr);

                var valueparams = new List<JToken>();
                int j = 0;

                foreach (var param in config["source"]["fields"]["valuefields"])
                {
                    //valueparams.Add(new JProperty(((JArray)config["destination"]["fields"]["otherfieldsname"])[j].ToString(), srd[param.Value<string>().ToLower()]));
                    valueparams.Add(new JProperty(((JArray)config["destination"]["fields"]["otherfieldsindex"])[j].ToString(), srd[param.Value<string>().ToLower()]));
                    j++;
                }

                //resolve jsonpath
                //var token_existing = JObject.Parse("{'contacts': [{'id': '100'}]}").SelectToken("$.contacts[0].id");
                var token_existing = destination.SelectToken(config["destination"]["fields"]["existingjsonpath"].ToString());

                string updatestr = config["destination"]["updatestring"].ToString();
                string addstr = config["destination"]["addstring"].ToString();

                //existing
                if (token_existing != null)
                {
                    var changerow = new JObject(keyparams.Union(valueparams).ToArray().Where(x => ((JArray)config["destination"]["fields"]["otherfieldsindex"]).Values<string>().Contains(((JProperty)x).Name)));
                    changerow.Add("_rdstatus", new JValue("update"));
                    changerow.Add("_id", (JValue)token_existing);
                    var changeset = new JArray(changerow);
                    destination.Add("_changeset", changeset);

                    // resolve it
                    //updatestr = QueryStringResolver.GetQueryString("CommonRegex", updatestr, new JValue[] { (JValue)token_existing });
                }
                //not existing
                else
                {
                    var changerow = new JObject(keyparams.Union(valueparams).ToArray());
                    changerow.Add("_rdstatus", new JValue("add"));
                    var changeset = new JArray(changerow);
                    destination.Add("_changeset", changeset);
                }
                destquery.UpdateData(destination, new string[] { updatestr, addstr });

            }

        };

        /// <summary>
        /// 
        /// </summary>
        private static Action<IQuery<JObject>, IQuery<DataSet>, JObject> SimplyCastToSunix = (srcquery, destquery, config) =>
         {


         };

    }
}
