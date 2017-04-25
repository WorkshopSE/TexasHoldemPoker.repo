using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Tests
{
    [TestFixture]
    public class TestUC2 : ProjectTests
    {
        [SetUp]
        public new void Setup()
        {
            base.Setup();
        }
        [Test]
        public void TestCase2()
        {
            string param = "testSomething";
            Assert.AreEqual(base.UC2(param), "NOT_FAKE");
        }
    }
}
