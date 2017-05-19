using System;
using System.Collections.Generic;

namespace Poker.BE.Domain.Game
{
    public class PickAWinner
    {

		#region Constants
		public const int POWER = 14;
		#endregion

		#region Fields
		private ICollection<Player> Players;
		private Card[] FifthHandCard;
		private Dictionary<Player, Card[]> PlayerSevenCards;
		#endregion

		#region Properties
		public Player Winner { get; private set; }
		#endregion

		#region Constructors
        public PickAWinner (ICollection<Player> Players, Card[] FifthHandCard)
		{
            this.Players = Players;
            this.FifthHandCard = FifthHandCard;
            InitializeDictionnary();
            GetWinner();
        }
		#endregion

		#region Methods
        private Player GetWinner(){
            return null;
        }

        private void InitializeDictionnary(){
            PlayerSevenCards = new Dictionary<Player, Card[]>();
            foreach (Player player in Players){
                Card[] PlayerSevenCardArray = new Card[7];
                for (int i = 0; i < 2; i++){
                    PlayerSevenCardArray[i] = player.PrivateCards[i];
                }
                for (int k = 0; k < 5; k++){
                    PlayerSevenCardArray[k + 2] = FifthHandCard[k];
                }
                SortSevenCardsByValue(PlayerSevenCardArray);
                PlayerSevenCards.Add(player, PlayerSevenCardArray);
            }
        }

        private void SortSevenCardsByValue (Card[] CardArray){
            Card[] SortedArrayCard = new Card[7];
            for (int i = 0; i < CardArray.Length; i++){
                for (int k = i+1; k < 6; k++){
                    if ( CardArray[i].GetValueNumber() > CardArray[k].GetValueNumber()){
                        Swap(CardArray[i], CardArray[k]); 
                    }
                }
            }
        }

        private void Swap (Card Card1, Card Card2){
            Card tmp = Card1;
            Card1 = Card2;
            Card2 = Card1;
        }


        //We calculate the sum in this way - BestCard * POWER^ 4 + SecondBestCard * POWER^3..
		private double HighFiveCardValue (Card[] PlayerAndTableCards) {
            int i=0;
            int exponant = 4;
            double sum = 0;
            int j = 6;
            while (PlayerAndTableCards[i].GetValueNumber() == 1){ // in case we have an or more AS
                sum = Math.Pow(POWER, exponant) * PlayerAndTableCards[i].GetValueNumberWIthAs();
                exponant = exponant - 1;
                i = i + 1;
            }
            while (exponant >= 0){
                sum = Math.Pow(POWER, exponant) * PlayerAndTableCards[j].GetValueNumber();
				exponant = exponant - 1;
                j = j - 1;
			}
            return sum;
		}

		private int IsPair(Card[] PlayerAndTableCards) {
            int PairValue = -1;
            for (int i = 0; i < PlayerAndTableCards.Length - 1 && PairValue== -1 ; i++) {
                if (PlayerAndTableCards[i].CompareToNumber(PlayerAndTableCards[i+1]) == 0){
                    PairValue = PlayerAndTableCards[i].GetValueNumberWIthAs();
                }
            }
            return PairValue;
		}

		private int IsTwoPair(Card[] PlayerAndTableCards) {
			int TwoPairValue = -1;
            for (int i = 0; i < PlayerAndTableCards.Length - 1 && TwoPairValue < (POWER + 1); i++)
			{
				if (PlayerAndTableCards[i].CompareToNumber(PlayerAndTableCards[i + 1]) == 0)
				{
                    if (TwoPairValue == -1 ){ // the value of the first pair x * POWER^0
						TwoPairValue = PlayerAndTableCards[i].GetValueNumberWIthAs();
					}
                    else{ // the value of the second pair - TwoPairValue + (x * POWER^1)
						TwoPairValue = TwoPairValue + (POWER * PlayerAndTableCards[i].GetValueNumberWIthAs());
                    }
				}
			}
            if (TwoPairValue < (POWER + 1)) TwoPairValue = -1;
			return TwoPairValue;

		}

		private int IsThreeOfAKind(Card[] PlayerAndTableCards) {
			int ThreeOfAKindValue = -1;
			for (int i = 0; i < PlayerAndTableCards.Length - 2 && ThreeOfAKindValue == -1; i++)
			{
				if (PlayerAndTableCards[i].CompareToNumber(PlayerAndTableCards[i + 1]) == 0) // pair
				{
                    if (PlayerAndTableCards[i + 1].CompareToNumber(PlayerAndTableCards[i + 2]) == 0) // Three of a Kind
						ThreeOfAKindValue = PlayerAndTableCards[i].GetValueNumberWIthAs();
				}
			}
			return ThreeOfAKindValue;
		}

		private int IsStraight (Card[] PlayerAndTableCards) {
			return -1;
		}

		private int IsFlush (Card[] PlayerAndTableCards) {
			return -1;
		}

		private int IsFullHouse (Card[] PlayerAndTableCards) {
            if (IsTwoPair(PlayerAndTableCards) > 0 && IsThreeOfAKind(PlayerAndTableCards) > 0){
                
            }
            else{
                return -1;
            }
		}

        private int IsFourOfAKind(Card[] PlayerAndTableCards) {
			return -1;
		}

		private int IsStraightFlush(Card[] PlayerAndTableCards) {
			return -1;
		}

        private int IsRoyalFlush (Card[] PlayerAndTableCards) {
			return -1;
		}

		#endregion



	}
}
