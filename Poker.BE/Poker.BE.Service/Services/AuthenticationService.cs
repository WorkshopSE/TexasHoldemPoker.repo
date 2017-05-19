using Poker.BE.Service.Modules.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poker.BE.Service.Modules.Requests;

namespace Poker.BE.Service.Services
{
    public class AuthenticationService : IServices.IAuthenticationService
    {
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
			return result;
		}
	}
}
