using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Game
{
    public class Pot
    {
        #region Properties
        public Pot PartialPot { get; set; }
        public Pot BasePot { get; set; }
        public double Value { get; set; }
        public double AmountToClaim { get; set; }
        public List<Player> PlayersClaimPot { get; set; }
        #endregion

        #region Constructors
        public Pot()
        {
            Value = 0;
            AmountToClaim = 0;
            PlayersClaimPot = new List<Player>();
            PartialPot = null;
        }

        public Pot(Pot basePot) : this()
        {
            BasePot = basePot;
        }
        #endregion

        #region Methods
        public void AddToPot(double amount)
        {
            Value += amount;
        }

        public void CreatePartialPot()
        {
            PartialPot = new Pot(this);
            
        }
        #endregion
    }
}
