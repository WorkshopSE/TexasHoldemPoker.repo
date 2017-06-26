using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AT.Domain;
using Poker.BE.Service.Modules.Requests;
using Poker.BE.Service.IServices;
using Poker.BE.Service.Modules.Results;
using Poker.BE.Service.Services;

namespace AT.Bridge
{
    class Real : TestsBridge
    {
		#region Fields
		private IAuthenticationService AuthenticationService;
		private IRoomsService RoomService;
		#endregion

		#region Constructors
		public Real()
		{
			AuthenticationService = new AuthenticationService();
			RoomService = new RoomsService();
		}
		#endregion

		public bool Logout(string userName, int securityKey)
		{
			LogoutResult result = AuthenticationService.Logout(new LogoutRequest() {
				UserName = userName,
				SecurityKey = securityKey,
			});
			if (!result.Success.HasValue || !result.Success.Value)
			{
				return false;
			}
			return true;
		}
		public bool Login(string UserName, string Password, out int securityKey)
		{
			LoginResult result = AuthenticationService.Login(new LoginRequest() { UserName = UserName, Password = Password});
			if (!result.Success.HasValue || !result.Success.Value)
			{
				securityKey = result.SecurityKey;
				return false;
			}
			securityKey = result.SecurityKey;
			return true;
		}

		public IList<Card> ShuffleCards(Deck TestDeck)
		{
			return null;
			//throw new NotImplementedException();
		}

		public string SignUp(string Name, string UserName, string Password)
		{
			SignUpResult result = AuthenticationService.SignUp(new SignUpRequest() {UserName = UserName, Password = Password, Deposit = 100 });
			if(!result.Success.HasValue || !result.Success.Value)
			{
				return null;
			}
			return result.UserName;
		}

		//Here goes the Adapter implementation - for later use!
		public int testCase1(int someParam)
        {
			return 0;
            //throw new NotImplementedException();
        }

        public string testCase2(string someParam)
        {
			return null;
            //throw new NotImplementedException();
        }

		public void EditProfilePassword(User User, string Password)
		{
			//throw new NotImplementedException();
		}

		public void EditProfileEmail(User User, string Email)
		{
			//throw new NotImplementedException();
		}

		public Image EditProfileAvatar(Image TestUserImage)
		{
			return null;
			//throw new NotImplementedException();
		}
		public int CreateARoom(int level, string userName, int securityKey, out int player)
		{
			CreateNewRoomResult result = RoomService.CreateNewRoom(new CreateNewRoomRequest() { Level = level, UserName = userName, SecurityKey = securityKey });
			if(!result.Success.HasValue || !result.Success.Value)
			{
				string error = result.ErrorMessage;
				player = 0;
				return 0;
			}
			player = result.Player;
			return result.Room;
		}

		public void TearDown()
		{
			((AuthenticationService)AuthenticationService).Clear();
			((RoomsService)RoomService).Clear();
		}
	}
}
