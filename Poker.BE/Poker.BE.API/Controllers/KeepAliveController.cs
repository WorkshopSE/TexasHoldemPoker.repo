using System;
using Poker.BE.Service.IServices;
using Poker.BE.Service.Modules.Requests;
using Poker.BE.Service.Modules.Results;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Poker.BE.API.Controllers
{
    public class KeepAliveController : ApiController
    {
        #region Fields
        private IKeepAliveService service;
        #endregion

        #region Properties
        public IKeepAliveService Service { get { return service; } }
        #endregion

        #region Constructors
        public KeepAliveController()
        {
            service = new Service.Services.KeepAliveService();
        }

        #endregion

        #region Methods
        [HttpGet]
        public HttpResponseMessage KeepAlive(KeepAliveRequest request)
        {
            var result = new KeepAliveResult();

            try
            {
                result = service.KeepAlive(request);
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
