using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poker.BE.Service.Modules.Results;
using Poker.BE.CrossUtility.Exceptions;
using Poker.BE.CrossUtility.Loggers;
using Poker.BE.Service.Modules.Caches;

namespace Poker.BE.Service.Services
{
    public class LeagueService : IServices.ILeaguesService
    {

        #region Fields
        private CommonCache _cache;
        #endregion

        #region Properties
        public ILogger Logger { get { return CrossUtility.Loggers.Logger.Instance; } }
        #endregion

        #region Constructors
        public LeagueService()
        {
            _cache = CommonCache.Instance;
        }
        #endregion

        #region Methods
        public LeaguesResult GetAllLeagues(string userName)
        {
            var result = new LeaguesResult();

            try
            {
                var user = _cache.RefreshAndGet(
                    _cache.Users,
                    userName,
                    new UserNotFoundException(string.Format("user name: {0} not found, please re-login to server.", userName))
                    );

                result.Leagues = _cache.Leagues.Select(domainLeaguePair => new LeaguesResult.League()
                {
                    ID = domainLeaguePair.Key,
                    MaxLevel = domainLeaguePair.Value.MaxLevel,
                    MinLevel = domainLeaguePair.Value.MinLevel,
                    Name = domainLeaguePair.Value.Name,
                    IsFull = domainLeaguePair.Value.IsFull,
                }).ToArray();

                result.RelevantID = _cache.GameCenter.GetRelevantLeague(user.Level);
            }
            catch (PokerException e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
                Logger.Error(e, this);
            }

            return result;
        }
        #endregion
    }
}
