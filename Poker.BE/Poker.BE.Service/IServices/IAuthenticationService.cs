using Poker.BE.Service.Modules.Requests;
using Poker.BE.Service.Modules.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Service.IServices
{
    /// <summary> Supports UCC01: Authentication & Profile </summary>
    /// <see cref="https://docs.google.com/document/d/1ob4bSynssE3UOfehUAFNv_VDpPbybhS4dW_O-v-QDiw/edit#heading=h.6eulaksypl6w"/>
    public interface IAuthenticationService
    {
		LoginResult Login(LoginRequest request);
		LogoutResult Logout(LogoutRequest request);
		SignUpResult SignUp(SignUpRequest request);

	}
}
