using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poker.BE.Domain.Game;


namespace Poker.BE.Domain.Core
{
    public class User : AbstractUser
    {
        #region Methods
        public  User (string userName, string password, double sumToDeposit){
            UserName = userName;
            Password = password;
            UserBank = new Bank (sumToDeposit);
            IsConnected = true;
            ActiveUsersPlayer = new List<Player>();
        }

        public void AddPlayer (Player PlayerToAdd){
            ActiveUsersPlayer.Add(PlayerToAdd);
        }

        public void Connect(){
            IsConnected = true;
        }

        public void Disconnect(){
            IsConnected = false;
        }

        #endregion   
    } 
}
