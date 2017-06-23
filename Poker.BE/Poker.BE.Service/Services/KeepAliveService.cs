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

namespace Poker.BE.Service.Services
{
    public class KeepAliveService : IServices.IKeepAliveService
    {
        #region Fields
        private CommonCache _cache;
        #endregion

        #region properties
        public IDictionary<string, User> Users { get { return _cache.Users; } }
        public UserManager UserManager { get { return _cache.UserManager; } }
        public GameCenter GameCenter { get { return _cache.GameCenter; } }
        #endregion

        #region Constructors
        public KeepAliveService()
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
                User user = UserManager.Users[request.User];
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
            catch (UserNotFoundException e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
            }
            catch (PlayerNotFoundException e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
            }
            catch (ArgumentException e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
            }

            return result;
        }

        public KeepAliveResult KeepAlive(KeepAliveRequest request)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            Users.Clear();
            UserManager.Clear();
            GameCenter.ClearAll();
        }
    }
}
