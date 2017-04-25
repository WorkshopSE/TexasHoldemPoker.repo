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
        public bool testCase1()
        {
            return bridge.testCase1();
        }

        public bool testCase2()
        {
            return bridge.testCase2();
        }
    }
}
