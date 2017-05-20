using System;
using System.Collections.Generic;

namespace Poker.BE.Domain.Game
{
    public class PickAWinner
    {

		#region Constants
		public const int POWER = 14;
        public const int FIVEBESTCARDS = 5;
        public const int NUMBERORDER = 10;
		public const int FALSERESULT = -1;

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
            bool winnerFounded = false, severalWinners = false;
            int numberOfPlayers = PlayerSevenCards.Count, k=0, ans=0, winnerIndex=0;
            int[] arrayWinners = new int[numberOfPlayers];
            Player winner = null;
            for (int i = NUMBERORDER; i > 0 && !winnerFounded; i--){
                foreach ( Player player in Players){
                    ans = CheckPlayerByOrder(player, i);
                    arrayWinners[k] = ans;
                    k++;
                    if (ans != FALSERESULT) winnerFounded = true;
                }
                if(winnerFounded){
                    for (int j = 0; j < arrayWinners.Length - 1; j++){
                        int maxResult = arrayWinners[i];
                        if (arrayWinners[i+1] > maxResult){
                            maxResult = arrayWinners[i];
                            winnerIndex = i;
                            severalWinners = false;
                        }
                        else{
                            severalWinners = true;
                        }
                    }
                    if (!severalWinners){
                        winner = getPlayerByDictionnaryAndIndex(winnerIndex);
                    }
                }
                else{
                    k = 0;
                }
            }
            return winner;
        }

        private int CheckPlayerByOrder(Player player, int numberRound){
            Card[] playerCardArray = null;
            bool isThereACorrectCardArray = PlayerSevenCards.TryGetValue(player, out playerCardArray);

            if (isThereACorrectCardArray){
				switch (numberRound){
                    case 1: return HighFiveCardValue(playerCardArray);
                    case 2: return IsPair(playerCardArray);
					case 3: return IsTwoPair(playerCardArray);
                    case 4: return IsThreeOfAKind(playerCardArray);
                    case 5: return IsStraight(playerCardArray);
                    case 6: return IsFlush (playerCardArray);
                    case 7: return IsFullHouse (playerCardArray);
                    case 8: return IsFourOfAKind(playerCardArray);
                    case 9: return IsStraightFlush(playerCardArray);
                    case 10: return IsRoyalFlush(playerCardArray);
					default:  return 0;
			    }
			}
            return FALSERESULT;
        }

        private Player getPlayerByDictionnaryAndIndex (int playerIndex){
            Player winner = null;
            int currentIndex = 0;
            foreach (Player player in Players){
                if (currentIndex == playerIndex){
                    winner = player;
                }
                currentIndex = currentIndex + 1;
            }
            return winner;
        }

        private void InitializeDictionnary(){
            PlayerSevenCards = new Dictionary<Player, Card[]>();
            foreach (Player player in Players){
                Card[] PlayerSevenCardArray = new Card[7];
                for (int i = 0; i < 2; i++){
                    PlayerSevenCardArray[i] = player.PrivateCards[i];
                }
                for (int k = 0; k < FIVEBESTCARDS; k++){
                    PlayerSevenCardArray[k + 2] = FifthHandCard[k];
                }
                SortSevenCardsByValue(PlayerSevenCardArray);
                PlayerSevenCards.Add(player, PlayerSevenCardArray);
            }
        }

