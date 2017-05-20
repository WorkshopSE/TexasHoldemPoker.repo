using Poker.BE.Service.IServices;
using Poker.BE.Service.Modules.Requests;
using Poker.BE.Service.Modules.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Poker.BE.API.Controllers
{
	public class AuthenticationController : ApiController
	{
		#region Fields
		private IAuthenticationService service;
		#endregion

		#region Constructors
		public AuthenticationController()
		{
			service = new Service.Services.AuthenticationService();
		}
		#endregion

		#region Methods
		[HttpPost]
		public HttpResponseMessage Login(LoginRequest request)
		{
			var result = default(LoginResult);

			try
			{
				result = service.Login(request);
			}
			catch (Exception e)
			{
				result.ErrorMessage = e.Message;
				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
			}

			return Request.CreateResponse(HttpStatusCode.OK, result);
		}
		public HttpResponseMessage Logout(LogoutRequest request)
		{
			var result = default(LogoutResult);

			try
			{
				result = service.Logout(request);
			}
			catch (Exception e)
			{
				result.ErrorMessage = e.Message;
				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
			}

			return Request.CreateResponse(HttpStatusCode.OK, result);
		}
		public HttpResponseMessage SignUp(SignUpRequest request)
		{
			var result = default(SignUpResult);

			try
			{
				result = service.SignUp(request);
			}
			catch (Exception e)
			{
				result.ErrorMessage = e.Message;
				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
			}

			return Request.CreateResponse(HttpStatusCode.OK, result);
		}
		#endregion
	}
}
