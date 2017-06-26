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

		public int testCase1(int someParam)
        {
            return -1;
        }

        public string testCase2(string someParam)
        {
            return "FAKE_HERE";
        }

		public void EditProfilePassword(User User, string Password)
		{
			
		}

		public void EditProfileEmail(User User, string Email)
		{
			
		}

		public Image EditProfileAvatar(Image TestUserImage)
		{
			return Image.FromFile("DefaultAvatar");
		}

		public void TearDown()
		{
		}

		public int CreateARoom(int level, string userName, int securityKey, out int player)
		{
			player = 0;
			return 0;
		}
	}
}
