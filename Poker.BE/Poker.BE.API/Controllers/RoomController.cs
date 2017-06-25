﻿using Poker.BE.Service.Modules.Requests;
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
                result.Success = false;
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        public HttpResponseMessage CreateNewRoom(CreateNewRoomRequest request)
        {
            var result = new CreateNewRoomResult();
            try
            {
                result = service.CreateNewRoom(request);
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
        public HttpResponseMessage JoinNextHand(JoinNextHandRequest request)
        {
            var result = new JoinNextHandResult();

            try
            {
                result = service.JoinNextHand(request);
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
        public HttpResponseMessage StandUpToSpactate(StandUpToSpactateRequest request)
        {
            var result = new StandUpToSpactateResult();

            try
            {
                result = service.StandUpToSpactate(request);
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
        public HttpResponseMessage LeaveRoom(LeaveRoomRequest request)
        {
            var result = new LeaveRoomResult();

            try
            {
                result = service.LeaveRoom(request);
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
        public HttpResponseMessage FindRoomsByCriteria(FindRoomsByCriteriaRequest request)
        {
            var result = new FindRoomsByCriteriaResult();

            try
            {
                result = service.FindRoomsByCriteria(request);
            }
            catch (Exception e)
            {
                result.ErrorMessage = e.Message;
                result.Success = false;
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }


        [HttpGet]
        public HttpResponseMessage GetAllRooms()
        {
            var result = new FindRoomsByCriteriaResult();

            try
            {
                result = service.GetAllRooms();
            }
            catch (Exception e)
            {
                result.ErrorMessage = e.Message;
                result.Success = false;
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }


        [HttpGet]
        public HttpResponseMessage GetAllRoomsOfLeague(int leagueId)
        {
            var result = new FindRoomsByCriteriaResult();

            try
            {
                result = service.GetAllRoomsOfLeague(leagueId);
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
