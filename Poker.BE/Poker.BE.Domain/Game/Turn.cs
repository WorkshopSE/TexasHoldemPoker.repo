using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Game
{
    public class Turn
    {
        #region Fields
        private Player currentPlayer;

        public Player CurrentPlayer { get => currentPlayer; set => currentPlayer = value; }
        #endregion

        #region Constructors
        public Turn(Player player)
        {
            this.CurrentPlayer = player;
        }
        #endregion

        #region Methods
        public void Check()
        {
            ///  Do noting? 
            ///  <see cref="Round.calculateNextPlayer" />
            ///  and <see cref="Round.PlayMove(Round.Move)" />
            ///  for more information TOMER
        }

        public void Call()
        {
            //TODO
        }

        public void Fold()
        {
            //TODO
        }

        public void Bet()
        {
            //TODO
        }

        public void Raise()
        {
            //TODO
        }

        public void AllIn()
        {
            //TODO
        }
    }
}
#endregion