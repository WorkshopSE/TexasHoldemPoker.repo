using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poker.BE.Domain.Core;
using Poker.BE.Domain.Utility.Exceptions;

namespace Poker.BE.Domain.Security
{
    /// <summary>
    /// Singleton class responsible for managing all the users
    /// </summary>
    /// <remarks>
    /// change-log for 2017-06-08_08-57-24 by Idan:
    ///     - converting this class to singleton
    ///     - not allowing 'protected' functions, only private
    ///     - moving constructor to be private, at 'constructor' region
    /// </remarks>
	public sealed class UserManager
    {
        #region Constants
        public const int MINIMUM_PASSWORD_LENGTH = 6;
        #endregion

        #region Fields
        private IDictionary<string, User> usersDictionary;
        #endregion

        #region Singleton Constructor
        // Note: for c# implementation
        static UserManager() { }

        // Note: Singleton private constructor
        private UserManager()
        {
            usersDictionary = new Dictionary<string, User>();
        }

        private static readonly UserManager _instance = new UserManager();

        public static UserManager Instance { get { return _instance; } }
        #endregion

        #region Methods
        public User AddUser(string userName, string password, double sumToDeposit)
        {
            if (IsUserExists(userName))
            {
                throw new UserNameTakenException();
            }
            if (!IsPasswordValid(password))
            {
                throw new InvalidPasswordException();
            }
            if (sumToDeposit < 0)
            {
                throw new InvalidDepositException();
            }
            User UserToAdd = new User(userName, password, sumToDeposit);
            usersDictionary.Add(userName, UserToAdd);
            return UserToAdd;
        }

        /// <summary>
        /// remove the user? summery missing
        /// </summary>
        /// <param name="userName">user name</param>
        /// <returns>true if found and deleted</returns>
        public bool RemoveUser(string userName)
        {
            return usersDictionary.Remove(userName);    
        }

        public bool IsUserExists(string userName)
        {
            if (userName != null)
            {
                return (usersDictionary.ContainsKey(userName));
            }
            return false;
        }

        private bool IsPasswordValid(string password)
        {
            if (password.Length >= MINIMUM_PASSWORD_LENGTH) { return true; }
            return false;
        }

        public User LogIn(string userName, string password)
        {
            if (!IsUserExists(userName))
            {
                throw new UserNotFoundException();
            }
            User UserToCheck;
            if (usersDictionary.TryGetValue(userName, out UserToCheck))
            {
                /**
                 * Note: We take the User Object from our DB
                 *      - answer - no need at this point of coding.
                 *      - UNDONE - change this.
                 * */
                string GoodPassword = UserToCheck.Password;
                bool arePasswordMatching = GoodPassword.Equals(password, StringComparison.Ordinal); // We check that the password is correct
                if (!arePasswordMatching)
                {
                    throw new IncorrectPasswordException();
                }
                UserToCheck.Connect();
                return UserToCheck;
            }
            return null;
        }

        public bool LogOut(User userToLogout)
        {
            if (!IsUserExists(userToLogout.UserName))
            {
                throw new UserNotFoundException();
            }
            userToLogout.Disconnect();
            return true;
        }

        public bool EditProfile(string oldUserName, string newUserName, string newPassword, string newAvatar)
        {
            if (!IsUserExists(oldUserName)) //Check user's existance
            {
                return false;
            }
            User userToUpdate = usersDictionary[oldUserName];
            RemoveUser(oldUserName);

            if (IsUserExists(newUserName) || !IsPasswordValid(newPassword))  //Check new username and password validation
            {
                usersDictionary.Add(oldUserName, userToUpdate);
                return false;
            }

            userToUpdate.UserName = newUserName;
            userToUpdate.Password = newPassword;
            userToUpdate.Avatar = newAvatar;
            usersDictionary.Add(newUserName, userToUpdate);
            return true;
        }

        /// <summary>
        /// Clears all class resources
        /// </summary>
        public void Clear()
        {
            usersDictionary.Clear();
        }
        #endregion

    }
}
