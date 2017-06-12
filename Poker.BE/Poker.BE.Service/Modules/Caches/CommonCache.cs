using Poker.BE.Domain.Core;
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
        public IDictionary<string, User> Users { get; set; }
        public UserManager UserManager { get; set; }

        #endregion

        #region Singleton Constructor
        // Note: for c# implementation
        static CommonCache() { }

		// Note: Singleton private constructor
		private CommonCache()
		{
            UserManager = UserManager.Instance;
            Users = new Dictionary<string, User>();
		}

		private static readonly CommonCache _instance = new CommonCache();

		public static CommonCache Instance { get { return _instance; } }
        #endregion

    }
}
