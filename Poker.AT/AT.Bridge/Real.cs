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
		#endregion

		#region Constructors
		public Real()
		{
			AuthenticationService = new Poker.BE.Service.Services.AuthenticationService();
		}
		#endregion

		public bool Logout(string UserName, string Password)
		{
			LogoutResult result = AuthenticationService.Logout(new LogoutRequest() { User = UserName });
			if (!result.Success.HasValue || !result.Success.Value)
			{
				return false;
			}
			return true;
		}
		public bool Login(string UserName, string Password)
		{
			LoginResult result = AuthenticationService.Login(new LoginRequest() { UserName = UserName, Password = Password});
			if (!result.Success.HasValue || !result.Success.Value)
			{
				return false;
			}
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
			return result.User;
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

		public void TearDown()
		{
			((AuthenticationService)AuthenticationService).Clear();
		}
	}
}
