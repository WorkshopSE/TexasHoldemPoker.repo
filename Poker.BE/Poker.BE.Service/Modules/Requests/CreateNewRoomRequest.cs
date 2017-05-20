﻿using Poker.BE.Domain.Game;

namespace Poker.BE.Service.Modules.Requests
{
    public class CreateNewRoomRequest : IRequest
    {
        public int Level { get; set; }
        public GameConfig GameConfig { get; set; }
    }
}