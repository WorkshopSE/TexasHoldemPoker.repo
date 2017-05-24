using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Game
{
    public class Pot
    {
        #region Fields
        private Pot partialPot;
        private Pot basePot;
        private double value;
        private double amountToClaim;
        private List<Player> playersClaimPot;
        #endregion

        #region Properties
        public Pot PartialPot { get { return this.partialPot; } set { this.partialPot = value; } }
        public Pot BasePot { get { return this.basePot; } set { this.basePot = value; } }

        public double Value { get { return this.value; } set { this.value = value; } }
        public double AmountToClaim { get { return this.amountToClaim; } set { this.amountToClaim = value; } }

        public List<Player> PlayersClaimPot { get { return this.playersClaimPot; } set { this.playersClaimPot = value; } }
        #endregion

        #region Constructors
        public Pot()
        {
            this.value = 0;
            this.amountToClaim = 0;
            this.playersClaimPot = new List<Player>();
            this.partialPot = null;
        }

        public Pot(Pot basePot) : this()
        {
            this.basePot = basePot;
        }
        #endregion

        #region Methods
        public void AddToPot(double amount)
        {
            this.value += amount;
        }

        public void CreatePartialPot()
        {
            partialPot = new Pot(this);
            
        }
        #endregion
    }
}
