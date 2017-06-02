using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Utility
{
    public class Statistics
    {
        #region Fields
        private int gamesPlayed;
        private double grossProfits;
        private double grossLosses;
        #endregion

        #region Constructors
        public Statistics()
        {
            gamesPlayed = 0;
            grossProfits = 0;
            grossLosses = 0;
        }
        #endregion

        #region Methods
        public void AddHandStatistic(int amountOfMoney)
        {
            if (amountOfMoney > 0)
            {
                grossProfits += amountOfMoney;
            }
            else
            {
                grossLosses -= amountOfMoney;
            }

            gamesPlayed++;
        }

        public double GetWinRate()
        {
            return (grossProfits - grossLosses) / gamesPlayed;
        }

        public double GetGrossProfitWinRate()
        {
            return grossProfits / gamesPlayed;
        }
        #endregion
    }
}
