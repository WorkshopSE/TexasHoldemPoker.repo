using Poker.BE.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Utility
{
    public class StatisticsManager
    {
        #region Fields
        private Dictionary<int, Statistics> usersStatistics;
        #endregion

        #region Constructors
        public StatisticsManager ()
        {
            usersStatistics = new Dictionary<int, Statistics>();
        }
        #endregion

        #region Methods
        public void AddUser(int userID)
        {
            usersStatistics.Add(userID, new Statistics());
        }

        public Statistics GetUserStatistics(int userID)
        {
            return usersStatistics[userID];
        }
        #endregion
    }
}
