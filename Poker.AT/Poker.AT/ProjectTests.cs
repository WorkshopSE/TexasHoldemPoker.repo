using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AT.Bridge;
using AT.Domain;
using NUnitLite;

namespace AT.Tests
{
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

		internal bool ShuffleCards(Deck TestDeck)
		{
			return bridge.ShuffleCards(TestDeck);
		}

		public string UC2(String someParam)
        {
            return bridge.testCase2(someParam);
        }
    }
}
