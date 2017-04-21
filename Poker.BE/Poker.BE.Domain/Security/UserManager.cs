using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Security
{
    public class UserManager
    {
        #region Properties
        protected Dictionary <string, string> UsersDictionary;

        #endregion

        #region Methods
        public UserManager (){
            UsersDictionary = new Dictionary <string, string>();
        }

        protected bool AddUser (string userName, string password){
            if ( !CheckExistingUser (userName) && CheckPasswordValidity(password) ){
                UsersDictionary.Add(userName, password);
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
            return (UsersDictionary.ContainsKey(userName));
        }

        
        protected bool CheckPasswordValidity (string password){
            if (password.Length >= 6 ) return true;
            return false;
        }


        #endregion

    }
}
