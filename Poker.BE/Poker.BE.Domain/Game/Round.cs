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
        //private Player dealer;        Is dealer needed??
        private Player currentPlayer;
        private Dictionary<Player, int> liveBets;
        private int totalRaise;
        private int lastRaise;
        private Pot currentPot;
        #endregion

        #region Constructors
        public Round(Player currentPlayer, ICollection<Player> activeUnfoldedPlayers, Pot currentPot)
        {
            this.currentPlayer = currentPlayer;
            this.activeUnfoldedPlayers = activeUnfoldedPlayers;
            this.currentPot = currentPot;
            this.currentTurn = new Turn(currentPlayer, currentPot);
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
                        //----------Put money in pot----------//
                        Pot tempPot = currentPot;
                        int amountToCall = totalRaise - liveBets[currentPlayer];
                        int playerCurrentBet = liveBets[currentPlayer];
                        while (amountToCall + playerCurrentBet > tempPot.AmountToClaim)
                        {
                            if (tempPot.PartialPot == null)
                            {
                                throw new ArgumentException("Not enough partial pots were created! Somthing isn't right!");
                            }

                            if (playerCurrentBet < tempPot.AmountToClaim)
                            {
                                int amountToAdd = tempPot.AmountToClaim - playerCurrentBet; //how much money does the player need to add in order to claim the pot
                                tempPot.Value += amountToAdd;
                                liveBets[currentPlayer] += amountToAdd;
                                currentTurn.Call(amountToAdd);
                                amountToCall -= amountToAdd;
                                playerCurrentBet = 0;
                            }
                            else
                            {
                                playerCurrentBet -= tempPot.AmountToClaim;
                            }

                            if (!tempPot.PlayersClaimPot.Contains(currentPlayer))
                                tempPot.PlayersClaimPot.Add(currentPlayer);

                            tempPot = tempPot.PartialPot;
                        }

                        if (amountToCall + playerCurrentBet < tempPot.AmountToClaim)
                        {
                            //Add new partial pot in the middle
                            Pot newPartialPot = new Pot(tempPot.BasePot);
                            newPartialPot.PartialPot = tempPot;
                            tempPot.BasePot = newPartialPot;
                            newPartialPot.BasePot.PartialPot = newPartialPot;

                            //Set new partial pot fields
                            foreach (Player player in tempPot.PlayersClaimPot)
                                newPartialPot.PlayersClaimPot.Add(player);
                            newPartialPot.PlayersClaimPot.Add(currentPlayer);
                            newPartialPot.AmountToClaim = amountToCall;
                            newPartialPot.Value = newPartialPot.PlayersClaimPot.Count * amountToCall;

                            tempPot.AmountToClaim -= amountToCall;
                            tempPot.Value -= newPartialPot.PlayersClaimPot.Count * amountToCall;
                        }
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
                        if (lastRaise > 0 || totalRaise > 0)
                            throw new IOException("Can't bet if someone had bet before... use raise move");
                        if (amountToBet == currentPlayer.Wallet.amountOfMoney)
                            throw new ArgumentOutOfRangeException("Can't bet all of your money... use all-in move");
                        int highestAllIn = 0;
                        foreach (Player player in activeUnfoldedPlayers)    //find highest all-in at the table
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
                            throw new IOException("Can't raise if no one had bet before... use bet move");
                        if (totalRaise + amountToBet == currentPlayer.Wallet.amountOfMoney + liveBets[currentPlayer])
                            throw new ArgumentOutOfRangeException("Can't bet all of your money... use all-in move");
                        int highestAllIn = 0;
                        foreach (Player player in activeUnfoldedPlayers)    //find highest all-in at the table
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
                        int highestOtherAllIn = 0;
                        foreach (Player player in activeUnfoldedPlayers)    //find highest all-in of the other players at the table
                        {
                            if (player != this.currentPlayer && player.Wallet.amountOfMoney + liveBets[player] > highestOtherAllIn)
                                highestOtherAllIn = player.Wallet.amountOfMoney;
                        }
                        if (this.currentPlayer.Wallet.amountOfMoney > highestOtherAllIn)
                            throw new IOException("all-in is bigger than the highest other player's all-in... use bet\raise move");
                        if (currentPlayer.Wallet.amountOfMoney == 0)
                            throw new ArgumentException("You're already all-in!!");

                        if (this.currentPlayer.Wallet.amountOfMoney <= totalRaise)
                            PlayMove(Move.call, this.currentPlayer.Wallet.amountOfMoney);
                        else
                        {
                            PlayMove(Move.call, totalRaise);
                            PlayMove(Move.raise, this.currentPlayer.Wallet.amountOfMoney);
                        }
                        currentTurn.AllIn();

                        /*//put money in pot - create partial pot if needed
                        Pot tempPot = currentPot;
                        int playerCurrentBet = liveBets[currentPlayer];
                        while (currentPlayer.Wallet.amountOfMoney + playerCurrentBet > tempPot.AmountToClaim)
                        {
                            if (playerCurrentBet < tempPot.AmountToClaim)
                            {
                                int amountToAdd = tempPot.AmountToClaim - playerCurrentBet; //how much money does the player need to add in order to claim the pot
                                tempPot.Value += amountToAdd;
                                liveBets[currentPlayer] += amountToAdd;
                                currentPlayer.Wallet.amountOfMoney -= amountToAdd;
                                playerCurrentBet = 0;
                            }
                            else
                            {
                                playerCurrentBet -= tempPot.AmountToClaim;
                            }

                            if (!tempPot.PlayersClaimPot.Contains(currentPlayer))
                                tempPot.PlayersClaimPot.Add(currentPlayer);
                            
                            
                            if (tempPot.PartialPot == null)
                            {
                                tempPot.PartialPot = new Pot(tempPot);
                            }
                            tempPot = tempPot.PartialPot;
                        }*/

                        break;
                    }
                default:
                    {
                        //TODO: print invalid move exception
                        throw new ArgumentException("Invalid Move");
                    }
            }
            //Change Player
            calculateNextPlayer();
            currentTurn.CurrentPlayer = this.currentPlayer;
        }
        private void calculateNextPlayer()
        {
            do
                this.currentPlayer = this.activeUnfoldedPlayers.ElementAt((activeUnfoldedPlayers.ToList().IndexOf(this.currentPlayer) + 1) % activeUnfoldedPlayers.Count);
            while (this.currentPlayer.CurrentState == Player.State.ActiveAllIn);

        }
        #endregion
    }
}
