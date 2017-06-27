using AT.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AT.Bridge
{
    public interface TestsBridge
    {
		IList<Card> ShuffleCards(Deck TestDeck);
		bool Login( string UserName, string Password, out int securityKey);
		string SignUp(string Name, string UserName, string Password);
		void TearDown();
		bool Logout(string UserName, int securityKey);
		int CreateARoom(int level, string userName, int securityKey, out int? player);
		int EnterRoom(int room, string userName, int securityKey, out int? player);
		bool EditProfilePassword(string userName, string oldPassword, string Password, int securityKey);
		bool EditProfileUserName(string userName, string newUserName, string password, int securityKey);
		string GetProfile(string userName, int securityKey, out string password, out int[] avatar);
		double JoinNextHand(string userName, int key, int? player, int seatIndex, double buyIn, out double wallet);
		bool StandUpToSpectate(string userName, int securityKey, int? player);
	}
}
