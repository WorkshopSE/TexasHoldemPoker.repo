using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Game
{
    public class Round
    {

        #region Enums
        public enum Move
        {
            check,
            call,
            bet,
            fold,
            raise,
            allin
        }
        #endregion

        #region fields
        private ICollection<Player> activeUnfoldedPlayers;
        private Turn currentTurn;
        private Player dealer;
        #endregion

        #region Constructors
        public Round(Player dealer, List<Player> activeUnfoldedPlayers)
        {
            this.dealer = dealer;
            this.activeUnfoldedPlayers = activeUnfoldedPlayers;
        }
        #endregion

        #region Methods
        public void PlayMove(Move playMove)
        {
            switch (playMove)
            {
                case Move.check :
                    {

                        break;
                    }
                case  Move.call:
                    {
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        #endregion
    }
}
