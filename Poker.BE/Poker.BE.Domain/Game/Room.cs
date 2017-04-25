using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Game
{

    /// <summary> Defined the room that the players are playing at the game </summary>
    /// <remarks>
    /// <author>Idan Izicovich</author>
    /// <lastModified>2017-04-25</lastModified>
    /// </remarks>
    public class Room
    {
        #region Fields
        private ICollection<Player> ActiveAndPassivePalyers;
        #endregion

        #region Properties
        public ICollection<Chair> Chairs { get; }
        public Hand CurrentHand { get; }
        public GamePreferences GamePrefrences { get; set; }
        #endregion

        #region Methods
        public void StartNewHand()
        {
            //TODO
        }


        public void SendMessage()
        { 
            //TODO
        }
        #endregion


    }//class
}
