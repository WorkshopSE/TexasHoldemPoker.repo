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

        #region Methods
        public KeepAliveResult KeepAlive(KeepAliveRequest request)
        {
            var result = new KeepAliveResult ();
            try
            {
                Room room = null;
                Hand hand = null;
                Round round = null;

                #region Set Room, Hand and Round
                if (GameCenter.RoomsByID.ContainsKey(request.Room))
                {
                    room = GameCenter.RoomsByID[request.Room];
                }
                else
                {
                    throw new RoomNotFoundException("Can't find room in game center");
                }

                if (room.CurrentHand != null)
                {
                    hand = room.CurrentHand;
                    if (hand.CurrentRound != null)
                    {
                        round = hand.CurrentRound;
                    }
                }
                #endregion

                #region Room's info
                //get room's active players
                result.ActivePlayers = new List<int>();
                foreach (Player p in room.ActivePlayers)
                {
                    result.ActivePlayers.Add(p.GetHashCode());
                }

                #region Players info by table location
                //get room's players location in table
                //get all current active player's states at the table
                //get all players bets in the current round
                result.TableLocationOfActivePlayers = new int[10];
                result.PlayersStates = new string[10];
                result.PlayersBets = new double[10];
                for (int i = 0; i < 10; i++)
                {
                    Chair chair = null;
                    foreach (Chair c in room.Chairs)
                    {
                        if (c.Index == i)
                        {
                            chair = c;
                            break;
                        }
                    }
                    if (chair == null)
                    {
                        throw new WrongIOException("Chair not found");
                    }
                    if (room.TableLocationOfActivePlayers.ContainsKey(chair))
                    {
                        Player player = room.TableLocationOfActivePlayers[chair];

                        result.TableLocationOfActivePlayers[i] = player.GetHashCode();
                        result.PlayersStates[i] = player.CurrentState.ToString();
                        if (hand != null && round != null)
                            result.PlayersBets[i] = room.CurrentHand.CurrentRound.LiveBets[player];
                    }
                    else
                    {
                        result.PlayersStates[i] = Player.State.Passive.ToString();
                        result.PlayersBets[i] = -1;
                    }
                }
                #endregion

                result.IsTableFull = room.IsTableFull;
                #endregion

                #region Hand's info
                //get player's and table's cards
                if (hand != null)
                {
                    result.PlayersAndTableCards = new int[52];
                    foreach (Player p in hand.ActivePlayers)
                    {
                        if (p.PrivateCards[0] != null && p.PrivateCards[1] != null)
                        {
                            result.PlayersAndTableCards[p.PrivateCards[0].Index] = p.GetHashCode();
                            result.PlayersAndTableCards[p.PrivateCards[1].Index] = p.GetHashCode();
                        }
                    }
                    for (int i = 0; i < hand.CommunityCards.Length; i++)
                    {
                        if (hand.CommunityCards[i] == null)
                            break;
                        result.PlayersAndTableCards[hand.CommunityCards[i].Index] = (-1) * (i + 1);
                    }

                    result.DealerId = hand.Dealer.GetHashCode();
                }
                #endregion

                #region Round's info
                if (round != null)
                {
                    result.CurrentPlayerID = round.CurrentPlayer.GetHashCode();

                    //get pot's values and amount to claim
                    result.PotsValues = new List<double>();
                    if (round.CurrentPot == null)
                    {
                        throw new WrongIOException("Round's pot is somehow null...");
                    }
                    Pot potIter = round.CurrentPot;
                    while (potIter.BasePot != null)
                    {
                        potIter = potIter.BasePot;
                    }
                    while (potIter != null)
                    {
                        result.PotsValues.Add(potIter.Value);
                        result.PotsAmountToClaim.Add(potIter.AmountToClaim);
                        potIter = potIter.PartialPot;
                    }

                    //Note: Result.PlayersBets info is in Room's info (above)

                    result.TotalRaise = round.TotalRaise;
                    result.LastRaise = round.LastRaise;
                }
                #endregion

                //Player's info
                if (room.ActivePlayersByID.ContainsKey(request.PlayerID))
                {
                    result.PlayerWallet = room.ActivePlayersByID[request.PlayerID].WalletValue;
                }

                result.Success = true;
            }
            catch (RoomNotFoundException e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
            }
            catch (WrongIOException e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
            }

            return result;
        }

        public void Clear()
        {
            Users.Clear();
            UserManager.Clear();
            GameCenter.ClearAll();
        }
        #endregion
    }
}
