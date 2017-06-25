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
    public class ProfileController : ApiController
    {
		#region Fields
		private IProfileService service;
		#endregion

		#region Properties
		public IProfileService Service { get { return service; } }
		#endregion

		#region Constructors
		public ProfileController()
		{
			service = new Service.Services.ProfileService();
		}

		#endregion

		#region Methods
		[HttpPost]
		public HttpResponseMessage EditProfile(EditProfileRequest request)
		{
			var result = new EditProfileResult();

			try
			{
				result = service.EditProfile(request);
			}
			catch (Exception e)
			{
				result.ErrorMessage = e.Message;
				result.Success = false;
				return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
			}

			return Request.CreateResponse(HttpStatusCode.OK, result);
		}

		[HttpPost]
		public HttpResponseMessage GetProfile(GetProfileRequest request)
		{
			var result = new GetProfileResult();

			try
			{
				result = service.GetProfile(request);
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
