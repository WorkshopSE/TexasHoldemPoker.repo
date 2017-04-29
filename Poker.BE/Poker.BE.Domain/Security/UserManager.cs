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

        #region Constructors
        public UserManager (){
            UsersDictionary = new Dictionary <string, User>();
        }
        #endregion

        #region Methods
        protected bool AddUser (string userName, string password, double sumToDeposit){
            if ( !CheckExistingUser (userName) && CheckPasswordValidity(password) && sumToDeposit > 0 ){
                User UserToAdd = new User (userName, password, sumToDeposit);
                UsersDictionary.Add(userName, UserToAdd);
                return true;
            }
            return false;
        }

        protected bool RemoveUser (string userName){
                return UsersDictionary.Remove(userName);
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

        public User LogIn (string userName, string password){
            if (CheckExistingUser(userName)){ // We check that the user is existing in our DB
               User UserToCheck;
               if (UsersDictionary.TryGetValue(userName, out UserToCheck) ){ // We take the User Object from our DB
                   string GoodPassword = UserToCheck.Password;
                   bool arePasswordMatching = GoodPassword.Equals(password, StringComparison.Ordinal); // We check that the password is correct
                   if (arePasswordMatching) {
                       UserToCheck.Connect();
                       return UserToCheck;
                    }
               }
            }
            return null;
        }

        public bool LogOut (User userToLogout){
            userToLogout.Disconnect();
            return true;
        }

        #endregion

    }
}