        private void SortSevenCardsByValue (Card[] CardArray){
            Card[] SortedArrayCard = new Card[7];
            for (int i = 0; i < CardArray.Length; i++){
                for (int k = i+1; k < FIVEBESTCARDS + 1; k++){
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
        // NumberOrderIndex = 1
		private int HighFiveCardValue (Card[] PlayerAndTableCards) {
            int i=0;
            int exponant = FIVEBESTCARDS -1;
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
            return (int)sum;
		}

		// NumberOrderIndex = 2
		private int IsPair(Card[] PlayerAndTableCards) {
            int PairValue = FALSERESULT;
            for (int i = 0; i < PlayerAndTableCards.Length - 1 && PairValue== FALSERESULT ; i++) {
                if (PlayerAndTableCards[i].CompareTo(PlayerAndTableCards[i+1]) == 0){
                    PairValue = PlayerAndTableCards[i].GetValueNumberWIthAs();
                }
            }
            return PairValue;
		}

		// NumberOrderIndex = 3
		private int IsTwoPair(Card[] PlayerAndTableCards) {
			int TwoPairValue = FALSERESULT;
            for (int i = 0; i < PlayerAndTableCards.Length - 1 && TwoPairValue < (POWER + 1); i++)
			{
				if (PlayerAndTableCards[i].CompareTo(PlayerAndTableCards[i + 1]) == 0)
				{
                    if (TwoPairValue == FALSERESULT ){ // the value of the first pair x * POWER^0
						TwoPairValue = PlayerAndTableCards[i].GetValueNumberWIthAs();
					}
                    else{ // the value of the second pair - TwoPairValue + (x * POWER^1)
						TwoPairValue = TwoPairValue + (POWER * PlayerAndTableCards[i].GetValueNumberWIthAs());
                    }
				}
			}
            if (TwoPairValue < (POWER + 1)) TwoPairValue = FALSERESULT;
			return TwoPairValue;

		}

		// NumberOrderIndex = 4
		private int IsThreeOfAKind(Card[] PlayerAndTableCards) {
			int ThreeOfAKindValue = FALSERESULT;
			for (int i = 0; i < PlayerAndTableCards.Length - 2 && ThreeOfAKindValue == FALSERESULT; i++)
			{
				if (PlayerAndTableCards[i].CompareTo(PlayerAndTableCards[i + 1]) == 0) // pair
				{
                    if (PlayerAndTableCards[i + 1].CompareTo(PlayerAndTableCards[i + 2]) == 0) // Three of a Kind
						ThreeOfAKindValue = PlayerAndTableCards[i].GetValueNumberWIthAs();
				}
			}
			return ThreeOfAKindValue;
		}

		// NumberOrderIndex = 5
		private int IsStraight (Card[] PlayerAndTableCards){
            if (IsAsStraight(PlayerAndTableCards) > 0) return IsAsStraight(PlayerAndTableCards);
            else return IsNormalStraight(PlayerAndTableCards);
        }

        private int IsNormalStraight (Card[] PlayerAndTableCards) {
            int i, MinimumStraightValueIndex = 0;
            int Straight = 1;
            for (i = 0; i < PlayerAndTableCards.Length - 1 && Straight < 5; i++){
                if (PlayerAndTableCards[i].CompareTo(PlayerAndTableCards[i + 1]) == FALSERESULT){
                    Straight = Straight + 1;
                }
                else{
                    MinimumStraightValueIndex = i;
                }
            }
            if (Straight < 5) return FALSERESULT;
            else return MinimumStraightValueIndex;
        }

        private int IsAsStraight(Card[] PlayerAndTableCards) {
            int[] asStraightIndex = new int[FIVEBESTCARDS];
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
            else return FALSERESULT;
        }

		// NumberOrderIndex = 6
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
            return FALSERESULT;
		}

		// NumberOrderIndex = 7
		private int IsFullHouse (Card[] PlayerAndTableCards) {
            int sum = FALSERESULT;
            if (IsTwoPair(PlayerAndTableCards) > 0 && IsThreeOfAKind(PlayerAndTableCards) > 0) {
                int PairValue = IsTwoPair(PlayerAndTableCards);
                int ThreeOfAKindValue = IsThreeOfAKind(PlayerAndTableCards);
                sum = PairValue + POWER * ThreeOfAKindValue;
            }
            return sum;
		}

		// NumberOrderIndex = 8
        private int IsFourOfAKind(Card[] PlayerAndTableCards) {
            int i;
			int Count = 1;
			for (i = 0; i < PlayerAndTableCards.Length - 1 && Count < FIVEBESTCARDS - 1; i++)
			{
				if (PlayerAndTableCards[i].CompareTo(PlayerAndTableCards[i + 1]) == 0)
				{
					Count = Count + 1;
				}
                else{
                    Count = 1;
                }
	
			}
            if (Count < 4) return FALSERESULT;
            else return PlayerAndTableCards[i-1].GetValueNumberWIthAs();
		}

		// NumberOrderIndex = 9
		private int IsStraightFlush(Card[] PlayerAndTableCards) {
            if (IsStraight(PlayerAndTableCards) > 0){
                int smallStraightCardValue = IsStraight(PlayerAndTableCards);
                int startStraightIndex = FALSERESULT;

                // we are looking for the last minimum straight value occurence in our array
                for (int i = 2; i > 0 && startStraightIndex == FALSERESULT; i--){
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
                else return FALSERESULT;
            }
			return FALSERESULT;
		}

		// NumberOrderIndex = 10
        private int IsRoyalFlush (Card[] PlayerAndTableCards) {
            if (IsStraightFlush(PlayerAndTableCards) > 0 && IsAsStraight(PlayerAndTableCards) > 0){
                return 1;
            }
            return FALSERESULT;
        }
		#endregion

	}
}
