using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Tests
{
    [TestFixture]
    [Category("Use Case 1")]
    public class TestUC1 : ProjectTests
    {
        [SetUp]
        public new void Setup()
        {
            base.Setup();
        }
        [Test]
        //Test impl HERE
        public void TestCase1()
        {
            int param = 5;
            Assert.AreEqual(base.UC1(param), 8);
        }
    }
}
