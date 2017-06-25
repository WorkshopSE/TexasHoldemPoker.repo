using Poker.BE.CrossUtility.Exceptions;
using System;
using System.Collections.Generic;

namespace Poker.BE.Domain.Game
{
    public class League
    {
        #region Constants
        public const int MAX_LEVEL = 100;
        public const int MIN_LEVEL = 1;
        #endregion

        #region Properties
        public ICollection<Room> Rooms { get; set; }
        public int MaxLevel { get; set; }
        public int MinLevel { get; set; }
        public bool IsFull { get; set; }
        #endregion

        #region Constructors
        public League()
        {
            Rooms = new List<Room>();
            MaxLevel = MAX_LEVEL;
            MinLevel = MIN_LEVEL;
        }
        #endregion

        #region Methods
        public void RemoveRoom(Room room)
        {
            Rooms.Remove(room);
        }

        #endregion

    }
}
