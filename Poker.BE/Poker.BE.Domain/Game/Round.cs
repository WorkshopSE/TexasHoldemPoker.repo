using Poker.BE.Domain.Utility.Exceptions;
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

        #region Fields
        private ICollection<Player> activeUnfoldedPlayers;
        private Turn currentTurn;
        private Player dealer;
        private Player currentPlayer;
        #endregion

        #region Constructors
        public Round(Player dealer, ICollection<Player> activeUnfoldedPlayers)
        {
            this.dealer = dealer;
            this.currentPlayer = this.activeUnfoldedPlayers.ElementAt((activeUnfoldedPlayers.ToList().IndexOf(dealer)+2)%activeUnfoldedPlayers.Count);
            this.activeUnfoldedPlayers = activeUnfoldedPlayers;
            this.currentTurn = new Turn(currentPlayer);
        }
        #endregion

        #region Methods
        public void PlayMove(Move playMove)
        {
            switch (playMove)
            {
                case Move.check :
                    {
                        currentTurn.Call();
                        break;
                    }
                case  Move.call:
                    {
                        currentTurn.Check();
                        break;
                    }
                case Move.fold:
                    {
                        currentTurn.Fold();
                        break;
                    }
                case Move.bet:
                    {
                        currentTurn.Bet();
                        break;
                    }
                case Move.raise:
                    {
                        currentTurn.Raise();
                        break;
                    }
                case Move.allin:
                    {
                        currentTurn.AllIn();
                        break;
                    }
                default:
                    {
                        //TODO: print invalid move exception
                        throw new NotEnoughPlayersException("Invalid Move");
                    }
            }
            //Change Player
            calculateNextPlayer();
            currentTurn.CurrentPlayer = this.currentPlayer;
        }
        private void calculateNextPlayer()
        {
            this.currentPlayer = this.activeUnfoldedPlayers.ElementAt((activeUnfoldedPlayers.ToList().IndexOf(this.currentPlayer) + 1) % activeUnfoldedPlayers.Count);
        }
        #endregion
    }
}
