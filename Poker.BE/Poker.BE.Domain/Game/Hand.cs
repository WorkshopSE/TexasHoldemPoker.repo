using Poker.BE.Domain.Utility.Exceptions;
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
        public const int MINIMAL_NUMBER_OF_ACTIVE_PLAYERS_TO_START = 2;
        public const int NUM_OF_COMMUNITY_CARDS = 5;
        #endregion

        #region Fields
        private Deck deck;
        private ICollection<Player> activePlayers;
        private Card[] communityCards;
        private Pot pot;
        private Player dealer;
        private GameConfig gameConfig;
        #endregion

        #region Properties
        public bool Active { get; set; }
        public Round CurrentRound { get; }
        public Card[] CommunityCards { get { return communityCards; } }
        #endregion

        #region Constructors
        public Hand(Player dealer, ICollection<Player> players, GameConfig gameConfig)
        {
            if(players.Count < MINIMAL_NUMBER_OF_ACTIVE_PLAYERS_TO_START)
            {
                throw new Utility.Exceptions.NotEnoughPlayersException();
            }
            this.deck = new Deck();
            this.activePlayers = players;
            this.communityCards = new CommunityCard[NUM_OF_COMMUNITY_CARDS];
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
            if (deck.Cards.Count != Deck.NCARDS)
            {
                throw new NotEnoughPlayersException("Cards must be dealt from a proper deck (standard 52-card deck containing no jokers)");
            }

            deck.ShuffleCards();
            PlaceBlinds();
            DealCards();
        }

        public void PlayHand()
        {
            //PRE FLOP
            Round round = CurrentRound;
            round.PlayBettingRound();

            //FLOP
            deck.PullCard(Card.State.FaceDown); //burn card
            int i = 0;
            foreach (Card card in deck.PullCards(3))
            {
                communityCards[i] = card;
                i++;
            }

            //SECOND BETTING ROUND
            round = new Round(dealer, activePlayers, pot, false, gameConfig);
            activePlayers = round.PlayBettingRound();

            //TURN
            deck.PullCard(Card.State.FaceDown); //burn card
            communityCards[i] = deck.PullCard(Card.State.FaceUp);
            i++;

            //THIRD BETTING ROUND
            round = new Round(dealer, activePlayers, pot, false, gameConfig);
            activePlayers = round.PlayBettingRound();

            //RIVER
            deck.PullCard(Card.State.FaceDown); //burn card
            communityCards[i] = deck.PullCard(Card.State.FaceUp);

            //FORTH BETTING ROUND
            round = new Round(dealer, activePlayers, pot, false, gameConfig);
            activePlayers = round.PlayBettingRound();
        }

        public void EndHand()
        {
            //TODO: implement
            Showdown();
            PickAWinner();

            this.Active = false;    //is it necessary? the hand will be killed anyway
        }
        #endregion

        #region Private Functions
        /// <summary>
        /// Each player receive 2 face-down cards
        /// </summary>
        private void DealCards()
        {
            foreach (Player player in activePlayers)
            {
                for (int i = 0; i < player.PrivateCards.Length; i++)
                {
                    player.PrivateCards[i] = deck.PullCard(Card.State.FaceDown);
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
                CurrentRound.PlayMove(Round.Move.Allin, gameConfig.AntesValue);
            else
                CurrentRound.PlayMove(Round.Move.Bet, gameConfig.AntesValue);

            //other players "call" the ante
            for (int i = 1; i < CurrentRound.ActiveUnfoldedPlayers.Count; i++)
            {
                if (CurrentRound.CurrentPlayer.Wallet.AmountOfMoney < gameConfig.AntesValue)
                    CurrentRound.PlayMove(Round.Move.Allin, gameConfig.AntesValue);
                else
                    CurrentRound.PlayMove(Round.Move.Call, gameConfig.AntesValue);
            }
        }

        private void PlaceSmallBlind()
        {
            if (CurrentRound.CurrentPlayer.Wallet.AmountOfMoney < gameConfig.MinimumBet / 2)
                CurrentRound.PlayMove(Round.Move.Allin, gameConfig.MinimumBet / 2);
            else
                CurrentRound.PlayMove(Round.Move.Raise, gameConfig.MinimumBet / 2);
        }

        private void PlaceBigBlind()
        {
            if (CurrentRound.CurrentPlayer.Wallet.AmountOfMoney < gameConfig.MinimumBet)
                CurrentRound.PlayMove(Round.Move.Allin, gameConfig.MinimumBet);
            else
                CurrentRound.PlayMove(Round.Move.Raise, gameConfig.MinimumBet);
        }

        private void Showdown()
        {
            throw new NotImplementedException();
        }

        private void PickAWinner()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
