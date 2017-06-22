using Poker.BE.Service.Modules.Requests;
using Poker.BE.Service.Modules.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Service.Services
{
    public class PokerGamePlayService : IServices.IPokerGamePlayService
    {
		public PlayMoveResult PlayMove(PlayMoveRequest request)
		{
			var result = new PlayMoveResult();
			return result;
		}
	}
}
