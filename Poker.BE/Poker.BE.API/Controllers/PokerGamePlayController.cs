using Poker.BE.Service.Modules.Requests;
using Poker.BE.Service.Modules.Results;
using Poker.BE.Service.IServices;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Poker.BE.Service.Services;

namespace Poker.BE.API.Controllers
{
    public class PokerGamePlayController : ApiController
    {
        #region Fields
        private IPokerGamePlayService service;
        #endregion

        #region Constructors
        public PokerGamePlayController()
        {
            service = new Service.Services.PokerGamePlayService();
        }
        #endregion

        #region Methods
        [HttpPost]
        public HttpResponseMessage ChoosePlayMove(ChoosePlayMoveRequest request)
        {
            var result = default(ChoosePlayMoveResult);

            try
            {
                result = service.ChoosePlayMove(request);
            }
            catch (Exception e)
            {
                result.ErrorMessage = e.Message;
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        public HttpResponseMessage StartNewHand(StartNewHandRequest request)
        {
            var result = default(StartNewHandResult);

            try
            {
                result = service.StartNewHand(request);
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