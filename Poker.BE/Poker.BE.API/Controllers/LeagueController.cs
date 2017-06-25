using Poker.BE.Service.IServices;
using Poker.BE.Service.Modules.Results;
using Poker.BE.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Poker.BE.API.Controllers
{
    public class LeagueController : ApiController
    {
        #region Fields
        private ILeaguesService service;

        #endregion

        #region Constructors
        public LeagueController()
        {
            service = new LeagueService();
        }

        #endregion

        #region Methods

        [HttpPost]
        public HttpResponseMessage GetAllLeagues(string userName)
        {
            var result = new LeaguesResult();

            try
            {
                result = service.GetAllLeagues(userName);
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
