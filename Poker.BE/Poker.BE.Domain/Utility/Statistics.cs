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
        protected int NumOfHandsPlayed { get; private set;}
        protected int NumOfHandsWon {get; private set;}
        protected int TotalBalance {get; private set;}      //The amount of money gained\lost in total
        #endregion

        #region Constructors
        public Statistics()
        {
            NumOfHandsPlayed = 0;
            NumOfHandsWon = 0;
            TotalBalance = 0;
        }
        #endregion

        #region Methods
        public void UpdateStatistic(bool won, int amount)
        {
            AddHandsPlayed();
            if (won)
            {
                AddHandsWon();
                ChangeBalance(amount);
            }
            else
                ChangeBalance(amount);
        }
        public void AddHandsPlayed()
        {
            NumOfHandsPlayed++;
        }

        public void AddHandsWon()
        {
            NumOfHandsWon++;
        }

        public void ChangeBalance(int amountOfMoney)
        {
            TotalBalance += amountOfMoney;
        }
        #endregion
    }
}
