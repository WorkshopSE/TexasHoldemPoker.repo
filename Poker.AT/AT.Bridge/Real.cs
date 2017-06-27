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
using Poker.BE.Service.Modules.Caches;

namespace AT.Bridge
{
	class Real : TestsBridge
	{
		#region Fields
		private IAuthenticationService authenticationService;
		private IRoomsService roomService;
		private IProfileService profileService;
		private ICache _cache;
		#endregion

		#region Constructors
		public Real()
		{
			authenticationService = new AuthenticationService();
			profileService = new ProfileService();
			roomService = new RoomsService();
			_cache = CommonCache.Instance;
		}
		#endregion

		public bool Logout(string userName, int securityKey)
		{
			LogoutResult result = authenticationService.Logout(new LogoutRequest()
			{
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
			LoginResult result = authenticationService.Login(new LoginRequest() { UserName = UserName, Password = Password });
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
			SignUpResult result = authenticationService.SignUp(new SignUpRequest() { UserName = UserName, Password = Password, Deposit = 100 });
			if (!result.Success.HasValue || !result.Success.Value)
			{
				return null;
			}
			return result.UserName;
		}

		public bool EditProfilePassword(string userName, string oldPassword, string Password, int securityKey)
		{
			EditProfileResult result = profileService.EditProfile(new EditProfileRequest() { UserName = userName, NewUserName = userName, Password = oldPassword, NewPassword = Password, NewAvatar = null, SecurityKey = securityKey });
			if (!result.Success.HasValue || !result.Success.Value)
			{
				string e = result.ErrorMessage;
				return false;
			}
			return true;
		}

		public bool EditProfileUserName(string userName, string newUserName, string password, int securityKey)
		{
			EditProfileResult result = profileService.EditProfile(new EditProfileRequest() { UserName = userName, NewUserName = newUserName, Password = password, SecurityKey = securityKey, NewAvatar = null });
			if(!result.Success.HasValue || !result.Success.Value)
			{
				return false;
			}
			return true;
		}

		public string GetProfile(string userName, int securityKey, out string password, out int[] avatar)
		{
			GetProfileResult result = profileService.GetProfile(new GetProfileRequest() { UserName = userName, SecurityKey = securityKey });
			if (!result.Success.HasValue || !result.Success.Value)
			{
				password = null;
				avatar = null;
				return null;
			}
			password = result.Password;
			avatar = result.Avatar;
			return result.UserName;
		}
		public int EnterRoom(int room, string userName, int securityKey, out int? player )
		{
			EnterRoomResult result = roomService.EnterRoom(new EnterRoomRequest() { Room = room, UserName = userName, SecurityKey = securityKey });
			if(!result.Success.HasValue || !result.Success.Value)
			{
				player = 0;
				return 0;
			}
			player = result.Player;
			return result.RoomID;
		}
		public int CreateARoom(int level, string userName, int securityKey, out int? player)
		{
			CreateNewRoomResult result = roomService.CreateNewRoom(new CreateNewRoomRequest() { Level = level, UserName = userName, SecurityKey = securityKey });
			if (!result.Success.HasValue || !result.Success.Value)
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
			((AuthenticationService)authenticationService).Clear();
			((RoomsService)roomService).Clear();
		}
	}
}
