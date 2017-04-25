using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Game
{
	public enum Suit
	{
		Hearts,
		Clubs,
		Spades,
		Diamonds
	}
	public enum Value
	{
		Two,
		Three,
		Four,
		Five,
		Six,
		Seven,
		Eight,
		Nine,
		Ten,
		Jack,
		Queen,
		King,
		Ace,
	}
	public class Card
    {
		// TODO: complete - set team member to do this
		#region Properties
		protected Suit CardSuit;
		protected Value CardNumber;
		#endregion

		#region Methods
		public Card(Suit suit, Value num)
		{
			CardSuit = suit;
			CardNumber = num;
		}
		#endregion
	}
}
