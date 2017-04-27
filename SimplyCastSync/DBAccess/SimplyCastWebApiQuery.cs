using Newtonsoft.Json.Linq;
using SimplyCastSync.CompareEngine.Strategy.Utility;
using SimplyCastSync.Exceptions;
using SimplyCastSync.PubLib.Log;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace SimplyCastSync.DBAccess
{
    /// <summary>
    /// 
    /// </summary>
    public class SimplyCastWebApiQuery : IQuery<JObject>, IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        private string queryaddress = "";

        /// <summary>
        /// 
        /// </summary>
        private string updateaddress = "";

        /// <summary>
        /// 
        /// </summary>
        private string key = "";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        private void AddMetaData(HttpClient client, string optype)
        {
            if (optype == "query")
            {
                client.BaseAddress = new Uri(queryaddress);
            }
            else if (optype == "update")
            {
                client.BaseAddress = new Uri(updateaddress);
            }

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JObject GetData(string querystr)
        {
            //add log
            new DomainException("GetData in SimplyCastWebApi", ExceptionSrc.Processing, ExceptionType.Message);

            try
            {
                using (var client = new HttpClient())
                {
                    AddMetaData(client, "query");
                    using (var gettask = client.GetAsync(querystr))
                    {
                        gettask.Wait(TimeSpan.FromSeconds(15));
                        if (gettask.Result.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            using (var readtask = gettask.Result.Content.ReadAsStringAsync())
                            {
                                readtask.Wait();
                                return JObject.Parse(readtask.Result);
                            }
                        }
                        else if (gettask.Result.StatusCode == System.Net.HttpStatusCode.NoContent)
                        {
                            // need modify for configurable files, how to config file route......
                            return JObject.Parse(File.ReadAllText(@"JsonTemplate\simplycast_emptycontacts.json"));
                        }
                        else
                        {
                            throw new Exception("Unknown Response Status");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // add log
                throw new DomainException(ex.Message + " in SimplyCastWebAPI GetData", ExceptionSrc.Processing, ExceptionType.Error);
                //return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="extras"></param>
        /// <returns></returns>
        public void UpdateData(JObject ds, params string[] extras)
        {
            //need extras to thell httpclient the exact address of update
            //and this method has the knowledge of msg body format
            try
            {
                //opertaion of HttpClient
                using (var client = new HttpClient())
                {
                    var changeset = ds["_changeset"];

                    if (changeset != null)
                    {
                        foreach (var rd in changeset)
                        {
                            try
                            {
                                //add log
                                new DomainException("UpdateData in SimplyCastWebApi", ExceptionSrc.Processing, ExceptionType.Message);

                                var querystring = "";

                                if (rd["_rdstatus"].ToString() == "update")
                                {
                                    ((JObject)rd).Remove("_rdstatus");
                                    querystring = QueryStringResolver.GetQueryString("CommonRegex", extras[0], new JValue[] { (JValue)rd["_id"] });
                                    ((JObject)rd).Remove("_id");
                                }
                                else if (rd["_rdstatus"].ToString() == "add")
                                {
                                    ((JObject)rd).Remove("_rdstatus");
                                    querystring = extras[1];

                                }

                                AddMetaData(client, "update");

                                using (var updatetask = client.PostAsync(querystring, new StringContent(CreateBodyString((JObject)rd), Encoding.UTF8, "application/json")))
                                {
                                    updatetask.Wait();
                                    if ((updatetask.Result.StatusCode != System.Net.HttpStatusCode.OK) && (updatetask.Result.StatusCode != System.Net.HttpStatusCode.Created))
                                    {
                                        new DomainException("Update StatusCode Not OK in SimplyCastWebApi UpdateData", ExceptionSrc.Processing, ExceptionType.Error);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                new DomainException(ex.Message + " in SimplyCastWebApi UpdateData", ExceptionSrc.Processing, ExceptionType.Error);
                            }

                        } // end of foreach
                    }
                }

            } //end of using
            catch (Exception ex)
            {
                throw new DomainException(ex.Message + " in SimplyCastWebAPI UpdateData", ExceptionSrc.Processing, ExceptionType.Error);
            }

        }//end of method

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string CreateBodyString(JObject body)
        {
            var newbody = JObject.Parse(File.ReadAllText(@"JsonTemplate\simplycast_updatecontact.json"));
            foreach (var p in body)
            {
                var newitem = new JObject();
                newitem.Add("id", p.Key);
                newitem.Add("value", p.Value);
                ((JArray)newbody["contact"]["fields"]).Add(newitem);
            }

            return newbody.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public SimplyCastWebApiQuery(string connstr)
        {
            var info = connstr.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            string pubkey = "", secretkey = "";
            foreach (var configitem in info)
            {
                if (configitem.Split('=')[0] == "QueryAddress")
                {
                    queryaddress = configitem.Split('=')[1];
                }
                else if (configitem.Split('=')[0] == "UpdateAddress")
                {
                    updateaddress = configitem.Split('=')[1];
                }
                else if (configitem.Split('=')[0] == "PublicKey")
                {
                    pubkey = configitem.Split('=')[1];
                }
                else if (configitem.Split('=')[0] == "SecretKey")
                {
                    secretkey = configitem.Split('=')[1];
                }
            }

            key = Convert.ToBase64String(Encoding.ASCII.GetBytes(pubkey + ":" + secretkey));

            // add log
            new DomainException("SimplyCastWebApi Connection String Loaded", ExceptionSrc.Processing, ExceptionType.Notification);
        }
    }
}
