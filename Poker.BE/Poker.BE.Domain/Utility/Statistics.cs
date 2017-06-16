using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Utility
{
    public class Statistics
    {
        #region Properties
        public int GamesPlayed { get; private set; }
        public double GrossProfits { get; private set; }
        public double GrossLosses { get; private set; }
        #endregion

        #region Constructors
        public Statistics()
        {
            GamesPlayed = 0;
            GrossProfits = 0;
            GrossLosses = 0;
        }
        #endregion

        #region Methods
        public void AddHandStatistic(double amountOfMoney)
        {
            if (amountOfMoney > 0)
            {
                GrossProfits += amountOfMoney;
            }
            else
            {
                GrossLosses -= amountOfMoney;
            }

            GamesPlayed++;
        }

        public void CombineStatistics(Statistics otherStatistics)
        {
            GamesPlayed += otherStatistics.GamesPlayed;
            GrossProfits += otherStatistics.GrossProfits;
            GrossLosses += otherStatistics.GrossLosses;
        }


        //Unnecessary function?
        public double GetWinRate()
        {
            return (GrossProfits - GrossLosses) / GamesPlayed;
        }
        //Unnecessary function?
        public double GetGrossProfitWinRate()
        {
            return GrossProfits / GamesPlayed;
        }
        #endregion
    }
}
