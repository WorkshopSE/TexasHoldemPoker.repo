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
		public bool Logout(string UserName, int securityKey)
		{
			return bridge.Logout(UserName, securityKey);
		}
		public bool Login(string UserName, string Password, out int securityKey)
		{
			return bridge.Login(UserName, Password, out securityKey);
		}

		public IList<Card> ShuffleCards(Deck TestDeck)
		{
			return bridge.ShuffleCards(TestDeck);
		}

		public string SignUp(string Name, string UserName, string Password)
		{
			return bridge.SignUp(Name, UserName, Password);
		}

		public void TearDown()
		{
			bridge.TearDown();
		}

		public int CreateARoom(int level, string userName, int securityKey, out int? player)
		{
			return bridge.CreateARoom(level, userName, securityKey, out player);
		}

		public int EnterRoom(int room, string userName, int securityKey, out int? player)
		{
			return bridge.EnterRoom(room, userName, securityKey, out player);
		}

		public string GetProfile(string userName, int securityKey, out string password, out int[] avatar)
		{
			return bridge.GetProfile(userName, securityKey, out password, out avatar);
		}
		public bool EditProfilePassword(string userName, string oldPassword, string Password, int securityKey)
		{
			return bridge.EditProfilePassword(userName, oldPassword, Password, securityKey);
		}
		public bool EditProfileUserName(string userName, string newUserName, string password, int securityKey)
		{
			return bridge.EditProfileUserName(userName, newUserName, password, securityKey);
		}
		public double JoinNextHand(string userName, int key, int? player, int seatIndex, double buyIn, out double wallet)
		{
			return bridge.JoinNextHand(userName, key, player, seatIndex, buyIn, out wallet);
		}

		public bool StandUpToSpectate(string userName, int key, int? player)
		{
			return bridge.StandUpToSpectate(userName, key, player);
		}
	}
}
