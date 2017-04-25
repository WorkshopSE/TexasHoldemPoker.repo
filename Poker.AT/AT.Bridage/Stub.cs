using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Bridge
{
    class Stub : TestsBridge
    {
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
