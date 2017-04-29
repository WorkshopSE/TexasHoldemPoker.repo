using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker.BE.Domain.Utility.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Utility.Logger.Tests
{
    [TestClass]
    public class LoggerTests
    {
        public TestContext TextContext { get; set; }
        private static ILogger logger;

        [ClassInitialize]
        public static void BeforeClass(TestContext tc)
        {
            logger = Logger.Instance;
            ((Logger)logger).LogMemory = new string[] { "" };
        }

        [TestInitialize]
        public void Before()
        {
            // testing our custom logger
            logger = Logger.Instance;
        }

        // testing our custom logger - private helper function
        private string[] GetLogMemory(ILogger logger)
        {
            if (logger is Logger) return GetLogMemory(logger as Logger);
            return null;
        }

        private string[] GetLogMemory(Logger logger)
        {
            return logger.LogMemory;
        }

        private string LogTestSuffix()
        {
            return ((Logger)logger).LogSuffix();
        }

        private string LogTestPrefix(string name, string priority = "Low")
        {
            return ((Logger)logger).LogPrefix(name, this, priority);
            /*
            return
                "[" + DateTime.Now.ToShortDateString() + " "
                + DateTime.Now.ToLongTimeString() + "]"
                + "\t" + priority + "\t\t"
                + "\t" + name + "\t\t"
                + "<" + this.GetType().FullName + "> :\t";
                */
        }

        [TestCleanup]
        public void After()
        {
            logger = null;
        }

        [TestMethod]
        public void DebugTest()
        {
            //Arrange
            string message = "test";
            string[] expected = new string[] { LogTestPrefix("Debug") + message + LogTestSuffix() };

            //Act
            logger.Debug(message, this);
            string[] actual = GetLogMemory(logger);

            //assert
            Assert.AreEqual<string>(expected[0], actual[actual.Length - 1]);
        }



        [TestMethod]
        public void ErrorTest()
        {
            //Arrange
            string message = "test";
            string[] expected = new string[] { LogTestPrefix("Error", "High") + message + LogTestSuffix() };

            //Act
            logger.Error(message, this);
            string[] actual = GetLogMemory(logger);

            //assert
            Assert.AreEqual<string>(expected[0], actual[actual.Length - 1]);
        }

        [TestMethod]
        public void InfoTest()
        {
            //Arrange
            string message = "test";
            string[] expected = new string[] { LogTestPrefix("Info") + message + LogTestSuffix() };

            //Act
            logger.Info(message, this);
            string[] actual = GetLogMemory(logger);

            //assert
            Assert.AreEqual<string>(expected[0], actual[actual.Length - 1]);
        }

        [TestMethod]
        public void LogTest()
        {
            //Arrange
            string message = "test";
            string[] expected = new string[] { LogTestPrefix("Logging") + message + LogTestSuffix() };

            //Act
            logger.Log(message, this);
            string[] actual = GetLogMemory(logger);

            //assert
            Assert.AreEqual<string>(expected[0], actual[actual.Length - 1]);
        }

        [TestMethod]
        public void WarnTest()
        {
            //Arrange
            string message = "test";
            string[] expected = new string[] { LogTestPrefix("Warning", "Medium") + message + LogTestSuffix() };

            //Act
            logger.Warn(message, this);
            string[] actual = GetLogMemory(logger);

            //assert
            Assert.AreEqual<string>(expected[0], actual[actual.Length - 1]);
        }

        [TestMethod()]
        public void ErrorTest1()
        {
            //Arrange
            Exception e = new Exception("exception message");
            string message =
                LogTestPrefix("Exception Error", "Critical") + "\n"
                   + "Message: " + e.Message + "\n"
                   + "Source: " + e.Source + "\n"
                   + "Help Link: " + e.HelpLink + LogTestSuffix();
            string expected =
                LogTestPrefix("Exception Error") +
                message +
                LogTestSuffix();

            //Act
            logger.Error(e, this, "Critical");
            string[] memo = GetLogMemory(logger);
            string actual =
                memo[memo.Length - 1] +
                memo[memo.Length - 2] +
                memo[memo.Length - 3] +
                memo[memo.Length - 4];

            //Assert
            Assert.AreEqual<string>(expected, actual);
        }
    }
}