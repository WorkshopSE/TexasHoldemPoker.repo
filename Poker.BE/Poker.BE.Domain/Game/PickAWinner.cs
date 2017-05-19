using System;
using System.Collections.Generic;

namespace Poker.BE.Domain.Game
{
    public class PickAWinner
    {

		#region Constants
		public const int POWER = 14;
        public const int FIVEBESTCARDS = 5;
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
                        CardArray = Swap(CardArray, i, k); 
                    }
                }
            }
        }

        private Card[] Swap (Card[] CardArray, int i, int k){
            Card tmp = CardArray[i];
            CardArray[i] = CardArray[k];
            CardArray[k] = tmp;
            return CardArray;
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
                if (PlayerAndTableCards[i].CompareTo(PlayerAndTableCards[i+1]) == 0){
                    PairValue = PlayerAndTableCards[i].GetValueNumberWIthAs();
                }
            }
            return PairValue;
		}

		private int IsTwoPair(Card[] PlayerAndTableCards) {
			int TwoPairValue = -1;
            for (int i = 0; i < PlayerAndTableCards.Length - 1 && TwoPairValue < (POWER + 1); i++)
			{
				if (PlayerAndTableCards[i].CompareTo(PlayerAndTableCards[i + 1]) == 0)
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
				if (PlayerAndTableCards[i].CompareTo(PlayerAndTableCards[i + 1]) == 0) // pair
				{
                    if (PlayerAndTableCards[i + 1].CompareTo(PlayerAndTableCards[i + 2]) == 0) // Three of a Kind
						ThreeOfAKindValue = PlayerAndTableCards[i].GetValueNumberWIthAs();
				}
			}
			return ThreeOfAKindValue;
		}

		
        private int IsStraight (Card[] PlayerAndTableCards){
            if (IsAsStraight(PlayerAndTableCards) > 0) return IsAsStraight(PlayerAndTableCards);
            else return IsNormalStraight(PlayerAndTableCards);
        }

        private int IsNormalStraight (Card[] PlayerAndTableCards) {
            int i, MinimumStraightValueIndex = 0;
            int Straight = 1;
            for (i = 0; i < PlayerAndTableCards.Length - 1 && Straight < 5; i++){
                if (PlayerAndTableCards[i].CompareTo(PlayerAndTableCards[i + 1]) == -1){
                    Straight = Straight + 1;
                }
                else{
                    MinimumStraightValueIndex = i;
                }
            }
            if (Straight < 5) return -1;
            else return MinimumStraightValueIndex;
        }

        private int IsAsStraight(Card[] PlayerAndTableCards) {
            int[] asStraightIndex = new int[5];
            for (int i = 0; i < PlayerAndTableCards.Length; i++)
            {
                int CardValue = PlayerAndTableCards[i].GetValueNumberWIthAs() - 10;
                if (CardValue >= 0)
                {
                    asStraightIndex[CardValue] = asStraightIndex[CardValue] + 1;
                }
            }
            bool ans = true;
            for (int k = 0; k < asStraightIndex.Length && ans; k++)
            {
                if (asStraightIndex[k] == 0) ans = false;
            }
            if (ans) return 10;
            else return -1;
        }

		private int IsFlush (Card[] PlayerAndTableCards) {
            int i;
			int[] FlushColor = new int[4];
			for (i = 0; i < PlayerAndTableCards.Length ; i++)
			{
                FlushColor[PlayerAndTableCards[i].GetSuitNumber() - 1] = FlushColor[PlayerAndTableCards[i].GetSuitNumber() - 1] +1;
			}
            for (i = 0; i < FlushColor.Length; i++){
                if (FlushColor[i]>=5){
                    return 1;
                }
            }
            return -1;
		}

		private int IsFullHouse (Card[] PlayerAndTableCards) {
            int sum = -1;
            if (IsTwoPair(PlayerAndTableCards) > 0 && IsThreeOfAKind(PlayerAndTableCards) > 0) {
                int PairValue = IsTwoPair(PlayerAndTableCards);
                int ThreeOfAKindValue = IsThreeOfAKind(PlayerAndTableCards);
                sum = PairValue + POWER * ThreeOfAKindValue;
            }
            return sum;
		}

        private int IsFourOfAKind(Card[] PlayerAndTableCards) {
            int i;
			int Count = 1;
			for (i = 0; i < PlayerAndTableCards.Length - 1 && Count < 4; i++)
			{
				if (PlayerAndTableCards[i].CompareTo(PlayerAndTableCards[i + 1]) == 0)
				{
					Count = Count + 1;
				}
                else{
                    Count = 1;
                }
	
			}
            if (Count < 4) return -1;
            else return PlayerAndTableCards[i-1].GetValueNumberWIthAs();
		}

		private int IsStraightFlush(Card[] PlayerAndTableCards) {
            if (IsStraight(PlayerAndTableCards) > 0){
                int smallStraightCardValue = IsStraight(PlayerAndTableCards);
                int startStraightIndex = -1;

                // we are looking for the last minimum straight value occurence in our array
                for (int i = 2; i > 0 && startStraightIndex == -1; i--){
                    if (PlayerAndTableCards[i].GetValueNumber() == smallStraightCardValue){
                        startStraightIndex = i;
                    }
                }

                // we check if the straight have the same suit
                bool ans = true;
                for (int k = 0; k < 4 && ans; k++){
                    if (PlayerAndTableCards[startStraightIndex+k].GetSuitNumber() != PlayerAndTableCards[startStraightIndex + k].GetSuitNumber() + 1){
                        ans = false;
                    }
                }
                if (ans) return smallStraightCardValue;
                else return -1;
            }
			return -1;
		}

        private int IsRoyalFlush (Card[] PlayerAndTableCards) {
            if (IsStraightFlush(PlayerAndTableCards) > 0 && IsAsStraight(PlayerAndTableCards) > 0){
                return 1;
            }
            return -1;
        }
		#endregion



	}
}
