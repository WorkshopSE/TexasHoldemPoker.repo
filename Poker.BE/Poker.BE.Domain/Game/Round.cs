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
        //this boolean helps us to know if the betting round is over
        private bool endBetting;

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
            PlayBettingRound();
		}

        // the functions is reponsable of the player turns
        public void PlayBettingRound(){
            
        }

        public void StartRound(){
            int numberOfPlayers = activeUnfoldedPlayers.Count;
            while (!endBetting){
				for (int i = 0; i < numberOfPlayers; i++)
				{
                    Player currentPlayer = 
                    if(PlayerInvest)

				}   
            }

        }

        public void PlayMove(Move playMove)
        {
            switch (playMove)
            {
                case Move.check :
                    {
                        break;
                    }
                case  Move.call:
                    {
                        break;
                    }
                case Move.fold:
                    {
                        break;
                    }
                case Move.bet:
                    {
                        break;
                    }
                case Move.raise:
                    {
                        break;
                    }
                case Move.allin:
                    {
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
