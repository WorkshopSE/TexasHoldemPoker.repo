using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AT.Bridge
{
    class ProxyBridge : TestsBridge
    {
        private TestsBridge bridge;
        public ProxyBridge()
        {
            bridge = null;
        }
        public void setRealBridge(TestsBridge implementation)
        {
            if (bridge == null)
                bridge = implementation;
        }

        //Implementation Example:
        public int testCase1(int someParam)
        {
            return bridge.testCase1(someParam);
        }

        public string testCase2(string someParam)
        {
            return bridge.testCase2(someParam);
        }
    }
}
