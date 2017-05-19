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
        private GamePreferences GamePreference;
        #endregion

        #region Properties
        public bool Active { get; set; }
        public Round CurrentRound { get; }
        #endregion

        #region Constructors
        public Hand(Player dealer, Deck deck, ICollection<Player> players, GamePreferences GamePreference)
        {
            if(players.Count < MINIMAL_NUMBER_OF_ACTIVE_PLAYERS_TO_START)
            {
                throw new Utility.Exceptions.NotEnoughPlayersException();
            }
            this.deck = deck;
            this.activePlayers = players;
            this.pot = new Pot();
            this.dealer = dealer;
			this.GamePreference = GamePreference;
			this.CurrentRound = new Round(dealer,activePlayers, pot, GamePreference);
            this.Active = true;
        }
		#endregion

		#region Methods
		/// <summary>
		/// Function who calls all the Hand functions by order 
		/// </summary>
		public void StartNewHand(){
            DealCards();
            PlayPreFlop();
            PlayFlop();
            PlayTurn();
            PlayRiver();
        }



        /// <summary>
        /// Each player receive 2 face-down cards
        /// </summary>
        public void DealCards()
        {
            for (int i = 0; i < Player.NPRIVATE_CARDS; i++)
            {
				foreach (Player player in activePlayers)
				{
                    player.PrivateCards[i] = deck.PullCard();
                    player.PrivateCards[i].CardState = Card.State.FaceDown;
                }
            }
        }

        public void PlaceBlinds(GamePreferences preferences)
        {
            PlaceSmallBlind(preferences);
            PlaceBigBlind(preferences);
            PlaceAnts(preferences);
        }

        public void endHand()
        {
            //TODO: implement
            this.Active = false;
        }

        /// <summary>
        /// If needed:
        /// All of the active players are forced to pay some blind
        /// payment to the pot, regardless to the regular blinds.
        /// </summary>
        private void PlaceAnts(GamePreferences preferences)
        {
            //TODO: WAIT FOR GamePreferences
            throw new NotImplementedException();
        }

        private void PlaceBigBlind(GamePreferences preferences)
        {
            //TODO: WAIT FOR GamePreferences
            throw new NotImplementedException();
        }

        private void PlaceSmallBlind(GamePreferences preferences)
        {
            //TODO: WAIT FOR GamePreferences
            throw new NotImplementedException();
        }


        private void PlayPreFlop() {
            Round PreFlopRound = new Round(dealer, activePlayers, pot, GamePreference);
            PreFlopRound.StartPreRound();
        }
        private void PlayFlop() {
            if (NeedNextTurn()){
				Round Flop = new Round(dealer, activePlayers, pot, GamePreference);
				Flop.StartRound();
			}
            else {
                PickAWinner();
            }
		}

        private void PlayTurn() {
			if (NeedNextTurn()) {
				Round TurnRound = new Round(dealer, activePlayers, pot, GamePreference);
				TurnRound.StartRound();
			}
			else {
			 	PickAWinner();
			}
		}

        private void PlayRiver() {
			if (NeedNextTurn()) {
				Round River = new Round(dealer, activePlayers, pot, GamePreference);
				River.StartRound();
			}
			else {
                ShowDown();
				PickAWinner();
			}
		} 

        private bool NeedNextTurn() {
            return activePlayers.Count() >= 2;
        }

        private void PickAWinner(){}

        private void ShowDown(){
            for (int i = 0; i < activePlayers.Count; i++){
                Card firstPlayerCard = activePlayers.ElementAt(i).PrivateCards[0];
				Card secondPlayerCard = activePlayers.ElementAt(i).PrivateCards[1];
                Console.WriteLine("The cards of Player {0} are: {1} {2}, and {3} {4}", i, firstPlayerCard.CardValue, firstPlayerCard.CardSuit, secondPlayerCard.CardValue, secondPlayerCard.CardSuit);
            }
        }

        #endregion
    }
}
