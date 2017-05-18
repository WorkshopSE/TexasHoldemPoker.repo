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
        private Player dealer;
        private Player currentPlayer;
        private Pot pot;
        #endregion

        #region Constructors
        public Round(Player dealer, ICollection<Player> activeUnfoldedPlayers, Pot pot)
        {
            this.dealer = dealer;
            this.currentPlayer = this.activeUnfoldedPlayers.ElementAt((activeUnfoldedPlayers.ToList().IndexOf(dealer) + 2) % activeUnfoldedPlayers.Count);
            this.activeUnfoldedPlayers = activeUnfoldedPlayers;
            this.pot = pot;
        }
		#endregion

		#region Methods
        // This is for the PreFlopRound, when the first player to speak is the third one (after big and blind), 
        // and the big and blind players must put the money before any bet
		public void StartPreRound()
		{
            calculateNextPlayer();
            Player SmallBlind = currentPlayer;

		}


        public void StartRound(){
            
        }

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
