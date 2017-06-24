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
                //first of all get the relevant room
                Room room = null;
                foreach (Room r in GameCenter.Rooms)
                {
                    if (request.Room == r.GetHashCode())
                    {
                        room = r;
                        break;
                    }
                }
                if (room == null)
                {
                    throw new RoomNotFoundException("can't find room in game center");
                }

                //get room's active players
                result.ActivePlayers = new List<int>();
                foreach (Player p in room.ActivePlayers)
                {
                    result.ActivePlayers.Add(p.GetHashCode());
                }

                //get room's players location in table
                result.TableLocationOfActivePlayers = new int[10];
                for (int i = 0; i < 10; i++)
                {
                    
                }

                result.IsTableFull = room.IsTableFull;


            }
            catch (RoomNotFoundException e)
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
