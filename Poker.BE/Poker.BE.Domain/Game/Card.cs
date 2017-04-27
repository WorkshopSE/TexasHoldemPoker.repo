using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Game
{
	/// <summary>Defiend Card and enum properties (suit and value) 
	/// <remarks>
	/// <author>Gal Wainer</author>
	/// <lastModified>2017-04-26</lastModified>
	/// </remarks>
	public enum Suit
	{
		Hearts,
		Clubs,
		Spades,
		Diamonds,
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
		protected Suit CardSuit { get; }
		protected Value CardNumber { get; }
		protected int ShuffledIndex { get; set; }//used for shuffling cards
		#endregion

		#region Methods
		public Card(Suit suit, Value num)
		{
			CardSuit = suit;
			CardNumber = num;
			ShuffledIndex = 0;
		}
		public void EnumerateCard()
		{
			Random rnd = new Random(DateTime.Now.Millisecond);
			ShuffledIndex = rnd.Next(1, int.MaxValue);
		}
		public int CompareTo(Card CardToCompare)
		{
			if (ShuffledIndex > CardToCompare.ShuffledIndex)
				return 1;
			else if (ShuffledIndex < CardToCompare.ShuffledIndex)
				return -1;
			return 0;
		}
		#endregion
	}
}
