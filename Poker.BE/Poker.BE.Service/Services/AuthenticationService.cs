using Poker.BE.Service.Modules.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Service.Services
{
    public class AuthenticationService : IServices.IAuthenticationService
    {
		public LoginResult Login(LoginRequest request)
		{
			var result = default(LoginResult);
			return result;
		}

	}
}
