using AT.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Bridge
{
    public interface TestsBridge
    {
        //Add Here The UC Functions to test
        //Example:
        int testCase1(int someParam);
        string testCase2(string someParam);
		bool ShuffleCards(Deck TestDeck);
	}
}
