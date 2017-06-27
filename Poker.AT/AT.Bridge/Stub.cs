using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AT.Domain;

namespace AT.Bridge
{
    class Stub : TestsBridge
    {
		public bool Logout(string UserName, int securityKey)
		{
			return false;
		}
		public bool Login(string UserName, string Password, out int securityKey)
		{
			securityKey = 0;
			return false;
		}

		public IList<Card> ShuffleCards(Deck TestDeck)
		{
			return new List<Card>();
		}

		public string SignUp(string Name, string UserName, string Password)
		{
			return UserName;
		}

		public string GetProfile(string userName, int securityKey, out string password, out int[] avatar)
		{
			password = null;
			avatar = null;
			return null;
		}

		public bool EditProfilePassword(string userName, string oldPassword, string Password, int securityKey)
		{
			return false;
		}

		public bool EditProfileUserName(string userName, string newUserName, string password, int securityKey)
		{
			return false;
		}

		public void TearDown()
		{
		}

		public int CreateARoom(int level, string userName, int securityKey, out int? player)
		{
			player = 0;
			return 0;
		}

		public int EnterRoom(int room, string userName, int securityKey, out int? player)
		{
			player = 0;
			return 0;
		}
	}
}
