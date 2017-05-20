using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Game
{
    public class Wallet
    {
        #region Properties
        public int amountOfMoney { get; set; }
        #endregion

        #region Contructors
        public Wallet(int amount)
        {
            this.amountOfMoney = amount;
        }
        #endregion

    }
}
