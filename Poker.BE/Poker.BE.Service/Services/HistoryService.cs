using Poker.BE.CrossUtility.Exceptions;
using Poker.BE.CrossUtility.Loggers;
using Poker.BE.Domain.Core;
using Poker.BE.Domain.Security;
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
    public class HistoryService : IServices.IHistoryService
    {
        #region Fields
        private CommonCache _cache;
        #endregion

        #region properties
        public IDictionary<string, User> Users { get { return _cache.Users; } }
        public ILogger Logger { get { return CrossUtility.Loggers.Logger.Instance; } }
        public UserManager UserManager { get { return _cache.UserManager; } }

        #endregion

        #region Constructors
        public HistoryService()
        {
            _cache = CommonCache.Instance;
        }

        public void Clear()
        {
            Users.Clear();
            UserManager.Clear();
        }
        #endregion


        public GetStatisticsResult GetStatistics(GetStatisticsRequest request)
        {
            var result = new GetStatisticsResult();
            try
            {
                result.WinRateStatistics = UserManager.Users[request.UserName].GetWinRateStatistics();
                result.GrossProfitWinRateStatistics = UserManager.Users[request.UserName].GetGrossProfitWinRateStatistics();
                result.Success = true;
            }
            catch (PokerException e)
            {
                result.ErrorMessage = e.Message;
                result.Success = false;
                Logger.Log(e.Message, this);
            }
            return result;
        }
    }
}
