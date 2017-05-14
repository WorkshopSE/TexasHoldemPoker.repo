using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Game
{
    public class League
    {
        // TODO: complete - set team member to do this - Gal?
        #region Constants
        public const int MAX_LEVEL = 100;
        public const int MIN_LEVEL = 1;
        #endregion

        #region Fields
        #endregion

        #region Properties
        public ICollection<Room> Rooms { get; set; }
        public int MaxLevel { get; set; }
        public int MinLevel { get; set; }
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
        #endregion

    }
}
