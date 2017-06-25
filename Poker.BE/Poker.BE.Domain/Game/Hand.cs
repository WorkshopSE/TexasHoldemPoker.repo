using Poker.BE.CrossUtility.Exceptions;
using System.Collections.Generic;

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
        private GamePreferences preferences;
        
        #endregion

        #region Properties
        public bool Active { get; set; }
        public Round CurrentRound { get; private set; }
        public Pot Pot { get { return pot; } set { pot = value; } }
        public Card[] CommunityCards { get { return communityCards; } set { communityCards = value; } }
        public Dictionary<Player, double> PlayersBets { get; private set; }
        public ICollection<Player> ActivePlayers { get { return activePlayers; } }
        public Player Dealer { get { return dealer; } }
        #endregion

        #region Constructors
        public Hand(Player dealer, ICollection<Player> players, GamePreferences preferences)
        {
            if (players.Count < MINIMAL_NUMBER_OF_ACTIVE_PLAYERS_TO_START)
            {
                throw new NotEnoughPlayersException();
            }
            this.preferences = preferences;
            deck = new Deck();
            activePlayers = players;
            PlayersBets = new Dictionary<Player, double>();
            foreach (Player player in activePlayers)
            {
                player.CurrentState = Player.State.ActiveUnfolded;
                PlayersBets.Add(player, 0);
            }
            communityCards = new Card[NUM_OF_COMMUNITY_CARDS];
            pot = new Pot();
            this.dealer = dealer;
            CurrentRound = new Round(dealer, activePlayers, this.pot, true, this.preferences);
            Active = true;
            
        }

        #endregion

        #region Methods
        public void PlayHand()
        {
            PrepareHand();

            //PRE FLOP
            CurrentRound.PlayBettingRound();
            UpdatePlayersBets();
            pot = CurrentRound.CurrentPot;

            //FLOP
            deck.PullCard(Card.State.FaceDown); //burn card
            int i = 0;
            foreach (Card card in deck.PullCards(3))
            {
                communityCards[i] = card;
                i++;
            }

            //SECOND BETTING ROUND
            CurrentRound = new Round(dealer, activePlayers, pot, false, preferences);
            activePlayers = CurrentRound.PlayBettingRound();
            UpdatePlayersBets();
            pot = CurrentRound.CurrentPot;

            //TURN
            deck.PullCard(Card.State.FaceDown); //burn card
            communityCards[i] = deck.PullCard(Card.State.FaceUp);
            i++;

            //THIRD BETTING ROUND
            CurrentRound = new Round(dealer, activePlayers, pot, false, preferences);
            activePlayers = CurrentRound.PlayBettingRound();
            UpdatePlayersBets();
            pot = CurrentRound.CurrentPot;

            //RIVER
            deck.PullCard(Card.State.FaceDown); //burn card
            communityCards[i] = deck.PullCard(Card.State.FaceUp);

            //FORTH BETTING ROUND
            CurrentRound = new Round(dealer, activePlayers, pot, false, preferences);
            activePlayers = CurrentRound.PlayBettingRound();
            UpdatePlayersBets();
            pot = CurrentRound.CurrentPot;
        }

        public void EndHand()
        {
            //TODO: implement Showdown
            Showdown();
            PickAWinner();

            this.Active = false;    //is it necessary? the hand will be killed anyway
        }
        #endregion

        #region Private Functions
        /// <summary>
        /// Prepare the hand for playing:
        /// Shuffle cards, place blinds and antes, and deal cards
        /// </summary>
        private void PrepareHand()
        {
            if (deck.Cards.Count != Deck.NCARDS)
            {
                throw new NotEnoughPlayersException("Cards must be dealt from a proper deck (standard 52-card deck containing no jokers)");
            }

            deck.ShuffleCards();
            PlaceBlinds();
            DealCards();
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
                    player.PrivateCards[i] = deck.PullCard(Card.State.FaceDown);
                }
            }
        }

        private void PlaceBlinds()
        {
            PlaceAntes();
            PlaceSmallBlind();
            PlaceBigBlind();

            UpdatePlayersBets();
        }

        /// <summary>
        /// If needed:
        /// All of the active players are forced to pay some blind
        /// payment to the pot, regardless to the regular blinds.
        /// </summary>
        private void PlaceAntes()
        {
            if (preferences.AntesValue == 0)
                return;

            //first player has to "bet" the ante
            if (CurrentRound.CurrentPlayer.Wallet.AmountOfMoney < preferences.AntesValue)
                CurrentRound.PlayMove(Round.Move.Allin, preferences.AntesValue);
            else
                CurrentRound.PlayMove(Round.Move.Bet, preferences.AntesValue);

            //other players "call" the ante
            for (int i = 1; i < CurrentRound.ActiveUnfoldedPlayers.Count; i++)
            {
                if (CurrentRound.CurrentPlayer.Wallet.AmountOfMoney < preferences.AntesValue)
                    CurrentRound.PlayMove(Round.Move.Allin, preferences.AntesValue);
                else
                    CurrentRound.PlayMove(Round.Move.Call, preferences.AntesValue);
            }
        }

        private void PlaceSmallBlind()
        {
            if (CurrentRound.CurrentPlayer.Wallet.AmountOfMoney < preferences.MinimumBet / 2)
                CurrentRound.PlayMove(Round.Move.Allin, 0);
            else
                CurrentRound.PlayMove(Round.Move.Raise, preferences.MinimumBet / 2);
        }

        private void PlaceBigBlind()
        {
            if (CurrentRound.CurrentPlayer.Wallet.AmountOfMoney < preferences.MinimumBet)
                CurrentRound.PlayMove(Round.Move.Allin, 0);
            else
                //the raise amount is another small blind
                CurrentRound.PlayMove(Round.Move.Raise, preferences.MinimumBet / 2);
        }

        private void UpdatePlayersBets()
        {
            foreach (Player player in CurrentRound.ActiveUnfoldedPlayers)
            {
                PlayersBets[player] += CurrentRound.LiveBets[player];
            }
        }

        private void Showdown()
        {
            //TODO
        }

        private void PickAWinner()
        {
            //Change all player's bets to negative numbers (for statistics)
            foreach (Player player in activePlayers)
            {
                PlayersBets[player] *= -1;
            }

            //Go to the last pot
            Pot lastPot = CurrentRound.CurrentPot;
            while (lastPot.BasePot != null)
            {
                lastPot = lastPot.BasePot;
            }

            List<Player> potWinners;
            //pick winner for each pot separately
            while (lastPot != null)
            {
                PickAWinner pickPotWinner = new PickAWinner(lastPot.PlayersClaimPot, communityCards);
                potWinners = pickPotWinner.GetWinners();

                double playerWinningMoney = lastPot.Value / potWinners.Count;
                //divide pot money to winning players
                foreach (Player player in potWinners)
                {
                    if (!PlayersBets.ContainsKey(player))
                    {
                        throw new PlayerNotFoundException("Can't find the winning player in the acitveUnfoldedPlayers... not possible");
                    }
                    PlayersBets[player] += playerWinningMoney;
                    player.AddMoney(playerWinningMoney);
                }

                lastPot = lastPot.PartialPot;
            }
        }
        #endregion
    }
}
