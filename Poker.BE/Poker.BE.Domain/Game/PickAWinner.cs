using System;
using System.Collections.Generic;

namespace Poker.BE.Domain.Game
{
    public class PickAWinner
    {

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

		private Player IsHighCard(Card[] PlayerAndTableCards, ICollection<Player> players) {
            return null;
		}

		private bool IsPair(Card[] PlayerAndTableCards) {
            return true;
		}

		private bool IsTwoPair(Card[] PlayerAndTableCards) {
			return true;
		}

		private bool IsThreeOfAKind(Card[] PlayerAndTableCards) {
			return true;
		}

		private bool IsStraight (Card[] PlayerAndTableCards) {
			return true;
		}

		private bool IsFlush (Card[] PlayerAndTableCards) {
			return true;
		}

		private bool IsFullHouse (Card[] PlayerAndTableCards) {
			return true;
		}

        private bool IsFourOfAKind(Card[] PlayerAndTableCards) {
			return true;
		}

		private bool IsStraightFlush(Card[] PlayerAndTableCards) {
			return true;
		}

        private bool IsRoyalFlush (Card[] PlayerAndTableCards) {
			return true;
		}

		#endregion



	}
}
