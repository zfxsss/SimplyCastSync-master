using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SimplyCastSync.DBAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyCastSync.DBAccess.Tests
{
    [TestClass()]
    public class SimplyCastWebApiQueryTests
    {
        [TestMethod()]
        public void GetDataTest()
        {
            var client = new SimplyCastWebApiQuery("UpdateAddress=https://app.simplycast.com;QueryAddress=https://app.simplycast.com;PublicKey=a8fe3712113090e5666a6b374452dc930d710553;SecretKey=7dc2feccd12ec26534a405c611f0134481c3394e;");
            //?q=api/call&request=contactmanager/lists/26
            //?q=api/call&request=contactmanager/lists
            //?q=api/call&request=contactmanager/lists/26/contacts&limit=25&offset=0&extended=1&system=1&sortField=id&sortOrder=asc
            //?q=api/call&request=contactmanager/lists/6/contacts
            var result = client.GetData("?q=api/call&request=contactmanager/lists/6/contacts");

            var o1 = result["contacts"].Where(x => x["id"].ToString() == "47862");
            var o2 = result.SelectToken("$..contacts[?(@id=='47862')]");
            var o3 = result.SelectToken("$.contacts[?(@..value=='khenry@advanstar.com' && @..name=='email')]");
            //var o4 = result.SelectTokens("");
            //result.SelectToken("$.contacts[?(@..value=='khenry@advanstar.com' && @..name=='email' && @..name == 'zip')]");

            var o5 = result.SelectToken("$.contacts[?(@..value=='khenry@advanstar.com' && @..name=='email' && @..name == 'zip')]");

            //add something to verify
            if (o3 == o5)
            {
                return;
            }

            Assert.Fail();
        }

        [TestMethod()]
        public void test_json()
        {
            var x  = JObject.Parse("{'book':[{'price':1,'name':'love'}]}");

            //var y = x.SelectToken("$.book[?(@.price=1)].name");

            var y = x.SelectToken("$.book[?(@.price==1)].name");
        }
    }
}