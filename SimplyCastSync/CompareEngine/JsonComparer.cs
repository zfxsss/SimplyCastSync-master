using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SimplyCastSync.CompareEngine.Strategy;
using SimplyCastSync.CompareEngine.Strategy.Attributes;
using SimplyCastSync.CompareEngine.Strategy.Utility;
using SimplyCastSync.DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimplyCastSync.CompareEngine
{
    /// <summary>
    /// 
    /// </summary>
    [QueryStringProvider("CommonRegex")]
    [StrategyValidation("Default")]
    public class JsonComparer<S, D> : IComparerT<S, D>
    {
        /// <summary>
        /// 
        /// </summary>
        private IQuery<S> sourceq;

        /// <summary>
        /// 
        /// </summary>
        private IQuery<D> destinationq;

        /// <summary>
        /// 
        /// </summary>
        public S Source { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public D Destination { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public JObject Config { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public Action<IComparerT<S, D>> StrategySync { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        public JsonComparer(IQuery<S> srcq, IQuery<D> destq, JObject config)
        {
            sourceq = srcq;
            destinationq = destq;

            Config = config;

            if (!string.IsNullOrWhiteSpace(Config["syncstrategy"].ToString()))
            {
                StrategySync = StrategySyncProvider<S, D>.SSP.GetStrategySync(Config["syncstrategy"].ToString());
            }
            else
            {
                StrategySync = StrategySyncProvider<S, D>.SSP.GetStrategySync("");
            }

            TypeCheck();
        }

        /// <summary>
        /// 
        /// </summary>
        private void TypeCheck()
        {
            // source
            if (typeof(S) == typeof(DataSet))
            {
                //jsource = JObject.Parse(JsonConvert.SerializeObject(source));
                //add log here

            }
            else if (typeof(S) == typeof(JObject))
            {
                //add log here

            }
            else
            {
                //add log and throw exception
                throw new Exception("Unsupported Source Type");
            }

            // destination
            if (typeof(D) == typeof(DataSet))
            {
                //jsource = JObject.Parse(JsonConvert.SerializeObject(source));
                //add log here

            }
            else if (typeof(D) == typeof(JObject))
            {
                //add log here
            }
            else
            {
                //add log and throw exception
                throw new Exception("Unsupported Source Type");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strategyname"></param>
        /// <param name="p"></param>
        private void StrategyValidationCheck(string strategyname, object[] p)
        {
            var attrs = GetType().GetCustomAttributes(typeof(StrategyValidation), false);

            if (attrs != null && attrs.Length > 0)
            {
                if ((attrs[0] as StrategyValidation).Collection.First(s => s.StrategyName == strategyname).UsingParams)
                {
                    if (p == null || p.Length == 0)
                    {
                        throw new Exception("");
                    }
                }
            }
            else
            {
                //throw error
                throw new Exception("");
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="strategy"></param>
        /// <param name="sp"></param>
        public void InitializeS(string strategy, params object[] sp)
        {
            //StrategyValidationCheck(strategy, sp);

            if (strategy == "FromQueryString_Static")
            {
                Source = sourceq.GetData(Config["source"]["querystring"].ToString());
            }
            // might use sp params
            else
            {
                if (sp != null && sp.Length > 0)
                {
                    //add handler
                }
                else
                {
                    //might throw exceptions
                }
            }

            if (Source == null)
            {
                //add log and throw exception

            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="strategy"></param>
        /// <param name="dp"></param>
        public void InitializeD(string strategy, params object[] dp)
        {
            //StrategyValidationCheck(strategy, dp);

            string querystr = "";

            if (strategy == "SeeParams_Single")
            {
                if (dp != null && dp.Length > 0)
                {
                    //resolve querystring
                    var attrs = GetType().GetCustomAttributes(typeof(QueryStringProvider), false);
                    if (attrs != null && attrs.Length > 0)
                    {
                        querystr = QueryStringResolver.GetQueryString(((QueryStringProvider)attrs[0]).ProviderName, Config["destination"]["querystring"].ToString(), dp);
                        Destination = destinationq.GetData(querystr);
                    }
                    else
                    {
                        //throw error

                    }
                }
            }
            //other strategy
            else
            {
                //no params
                Destination = destinationq.GetData("");
            }

            if (Destination == null)
            {
                // add log and throw exception

            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="strategy"></param>
        /// <param name="mp"></param>
        public void Mark(string strategy, params object[] mp)
        {
            //StrategyValidationCheck(strategy, mp);

            if (strategy == "SeeParams_Add_UpdateAnyway")
            {

                if (typeof(D) == typeof(DataSet))
                {
                    //implemented later
                    var row_existing = (Destination as DataSet).Tables[0].Select("");

                }
                else if (typeof(D) == typeof(JObject))
                {

                    //resolve jsonpath

                    var token_existing = (Destination as JObject).SelectToken("");

                    //existing
                    if (token_existing != null)
                    {
                        //update anyway, so add "update tags" at the root of object "destination"
                        //add root property "_changeset": [{"_id":1,"name":"Joe"...,"_rdstatus":"update"},{"name":"Harry",...,"_rdstatus":"add"}]

                        #region not used?
                        //var token_equal = (Destination as JObject).SelectToken("");
                        //if (token_equal != null)
                        //{
                        //    //Destination operation


                        //}
                        #endregion

                        var changerow = new JObject(mp.Where(x => ((JArray)Config["destination"]["fields"]["otherfieldsname"]).Values<string>().Contains(((JProperty)x).Name)));
                        changerow.Add("_rdstatus", new JValue("update"));
                        var changeset = new JArray(changerow);
                        (Destination as JObject).Add("_changeset", changeset);

                    }
                    //not existing
                    else
                    {
                        //operate at root of object "destination"
                        //add root property "_changeset": [{"id":1,"name":"Joe"...,"_rdstatus":"update"},{"name":"Harry",...,"_rdstatus":"add"}]

                        var changerow = new JObject(mp.Where(x => ((JArray)Config["destination"]["fields"]["otherfieldsname"]).Values<string>().Contains(((JProperty)x).Name)));
                        changerow.Add("_rdstatus", new JValue("add"));
                        var changeset = new JArray(changerow);
                        (Destination as JObject).Add("_changeset", changeset);

                    }

                }

                #region not used
                //else
                //{
                //    if (typeof(D) == typeof(DataSet))
                //    {
                //        if (typeof(S) == typeof(DataSet))
                //        {

                //        }
                //        else if (typeof(S) == typeof(JObject))
                //        {

                //        }
                //    }
                //    else if (typeof(D) == typeof(JObject))
                //    {
                //        if (typeof(S) == typeof(DataSet))
                //        {

                //        }
                //        else if (typeof(S) == typeof(JObject))
                //        {

                //        }
                //    }
                //}
                #endregion

            }
            // not implemented yet!
            else
            {
                throw new Exception("");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public void Commit()
        {
            // need to provide remote url in the second parameter
            destinationq.UpdateData(Destination, null);

            // add log

            #region temperory
            //if (typeof(D) == typeof(JObject))
            //{
            //    destinationq.UpdateData(Destination);
            //}
            //else if (typeof(D) == typeof(DataSet))
            //{
            //    destinationq.UpdateData(Destination);
            //}
            #endregion
        }


    }
}
