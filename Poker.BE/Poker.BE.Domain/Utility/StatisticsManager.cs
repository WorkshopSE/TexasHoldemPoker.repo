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

        #region Constructors
        public StatisticsManager()
        {
            StatisticsDictionary = new Dictionary<int, Statistics>();
        }
        #endregion

        #region Methods
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
            return StatisticsDictionary.ContainsKey(userId);
        }

        public Statistics GetUserStatistics(int userId)
        {
            return StatisticsDictionary[userId];
        }
        #endregion
    }
}
