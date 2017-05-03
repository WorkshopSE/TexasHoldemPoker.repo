using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AT.Domain;

namespace AT.Bridge
{
    class Stub : TestsBridge
    {
		public IList<Card> ShuffleCards(Deck TestDeck)
		{
			return new List<Card>();
		}

		public int testCase1(int someParam)
        {
            return -1;
        }

        public string testCase2(string someParam)
        {
            return "FAKE_HERE";
        }
    }
}
