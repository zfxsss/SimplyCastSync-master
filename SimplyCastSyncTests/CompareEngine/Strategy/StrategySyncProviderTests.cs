using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SimplyCastSync.CompareEngine.Strategy;
using SimplyCastSync.DBAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SimplyCastSync.CompareEngine.ComparerBuilder;
using static SimplyCastSync.Config.ConfigRepository;

namespace SimplyCastSync.CompareEngine.Strategy.Tests
{
    [TestClass()]
    public class StrategySyncProviderTests
    {
        [TestMethod()]
        public void GetStrategySyncTest()
        {
            //var foxquery = new FoxproDataQuery("Provider=VFPOLEDB.1;Data Source=C:\\Users\\zhefeng\\Documents\\Foxpro;");
            //var client = new SimplyCastWebApiQuery("BaseAddress=https://app.simplycast.com;PublicKey=a8fe3712113090e5666a6b374452dc930d710553;SecretKey=7dc2feccd12ec26534a405c611f0134481c3394e;");
            var pairsconfig = Content["pairs"];

            foreach (var pair in pairsconfig)
            {
                var src = pair["source"] as JObject;
                var dest = pair["destination"] as JObject;


                var comparer = CB.GetComparer<DataSet, JObject>((JObject)pair);
                comparer.StrategySync(comparer);
            }

            //new JsonComparer<DataSet,JObject>(foxquery, client, )

            Assert.Fail();
        }
    }
}