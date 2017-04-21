using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poker.BE.Domain.Core;

namespace Poker.BE.Domain.Security
{
    public class UserManager
    {
        #region Properties
        protected Dictionary <string, User> UsersDictionary;

        #endregion

        #region Methods
        public UserManager (){
            UsersDictionary = new Dictionary <string, User>();
        }

        protected bool AddUser (string userName, string password, double sumToDeposit){
            if ( !CheckExistingUser (userName) && CheckPasswordValidity(password) && sumToDeposit > 0 ){
                User UserToAdd = new User (userName, password, sumToDeposit);
                UsersDictionary.Add(userName, UserToAdd);
                return true;
            }
            return false;
        }

        protected bool RemoveUser (string userName){
            if (CheckExistingUser (userName)){
                UsersDictionary.Remove(userName);
                return true;
            }
            return false;
        }

        protected bool CheckExistingUser (string userName){
            if (userName != null)
                return (UsersDictionary.ContainsKey(userName));
            return false;
        }

        
        protected bool CheckPasswordValidity (string password){
            if (password.Length >= 6 ) return true;
            return false;
        }


        #endregion

    }
}
