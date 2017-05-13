using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Game
{
    public class League
    {
        public ICollection<Room> Rooms { get; set; }

        // TODO: complete - set team member to do this
        internal void RemoveRoom(Room room)
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}
