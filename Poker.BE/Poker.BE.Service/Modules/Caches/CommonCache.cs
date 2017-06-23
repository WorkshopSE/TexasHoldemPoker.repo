using Poker.BE.Domain.Core;
using Poker.BE.Domain.Game;
using Poker.BE.Domain.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Service.Modules.Caches
{
    public sealed class CommonCache : ICache
    {

        #region Properties
        public UserManager UserManager { get; set; }

        public GameCenter GameCenter { get; set; }
        
        /// <summary>
        /// Map for the player (user session) ID -> player at the given session.
        /// using the player.GetHashCode() for generating this ID.
        /// </summary>
        /// <remarks>
        /// session ID is the ID we give for a screen the user opens.
        /// this is a need because the user can play several screen at once.
        /// thus, to play with different players at the same time.
        /// 
        /// for now - the user cannot play as several players, at the same room.
        ///    - this option is blocked.
        /// </remarks>
        public IDictionary<int, Player> Players { get; set; }

        public IDictionary<int, Room> Rooms { get; set; }

        public IDictionary<string, User> Users { get; set; }

        #endregion

        #region Singleton Constructor
        // Note: for c# implementation
        static CommonCache() { }

		/* Note: Singleton private constructor
         * -----------------------------------
         * 
         * For every Cached Property we should initiate its value here.
         * */
		private CommonCache()
		{
            UserManager = UserManager.Instance;
            GameCenter = GameCenter.Instance;
            Players = new Dictionary<int, Player>();
            Rooms = new Dictionary<int, Room>();
            Users = new Dictionary<string, User>();
        }

		private static readonly CommonCache _instance = new CommonCache();

		public static CommonCache Instance { get { return _instance; } }
        #endregion

    }
}
