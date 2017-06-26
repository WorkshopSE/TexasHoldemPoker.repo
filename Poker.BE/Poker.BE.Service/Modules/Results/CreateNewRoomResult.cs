﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Service.Modules.Results
{
	public class CreateNewRoomResult : IResult
	{
		public int Room { get; set; }   //room hash code
		public int Player { get; set; } //player hash code 

        // Request's Game Preferences info
        public string Name { get; set; }
        public double BuyInCost { get; set; }
        public double MinimumBet { get; set; }
        public double Antes { get; set; }
        public int MinNumberOfPlayers { get; set; }
        public int MaxNumberOfPlayers { get; set; }
        public bool IsSpactatorsAllowed { get; set; }

        public double Limit { get; set; }       //this is for decorator (PotLimit/Limit game mode)
    }
}
