using System;
using Poker.BE.Service.IServices;
using Poker.BE.Service.Modules.Requests;
using Poker.BE.Service.Modules.Results;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Poker.BE.API.Controllers
{
    public class PokerGamePlayController : ApiController
    {
        #region Fields
        private IPokerGamePlayService service;
        #endregion

        #region Properties
        public IPokerGamePlayService Service { get { return service; } }
        #endregion

        #region Constructors
        public PokerGamePlayController()
        {
            service = new Service.Services.PokerGamePlayService();
        }

        #endregion

        #region Methods
        [HttpPost]
        public HttpResponseMessage PlayMove(PlayMoveRequest request)
        {
            var result = new PlayMoveResult();

            try
            {
                result = service.PlayMove(request);
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
