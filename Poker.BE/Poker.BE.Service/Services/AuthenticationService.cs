using Poker.BE.Service.Modules.Results;
using System.Collections.Generic;
using Poker.BE.Service.Modules.Requests;
using Poker.BE.Domain.Core;
using Poker.BE.Domain.Security;
using Poker.BE.CrossUtility.Exceptions;
using Poker.BE.Service.Modules.Caches;
using Poker.BE.CrossUtility.Loggers;

namespace Poker.BE.Service.Services
{
    public class AuthenticationService : IServices.IAuthenticationService
    {
        #region Fields
        private CommonCache _cache;
        #endregion

        #region properties
        public IDictionary<string, User> Users { get { return _cache.Users; } }
        public UserManager UserManager { get { return _cache.UserManager; } }
        public ILogger Logger { get { return CrossUtility.Loggers.Logger.Instance; } }
        // TODO: idan - add logger calls.
        #endregion

        #region Constructors
        public AuthenticationService()
        {
            _cache = CommonCache.Instance;
        }
        #endregion

        #region Methods
        public LoginResult Login(LoginRequest request)
        {
            var result = new LoginResult();

            try
            {
                var user = UserManager.Login(request.UserName, request.Password);
                result.UserName = user.UserName;
                result.SecurityKey = user.SecurityKey.Value;
                result.UserBank = user.UserBank.Money;
                result.Success = true;
            }
            catch (PokerException e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
                Logger.Error(e, this);
            }

            return result;
        }

        public LogoutResult Logout(LogoutRequest request)
        {
            var result = new LogoutResult();
            try
            {
                var user = _cache.RefreshAndGet(
                    Users,
                    request.UserName,
                    new UserNotFoundException(string.Format("User Name: {0} not found", request.UserName))
                    );

                UserManager.SecurityCheck(request.SecurityKey, user);

                result.Output = UserManager.Logout(user);
                result.UserName = user.UserName;
                result.Success = true;
            }
            catch (PokerException e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
                Logger.Error(e, this);
            }

            return result;
        }

        public SignUpResult SignUp(SignUpRequest request)
        {
            var result = new SignUpResult();

            try
            {
                User user = UserManager.AddUser(request.UserName, request.Password, request.Deposit);
                result.UserName = user.UserName;
                Users.Add(user.UserName, user);
                result.Success = true;
            }
            catch (PokerException e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
                Logger.Error(e, this);
            }
            return result;
        }

        public void Clear()
        {
            Users.Clear();
            UserManager.Clear();
        }
        #endregion
    }
}
