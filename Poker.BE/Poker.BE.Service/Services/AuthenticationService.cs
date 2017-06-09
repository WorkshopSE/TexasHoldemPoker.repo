using Poker.BE.Service.Modules.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poker.BE.Service.Modules.Requests;
using Poker.BE.Domain.Core;
using Poker.BE.Domain.Utility.Exceptions;
using Poker.BE.Domain.Security;
using Poker.BE.Domain.Utility.Logger;

namespace Poker.BE.Service.Services
{
    public class AuthenticationService : IServices.IAuthenticationService
    {
        #region Fields
        public UserManager userManager;
        #endregion

        #region properties
        public IDictionary<int, User> Users { get; set; }
        #endregion

        #region Constructors
        public AuthenticationService()
        {
            userManager = UserManager.Instance;
            Users = new Dictionary<int, User>();
        }
        #endregion

        #region Methods
        public LoginResult Login(LoginRequest request)
        {
            var result = new LoginResult();
            try
            {
                result.User = userManager.LogIn(request.UserName, request.Password).GetHashCode();
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
            }

            try
            {
                result.Output = userManager.LogOut(user);
                result.User = user.GetHashCode();
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
                User user = userManager.AddUser(request.UserName, request.Password, request.Deposit);
                result.User = user.GetHashCode();
                Users.Add(user.GetHashCode(), user);
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
            userManager.Clear();
        }
        #endregion
    }
}
