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
    public class HistoryController : ApiController
    {
        #region Fields
        private IHistoryService service;
        #endregion

        #region Properties
        public IHistoryService Service { get { return service; } }
        #endregion

        #region Constructors
        public HistoryController()
        {
            service = new Service.Services.HistoryService();
        }
        #endregion

        [HttpPost]
        public HttpResponseMessage GetStatistics(GetStatisticsRequest request)
        {
            var result = new GetStatisticsResult();

            try
            {
                result = service.GetStatistics(request);
            }
            catch (Exception e)
            {
                result.ErrorMessage = e.Message;
                result.Success = false;
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
