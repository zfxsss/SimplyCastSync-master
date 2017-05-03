using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimplyCastSync.DBAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyCastSync.DBAccess.Tests
{
    [TestClass()]
    public class FoxproDataQueryTests
    {
        [TestMethod()]
        public void GetDataTest()
        {
            var foxquery = new FoxproDataQuery("Provider=VFPOLEDB.1;Data Source=C:\\v2k\\Data;");
            var result = foxquery.GetData("select  a.ADDR1, a.ADDR2, a.ADDR3, a.EMAIL, a.HPHONE, a.MOBILE, a.POSTCODE, a.STATE, a.TITLE, a.OCCUPT, a.SURNAME, a.GIVEN_NAME, a.POSTADDR1, a.POSTADDR2, a.POSTADDR3, a.POSTSTATE, a.POSTPCODE, a.SEX, a.WPHONE, a.PHONE3, a.POSTNAME, a.BIRTHDAY, a.FIRSTVISIT, a.LASTRCLET, a.LASTRECALL, a.LASTVISIT, a.NEXTRCLET, a.NEXTRECALL, a.RECALL, a.RECALLSENT, a.FPOSTADDR, a.RCPERIOD, a.REFNUM, b.BOOKDATE, b.STARTTIME, b.STATUS, c.PAMOUNT from VPATIENT a left join (select k.BOOKDATE, k.STARTTIME, k.REFNUM, k.STATUS from (select max(BOOKDATE) as BOOKDATE, REFNUM from VAPPOINT group by REFNUM) h inner join VAPPOINT k on h.BOOKDATE = k.BOOKDATE and h.REFNUM = k.REFNUM) b on a.REFNUM = b.REFNUM left join (SELECT n.REFNUM, SUM(n.PAMOUNT) as PAMOUNT FROM (SELECT MAX(ACCDATE) as ACCDATE,REFNUM from VACCOUNT GROUP BY REFNUM) m inner join VACCOUNT n on m.ACCDATE = n.ACCDATE and m.REFNUM = n.REFNUM GROUP BY n.REFNUM) c on a.REFNUM = c.REFNUM where (!EMPTY(TRIM(a.SURNAME)) OR !EMPTY(TRIM(a.GIVEN_NAME))) order by a.REFNUM");

            var oo =  result.Tables[0].Select("REFNUM = 1");

            Assert.Fail();
        }

        [TestMethod()]
        public void GetDataTest2()
        {
            var foxquery = new FoxproDataQuery("Provider=VFPOLEDB.1;Data Source=C:\\v2k\\Data;");
            var result = foxquery.GetData("SELECT * FROM vpatient a INNER JOIN vprecall b ON a.Refnum = b.Refnum");

            var oo = result.Tables[0].Select("REFNUM = 1");
        }
    }
}