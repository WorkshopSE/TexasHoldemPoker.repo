using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Utility
{
    public class StatisticsManager
    {
        #region Properties
        protected Dictionary<int, Statistics> StatisticsDictionary;
        #endregion

        #region Methods
        public StatisticsManager()
        {
            StatisticsDictionary = new Dictionary<int, Statistics>();
        }

        public bool AddUser(int userId)
        {
            if(!IsUserExist(userId)) {
                Statistics StatisticsToAdd = new Statistics();
                StatisticsDictionary.Add(userId, StatisticsToAdd);
                return true;
            }
            return false;
        }

        public bool RemoveUser(int userId)
        {
            return StatisticsDictionary.Remove(userId);
        }

        public bool IsUserExist(int userId)
        {
            if(userId != null)
                return StatisticsDictionary.ContainsKey(userId);
            return false;
        }

        public Statistics GetUserStatistics(int userId)
        {
            return Statistics[userId];
        }
        #endregion
    }
}
