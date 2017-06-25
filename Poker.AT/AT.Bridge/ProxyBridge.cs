using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AT.Domain;

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
		public bool Logout(string UserName, string Password)
		{
			return bridge.Logout(UserName, Password);
		}
		public bool Login(string UserName, string Password)
		{
			return bridge.Login(UserName,Password);
		}

		public IList<Card> ShuffleCards(Deck TestDeck)
		{
			return bridge.ShuffleCards(TestDeck);
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

		public string SignUp(string Name, string UserName, string Password)
		{
			return bridge.SignUp(Name, UserName, Password);
		}

		public void EditProfilePassword(User User, string Password)
		{
			bridge.EditProfilePassword(User,Password);
		}

		public void EditProfileEmail(User User, string Email)
		{
			bridge.EditProfileEmail(User, Email);
		}

		public Image EditProfileAvatar(Image TestUserImage)
		{
			return bridge.EditProfileAvatar(TestUserImage);
		}

		public void TearDown()
		{
			bridge.TearDown();
		}
	}
}
