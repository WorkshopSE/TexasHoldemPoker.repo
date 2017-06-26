﻿namespace Poker.BE.Service.Modules.Requests
{
    public class EnterRoomRequest : IRequest
    {
        /// <summary>
        /// requested room hash code
        /// </summary>
        public int Room { get; set; }
    }
}