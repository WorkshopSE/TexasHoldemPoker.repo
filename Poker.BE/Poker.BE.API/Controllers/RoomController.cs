using Poker.BE.Service.Modules.Requests;
using Poker.BE.Service.Modules.Results;
using Poker.BE.Service.IServices;
using Poker.BE.Service.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Poker.BE.API.Controllers
{
    public class RoomController : ApiController
    {
        #region Fields
        private IRoomsService service;
        #endregion

        #region Constructors
        public RoomController()
        {
            service = new RoomsService();
        }
        #endregion

        #region Methods
        [HttpPost]
        public HttpResponseMessage EnterRoom(EnterRoomRequest request)
        {
            var result = default(EnterRoomResult);

            try
            {
                result = service.EnterRoom(request);
            }
            catch (Exception)
            {
                // TODO
                throw new NotImplementedException();
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        #endregion
    }
}
