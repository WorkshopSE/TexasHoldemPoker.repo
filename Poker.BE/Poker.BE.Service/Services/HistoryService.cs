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


        public GetStatisticsResult GetStatistics(string userName)
        {
            var result = new GetStatisticsResult();
            try
            {
                User user = null;
                if (userName != null)
                {
                    user = _cache.RefreshAndGet(
                        Users,
                        userName,
                        new UserNotFoundException("User was not found, can't get statistics")
                        );
                }
                result.WinRateStatistics = user.GetWinRateStatistics();
                result.GrossProfitWinRateStatistics = user.GetGrossProfitWinRateStatistics();
                result.Success = true;
            }
            catch (PokerException e)
            {
                result.ErrorMessage = e.Message;
                result.Success = false;
                Logger.Error(e, this);
            }
            return result;
        }

        private GetAllUsersResult.UserResult DomainUserToUserResult(User domainUser)
        {
            GetAllUsersResult.UserResult result = new GetAllUsersResult.UserResult()
            {
                Avatar = domainUser.Avatar.Select(b => (int)b).ToArray(),
                GamesPlayed = domainUser.UserStatistics.GamesPlayed,
                GrossLosses = domainUser.UserStatistics.GrossLosses,
                GrossProfits = domainUser.UserStatistics.GrossProfits,
                Level = domainUser.Level,
                UserName = domainUser.UserName,
            };

            result.CashGain = result.GrossProfits - result.GrossLosses;

            return result;
        }

        public GetAllUsersResult GetAllUsers(CommonRequest request)
        {
            var result = new GetAllUsersResult();

            try
            {
                _cache.Refresh();
                result.Users = _cache.Users.Values.Distinct().Select(user => DomainUserToUserResult(user)).ToArray();
                result.Success = true;
            }
            catch (PokerException e)
            {
                result.ErrorMessage = e.Message;
                result.Success = false;
                Logger.Error(e, this);
            }

            return result;
        }
    }
}
