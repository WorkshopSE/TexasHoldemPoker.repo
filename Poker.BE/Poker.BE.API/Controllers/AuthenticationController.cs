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
    //TODO @idan add logger
    public class AuthenticationController : ApiController
    {
        #region Fields
        private IAuthenticationService service;
        #endregion

        #region Properties
        public IAuthenticationService Service { get { return service; } }
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
            var result = new LoginResult();

            try
            {
                result = service.Login(request);
            }
            catch (Exception e)
            {
                result.ErrorMessage = e.Message;
                result.Success = false;
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        public HttpResponseMessage Logout(LogoutRequest request)
        {
            var result = new LogoutResult();

            try
            {
                result = service.Logout(request);
            }
            catch (Exception e)
            {
                result.ErrorMessage = e.Message;
                result.Success = false;
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        public HttpResponseMessage SignUp(SignUpRequest request)
        {
            var result = new SignUpResult();

            try
            {
                result = service.SignUp(request);
            }
            catch (Exception e)
            {
                result.ErrorMessage = e.Message;
                result.Success = false;
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        #endregion
    }
}
