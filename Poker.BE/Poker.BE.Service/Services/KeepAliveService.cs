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
            throw new NotImplementedException();
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
