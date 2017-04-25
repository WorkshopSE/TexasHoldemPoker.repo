using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AT.Bridge;

namespace AT.Tests
{
    [TestFixture]
    public class ProjectTests
    {
        protected TestsBridge bridge;
        [SetUp]
        public void Setup()
        {
            this.bridge = Driver.getBridge();
        }
        public int UC1(int someParam)
        {
            return bridge.testCase1(someParam);
        }
        public string UC2(String someParam)
        {
            return bridge.testCase2(someParam);
        }
    }
}
