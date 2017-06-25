using Poker.BE.CrossUtility.Exceptions;
using Poker.BE.CrossUtility.Loggers;
using Poker.BE.Domain.Core;
using Poker.BE.Domain.Security;
using Poker.BE.Service.Modules.Caches;
using Poker.BE.Service.Modules.Requests;
using Poker.BE.Service.Modules.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Service.Services
{
	public class ProfileService : IServices.IProfileService
	{
		#region Fields
		private CommonCache _cache;
		#endregion

		#region properties
		public IDictionary<string, User> Users { get { return _cache.Users; } }
		public ILogger Logger { get { return CrossUtility.Loggers.Logger.Instance; } }
		public UserManager UserManager { get { return _cache.UserManager; } }

		#endregion

		#region Constructors
		public ProfileService()
		{
			_cache = CommonCache.Instance;
		}

		public void Clear()
		{
			Users.Clear();
			UserManager.Clear();
		}
		#endregion

		public EditProfileResult EditProfile(EditProfileRequest request)
		{
			var result = new EditProfileResult();
			result.newUserName = request.UserName;
			try
			{
                // call domain action
				UserManager.EditProfile(request.UserName, request.NewUserName ?? request.UserName, request.NewPassword ?? request.Password, request.NewAvatar);

                // update result

                // parse byte[] to JSON (using int[] as a cleaver KOMBINA)
				result.newAvatar = request.NewAvatar.Select(b => (int)b).ToArray();

				result.newPassword = request.NewPassword;
				result.newUserName = request.NewUserName;
				result.Success = true;
			}
            catch(PokerException e)
			{
				result.Success = false;
				result.ErrorMessage = e.Message;
                Logger.Error(e, this);
			}
			return result;
		}

		public GetProfileResult GetProfile(GetProfileRequest request)
		{
			var result = new GetProfileResult();
			try
			{
				String password;
				byte[] avatar;
				UserManager.GetProfile(request.UserName, out password, out avatar);
				result.Avatar = avatar;
				result.Password = password;
				result.UserName = request.UserName;
				result.Success = true;
			}
			catch (UserNotFoundException e)
			{
				result.Success = false;
				result.ErrorMessage = e.Message;
				Logger.Log(e.Message, this);
			}
			return result;
		}
    }
}
