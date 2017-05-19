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
		private GamePreferences GamePreference;
        private double SumToEqual;
        // this is the way we have to now how much each player invested in the round 
        private Dictionary<Player, double> PlayerInvest;
        //this variable helps us to know if the betting round is over
        private int RestBettingPerson;

        #endregion

		#region Constructors
		public Round(Player dealer, ICollection<Player> activeUnfoldedPlayers, Pot pot, GamePreferences GamePreference)
        {
			InitializeDictionary();
			this.dealer = dealer;
            this.currentPlayer = this.activeUnfoldedPlayers.ElementAt((activeUnfoldedPlayers.ToList().IndexOf(dealer) + 2) % activeUnfoldedPlayers.Count);
            this.activeUnfoldedPlayers = activeUnfoldedPlayers;
            this.pot = pot;
            this.GamePreference = GamePreference;
            this.RestBettingPerson = 0;
        }
		#endregion

		#region Methods
        // This is for the PreFlopRound, when the first player to speak is the third one (after big and blind), 
        // and the big and blind players must put the money before any bet
		public void StartPreRound()
		{
            calculateNextPlayer(); // we want to get the small blind player
            Player SmallBlind = currentPlayer;
            double SmallBlindAmount = GamePreference.SmallBlind;
            bool succeedToPay = TakePlayerBet(SmallBlind, SmallBlindAmount);

			calculateNextPlayer(); // we want to get the big blind player
			Player BigBlind = currentPlayer;
			double BigBlindAmount = GamePreference.BigBlind;
            succeedToPay = TakePlayerBet(BigBlind, BigBlindAmount);
            SumToEqual = BigBlindAmount;

            calculateNextPlayer();
            StartRound();
		}

        // the functions is reponsable of the player turns

        public void StartRound(){
           int numberOfPlayers = activeUnfoldedPlayers.Count;
            while (RestBettingPerson < numberOfPlayers){
                for (int i = 0 ; i < numberOfPlayers; i++)
				{
                    if(PlayerInvest.ContainsKey(currentPlayer) && currentPlayer.CurrentState.Equals("ActiveUnfolded")){
                        if (PlayerInvest[currentPlayer] < SumToEqual){ // it means that someone bet more than actual player
                            PlayBetTurn(currentPlayer);
                        }
                        else{ 
                            PlayTurn(currentPlayer);
                        }
                        if (PlayerInvest[currentPlayer] > SumToEqual){
                            RestBettingPerson = 0;
                        }
                    }
                    else{
                        RestBettingPerson = RestBettingPerson + 1;
                    }
                    numberOfPlayers = activeUnfoldedPlayers.Count;
				}   
            }

        }

        private void PlayBetTurn(Player CurrentPlayer){
            bool canCheck = false;
            Turn playerTurn = new Turn(CurrentPlayer, canCheck);
        }

        private void PlayTurn (Player CurrentPlayer) {
			bool canCheck = true;
			Turn playerTurn = new Turn(CurrentPlayer, canCheck);
		}


        private void calculateNextPlayer()
        {
            this.currentPlayer = this.activeUnfoldedPlayers.ElementAt((activeUnfoldedPlayers.ToList().IndexOf(this.currentPlayer) + 1) % activeUnfoldedPlayers.Count);
        }

        private bool TakePlayerBet (Player PlayerWhoNeedToPays, double SumToPay){
            bool succeed = PlayerWhoNeedToPays.Pay(SumToPay);
            if (succeed){
                UpdateDictionnary(PlayerWhoNeedToPays, SumToPay);
            }
            return false;
        }

        private void InitializeDictionary(){
            PlayerInvest = new Dictionary<Player, double>();
            for (int i = 0; i < activeUnfoldedPlayers.Count; i++){
                PlayerInvest.Add(activeUnfoldedPlayers.ElementAt(i), 0);
            }
        }

        private void UpdateDictionnary(Player PlayerWhoNeedToPays, double SumToPay){
            double existingValue;
            if (PlayerInvest.TryGetValue(PlayerWhoNeedToPays, out existingValue)) // if the value exist in our dictionnary, update it to be the old+new sum.
                PlayerInvest[PlayerWhoNeedToPays] = existingValue + SumToPay;
        }


        #endregion
    }
}
