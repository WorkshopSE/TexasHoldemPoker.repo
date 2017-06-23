using Poker.BE.Service.Modules.Results;
using System.Collections.Generic;
using Poker.BE.Service.Modules.Requests;
using Poker.BE.Domain.Core;
using Poker.BE.Domain.Security;
using Poker.BE.CrossUtility.Exceptions;
using Poker.BE.Service.Modules.Caches;

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
                result.User = UserManager.LogIn(request.UserName, request.Password).UserName;
                result.Success = true;
            }
            catch (UserNotFoundException e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
            }
            catch (IncorrectPasswordException e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
            }
            return result;
        }



        public LogoutResult Logout(LogoutRequest request)
        {
            var result = new LogoutResult();

            User user;
            if (!Users.TryGetValue(request.User, out user))
            {
                result.Success = false;
                result.ErrorMessage = "User ID not found";
                return result;
            }

            try
            {
                result.Output = UserManager.LogOut(user);
                result.User = user.UserName;
                result.Success = true;
            }
            catch (UserNotFoundException e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
            }

            return result;
        }

        public SignUpResult SignUp(SignUpRequest request)
        {
            var result = new SignUpResult();

            try
            {
                User user = UserManager.AddUser(request.UserName, request.Password, request.Deposit);
                result.User = user.UserName;
                Users.Add(user.UserName, user);
                result.Success = true;
            }
            catch (UserNameTakenException e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
            }
            catch (IncorrectPasswordException e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
            }
            catch (InvalidPasswordException e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
            }
            catch (InvalidDepositException e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
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
