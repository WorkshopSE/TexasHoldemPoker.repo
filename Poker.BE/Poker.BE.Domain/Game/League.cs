using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Game
{
	public class League : IComparable
	{
		#region Constants
		public const int MAX_LEVEL = 100;
		public const int MIN_LEVEL = 1;
		public const int SMALLER = -1;
		public const int LARGER = 1;
		public const int EQUALS = 0;
		public enum LeagueType
		{
			Unknown = 0,
			OneStar = 1,
			TwoStars = 2,
			ThreeStars = 3,
			FourStars = 4,
			FiveStars = 5,
			SixStars = 6
		}
		#endregion

		#region Fields

		#endregion

		#region Properties
		public LeagueType Type { get; }
		public ICollection<Room> Rooms { get; set; }
		public int MaxLevel { get; set; }
		public int MinLevel { get; set; }
		public bool IsFull { get; internal set; }
		#endregion

		#region Constructors
		public League()
		{
			Rooms = new List<Room>();
			MaxLevel = MAX_LEVEL;
			MinLevel = MIN_LEVEL;
		}
		#endregion

		#region Private Functions

		#endregion

		#region Methods
		public void RemoveRoom(Room room)
		{
			// TODO
			throw new NotImplementedException();
		}

		public int CompareTo(object obj)
		{
			if (this.Type > ((League)obj).Type)
				return LARGER;
			else if (this.Type == ((League)obj).Type)
				return EQUALS;
			return SMALLER;
		}

		#endregion

	}
}
