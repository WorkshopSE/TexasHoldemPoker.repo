using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poker.BE.Domain.Game;

namespace Poker.BE.Domain.Core
{
    public abstract class AbstractUser
    {

        #region Properties
        public string UserName { get; set;}
        public string Password { get; set; }
        public string Avatar { get; set; }
        protected Bank UserBank { get; set; }
        protected bool IsConnected { get; set; }
        protected ICollection<Player> ActiveUsersPlayer { get; set; }
        #endregion



    }
}
