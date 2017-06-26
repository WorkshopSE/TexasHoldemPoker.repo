using Poker.BE.CrossUtility.Exceptions;
using Poker.BE.Domain.Core;
using Poker.BE.Domain.Game;
using Poker.BE.Domain.Security;
using Poker.BE.Domain.Utility;
using Poker.BE.Service.Modules.Caches;
using Poker.BE.Service.Modules.Requests;
using Poker.BE.Service.Modules.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poker.BE.CrossUtility.Loggers;

namespace Poker.BE.Service.Services
{
    public class PokerGamePlayService : IServices.IPokerGamePlayService
    {
        #region Fields
        private CommonCache _cache;
        #endregion

        #region properties
        public IDictionary<string, User> Users { get { return _cache.Users; } }
        public UserManager UserManager { get { return _cache.UserManager; } }
        public GameCenter GameCenter { get { return _cache.GameCenter; } }

        public ILogger Logger { get { return CrossUtility.Loggers.Logger.Instance; } }


        #endregion

        #region Constructors
        public PokerGamePlayService()
        {
            _cache = CommonCache.Instance;
            //UserManager = UserManager.Instance;
            //Users = new Dictionary<string, User>();
        }
        #endregion

        public PlayMoveResult PlayMove(PlayMoveRequest request)
		{
			var result = new PlayMoveResult();
            try
            {
                User user = _cache.GetSecuredUser(request.SecurityKey, request.UserName);
                
                //getting the acting player
                Player player = null;
                foreach (Player p in user.Players)
                {
                    if (p.GetHashCode() == request.Player)
                    {
                        player = p;
                        break;
                    }
                }
                if (player == null)
                {
                    throw new PlayerNotFoundException("Player not found in user's players list");
                }

                user.ChoosePlayMove(player, new PlayMoveEventArgs(request.PlayMove, request.AmountOfMoney));

                while (player == GameCenter.FindRoomsByCriteria(-1, player).ElementAt(0).CurrentHand.CurrentRound.CurrentPlayer) ;

                result.NextPlayer = GameCenter.FindRoomsByCriteria(-1, player).ElementAt(0).CurrentHand.CurrentRound.CurrentPlayer.GetHashCode();
                result.TotalRaise = GameCenter.FindRoomsByCriteria(-1, player).ElementAt(0).CurrentHand.CurrentRound.TotalRaise;
                result.LastRaise = GameCenter.FindRoomsByCriteria(-1, player).ElementAt(0).CurrentHand.CurrentRound.LastRaise;
                result.NextPlayerInvest = GameCenter.FindRoomsByCriteria(-1, player).ElementAt(0).CurrentHand.CurrentRound.LiveBets[player];
                result.Success = true;
            }
            catch(PokerException e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
                Logger.Error(e, this);
            }

            return result;
		}

        public void Clear()
        {
            Users.Clear();
            UserManager.Clear();
            GameCenter.ClearAll();
        }
    }
}
