using Newtonsoft.Json.Linq;
using SimplyCastSync.CompareEngine.Strategy.Utility;
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
            using (var client = new HttpClient())
            {
                try
                {
                    AddMetaData(client, "query");
                    var gettask = client.GetAsync(querystr);
                    gettask.Wait(TimeSpan.FromSeconds(15));
                    if (gettask.Result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var readtask = gettask.Result.Content.ReadAsStringAsync();
                        readtask.Wait();
                        return JObject.Parse(readtask.Result);
                    }
                    else if (gettask.Result.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        // need modify for configurable files, how to config file route......
                        return JObject.Parse(File.ReadAllText(@"JsonTemplate\simplycast_emptycontacts.json"));
                    }
                    else
                    {
                        throw new Exception("");
                    }
                }
                catch (Exception ex)
                {
                    // add log

                    return null;
                }
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

            //opertaion of HttpClient
            using (var client = new HttpClient())
            {
                try
                {
                    var changeset = ds["_changeset"];

                    if (changeset != null)
                    {
                        foreach (var rd in changeset)
                        {
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
                            var updatetask = client.PostAsync(querystring, new StringContent(((JObject)rd).ToString(), Encoding.UTF8, "application/json"));
                            updatetask.Wait();
                        }
                    }
                }
                catch (Exception ex)
                {

                }

            }//end of using

        }//end of method





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
        }
    }
}
