using Poker.BE.Service.Modules.Requests;
using Poker.BE.Service.Modules.Results;
using Poker.BE.Service.IServices;

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

		#region Properties
		public IRoomsService Service { get { return service; } }
		#endregion

		#region Constructors
		public RoomController()
		{
			service = new Service.Services.RoomsService();
		}
		#endregion

		#region Methods
		[HttpPost]
		public HttpResponseMessage EnterRoom(EnterRoomRequest request)
		{
			var result = new EnterRoomResult();

			try
			{
				result = service.EnterRoom(request);
			}
			catch (Exception e)
			{
				result.ErrorMessage = e.Message;
				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
			}

			return Request.CreateResponse(HttpStatusCode.OK, result);
		}
		[HttpPost]
		public HttpResponseMessage CreateNewRoom(CreateNewRoomRequest request)
		{
			var result = default(CreateNewRoomResult);
			try
			{
				result = service.CreateNewRoom(request);
			}
			catch (Exception e)
			{
				result.ErrorMessage = e.Message;
				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
			}
			return Request.CreateResponse(HttpStatusCode.OK, result);
		}
		#endregion
	}
}
