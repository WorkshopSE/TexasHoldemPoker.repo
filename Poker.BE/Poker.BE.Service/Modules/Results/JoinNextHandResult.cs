﻿namespace Poker.BE.Service.Modules.Results
{
    public class JoinNextHandResult : IResult
    {
        public double UserBank { get; set; }
        public double Wallet { get; set; }
    }
}