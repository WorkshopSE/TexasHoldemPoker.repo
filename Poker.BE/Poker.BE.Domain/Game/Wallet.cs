using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Game
{
    public class Wallet : Utility.MoneyStorage
    {
        #region Properties
        public double AmountOfMoney { get; set; }
        #endregion

        #region Constructors
        public Wallet() : base() { }
        public Wallet(Currency currency) : base(currency) { }
        public Wallet(Currency currency, double amount) : base(currency, amount) { }

        // fixme - make wallet unified by ucc03/6
        public Wallet(int amount)
        {
            AmountOfMoney = amount;
        }
        #endregion

    }
}
