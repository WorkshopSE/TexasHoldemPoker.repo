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

namespace Poker.BE.Service.Services
{
    public class AuthenticationService : IServices.IAuthenticationService
    {
		public UserManager Manager { get; set; }
		public LoginResult Login(LoginRequest request)
		{
			var result = default(LoginResult);
			return result;
		}
		public LogoutResult Logout(LogoutRequest request)
		{
			var result = default(LogoutResult);
			return result;
		}

		public SignUpResult SignUp(SignUpRequest request)
		{
			var result = default(SignUpResult);
			result = new SignUpResult()
			{
				User = Manager.LogIn(request.UserName, request.Password).GetHashCode(),
			};
			return result;
		}
	}
}
