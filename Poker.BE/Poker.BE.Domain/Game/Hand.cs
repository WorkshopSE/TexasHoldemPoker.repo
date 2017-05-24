using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Game
{
    /// <summary> Defined the hand that the active players are playing poker at the room </summary>
    /// <remarks>
    /// <author>Idan Izicovich</author>
    /// <lastModified>2017-04-25</lastModified>
    /// </remarks>
    public class Hand
    {

        #region Constants
        public static readonly int MINIMAL_NUMBER_OF_ACTIVE_PLAYERS_TO_START;
        #endregion

        #region Fields
        private Deck deck;
        private ICollection<Player> activePlayers;
        private Pot pot;
        private Player dealer;
        private GameConfig gameConfig;
        #endregion

        #region Properties
        public bool Active { get; set; }
        public Round CurrentRound { get; }
        #endregion

        #region Constructors
        public Hand(Player dealer, Deck deck, ICollection<Player> players, GameConfig gameConfig)
        {
            if(players.Count < MINIMAL_NUMBER_OF_ACTIVE_PLAYERS_TO_START)
            {
                throw new Utility.Exceptions.NotEnoughPlayersException();
            }
            this.deck = deck;
            this.activePlayers = players;
            this.pot = new Pot();
            this.dealer = dealer;
            this.CurrentRound = new Round(dealer, activePlayers, this.pot, true, this.gameConfig);
            this.Active = true;
            this.gameConfig = gameConfig;
            
        }

        #endregion

        #region Methods
        public void PrepareHand()
        {
            deck.ShuffleCards();
            PlaceBlinds();
            DealCards();
        }

        public void endHand()
        {
            //TODO: implement
            this.Active = false;
        }

        /// <summary>
        /// Each player receive 2 face-down cards
        /// </summary>
        private void DealCards()
        {
            foreach (Player player in activePlayers)
            {
                for (int i = 0; i < player.PrivateCards.Length; i++)
                {
                    player.PrivateCards[i] = deck.PullCard();
                    player.PrivateCards[i].CardState = Card.State.FaceDown;
                }
            }
        }

        private void PlaceBlinds()
        {
            PlaceAntes();
            PlaceSmallBlind();
            PlaceBigBlind();
        }

        /// <summary>
        /// If needed:
        /// All of the active players are forced to pay some blind
        /// payment to the pot, regardless to the regular blinds.
        /// </summary>
        private void PlaceAntes()
        {
            if (gameConfig.AntesValue == 0)
                return;

            //first player has to "bet" the ante
            if (CurrentRound.CurrentPlayer.Wallet.AmountOfMoney < gameConfig.AntesValue)
                CurrentRound.PlayMove(Round.Move.allin, gameConfig.AntesValue);
            else
                CurrentRound.PlayMove(Round.Move.bet, gameConfig.AntesValue);

            //other players "call" the ante
            for (int i = 1; i < CurrentRound.ActiveUnfoldedPlayers.Count; i++)
            {
                if (CurrentRound.CurrentPlayer.Wallet.AmountOfMoney < gameConfig.AntesValue)
                    CurrentRound.PlayMove(Round.Move.allin, gameConfig.AntesValue);
                else
                    CurrentRound.PlayMove(Round.Move.call, gameConfig.AntesValue);
            }
        }

        private void PlaceSmallBlind()
        {
            if (CurrentRound.CurrentPlayer.Wallet.AmountOfMoney < gameConfig.AntesValue)
                CurrentRound.PlayMove(Round.Move.allin, gameConfig.MinimumBet / 2);
            else
                CurrentRound.PlayMove(Round.Move.raise, gameConfig.MinimumBet / 2);
        }

        private void PlaceBigBlind()
        {
            if (CurrentRound.CurrentPlayer.Wallet.AmountOfMoney < gameConfig.AntesValue)
                CurrentRound.PlayMove(Round.Move.allin, gameConfig.MinimumBet);
            else
                CurrentRound.PlayMove(Round.Move.raise, gameConfig.MinimumBet);
        }
        #endregion
    }
}
