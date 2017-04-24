using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimplyCastSync.PubLib.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyCastSync.PubLib.Tests
{
    [TestClass()]
    public class LogMgrTests
    {
        [TestMethod()]
        public void ExitConsoleLogTaskTest()
        {
            Console.WriteLine(LogMgr.ConsoleLogTask.Status);
            Assert.Fail();
        }

        [TestMethod()]
        public void ExitFileLogTaskTest()
        {
            Console.WriteLine(LogMgr.FileLogTask.Status);
            Assert.Fail();
        }

        [TestMethod()]
        public void ExitConsoleLogTaskTest1()
        {
            Assert.AreEqual(LogMgr.ConsoleLogTask.Status, TaskStatus.Running);
            LogMgr.ExitConsoleLogTask();
            LogMgr.ConsoleLogTask.Wait();
            Assert.AreEqual(LogMgr.ConsoleLogTask.Status, TaskStatus.RanToCompletion);
        }

        [TestMethod()]
        public void ExitFileLogTaskTest1()
        {
            var status = LogMgr.FileLogTask.Status;
            Assert.AreEqual(status, TaskStatus.WaitingToRun);
            LogMgr.ExitFileLogTask();
            LogMgr.FileLogTask.Wait();
            Assert.AreEqual(LogMgr.FileLogTask.Status, TaskStatus.RanToCompletion);
        }
    }
}