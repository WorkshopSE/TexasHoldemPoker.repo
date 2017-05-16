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
        private int totalRaise;
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
            this.totalRaise = 0;
            this.lastRaise = 0;
        }
        #endregion

        #region Methods
        public void PlayMove(Move playMove, int amountToBet)
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
                        int amountToCall = totalRaise - liveBets[currentPlayer];
                        currentTurn.Call(amountToCall);
                        liveBets[currentPlayer] = totalRaise;
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
                        if (lastRaise > 0)
                            throw new IOException("Can't bet if someone had bet before... use raise");
                        int highestAllIn = 0;
                        foreach (Player player in activeUnfoldedPlayers)
                        {
                            if (player.Wallet.amountOfMoney > highestAllIn)
                                highestAllIn = player.Wallet.amountOfMoney;
                        }
                        if (amountToBet > highestAllIn)
                            throw new ArgumentOutOfRangeException("Bet is bigger than the highest player's all-in");

                        currentTurn.Bet(amountToBet);
                        lastRaise = amountToBet;
                        totalRaise += lastRaise;
                        liveBets[currentPlayer] = totalRaise;
                        break;
                    }
                case Move.raise:
                    {
                        if (lastRaise == 0)
                            throw new IOException("Can't bet if someone had bet before... use raise");
                        int highestAllIn = 0;
                        foreach (Player player in activeUnfoldedPlayers)
                        {
                            if (player.Wallet.amountOfMoney + liveBets[player] > highestAllIn)
                                highestAllIn = player.Wallet.amountOfMoney;
                        }
                        if (totalRaise + amountToBet > highestAllIn)
                            throw new ArgumentOutOfRangeException("Raise is bigger than the highest player's all-in");

                        currentTurn.Raise(amountToBet);
                        lastRaise = amountToBet;
                        totalRaise += lastRaise;
                        liveBets[currentPlayer] = lastRaise;
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
