using Poker.BE.Domain.Utility.Exceptions;
using System.IO;
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
        private Player currentPlayer;
        private Dictionary<Player, int> liveBets;
        private int lastRaise;
        #endregion

        #region Constructors
        public Round(Player dealer, ICollection<Player> activeUnfoldedPlayers)
        {
            this.dealer = dealer;
            this.currentPlayer = this.activeUnfoldedPlayers.ElementAt((activeUnfoldedPlayers.ToList().IndexOf(dealer)+2)%activeUnfoldedPlayers.Count);
            this.activeUnfoldedPlayers = activeUnfoldedPlayers;
            this.currentTurn = new Turn(currentPlayer);
            this.liveBets = new Dictionary<Player, int>();
            foreach (Player player in activeUnfoldedPlayers)
            {
                liveBets.Add(player, 0);
            }
            this.lastRaise = 0;
        }
        #endregion

        #region Methods
        public void PlayMove(Move playMove)
        {
            switch (playMove)
            {
                case Move.check :
                    {
                        currentTurn.Check();  // Do nothing??
                        break;
                    }
                case  Move.call:
                    {
                        int amountToCall = lastRaise - liveBets[currentPlayer];
                        currentTurn.Call(amountToCall);
                        break;
                    }
                case Move.fold:
                    {
                        currentTurn.Fold();
                        Player playerToRemove = this.currentPlayer;
                        this.currentPlayer = this.activeUnfoldedPlayers.ElementAt((activeUnfoldedPlayers.ToList().IndexOf(this.currentPlayer) - 1) % activeUnfoldedPlayers.Count);
                        activeUnfoldedPlayers.Remove(playerToRemove);
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
