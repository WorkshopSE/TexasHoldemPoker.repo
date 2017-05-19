using System;
using System.Collections.Generic;

namespace Poker.BE.Domain.Game
{
    public class PickAWinner
    {

		#region Fields
		private ICollection<Player> Players;
		private Card[] ChoosenCards;
		#endregion

		#region Properties
		public Player Winner { get; private set; }
		#endregion

		#region Constructors
        public PickAWinner (ICollection<Player> Players, Card[] ChoosenCards)
		{
            this.Players = Players;
            this.ChoosenCards = ChoosenCards;
		}
		#endregion

		#region Methods
        private Player GetWinner(){
            return null;
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
