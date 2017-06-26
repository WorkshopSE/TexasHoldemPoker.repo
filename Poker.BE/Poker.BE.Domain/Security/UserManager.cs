using System;
using System.Collections.Generic;
using System.Linq;
using Poker.BE.Domain.Core;
using Poker.BE.CrossUtility.Exceptions;

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

        #region Properties
        public IDictionary<string, User> Users { get; set; }

        public IDictionary<string, User> ConnectedUsers
        {
            get
            {
                var result = from user in Users
                             where user.Value.IsConnected
                             select user;
                return result.ToDictionary(x => x.Key, x => x.Value);
            }
        }

        public Random SecurityKeyGenerator { get; set; }
        #endregion

        #region Singleton Constructor
        // Note: for c# implementation
        static UserManager() { }

        // Note: Singleton private constructor
        private UserManager()
        {
            Users = new Dictionary<string, User>();
            SecurityKeyGenerator = new Random();
        }

        private static readonly UserManager _instance = new UserManager();

        public static UserManager Instance { get { return _instance; } }
        #endregion

        #region Private Functions
        private bool IsPasswordValid(string password, out string reason)
        {
            reason = "";
            if (password.Length < MINIMUM_PASSWORD_LENGTH)
            {
                reason = "password length must be over " + MINIMUM_PASSWORD_LENGTH;
                return false;
            }

            return true;
        }
        #endregion

        public static bool SecurityCheck(int? securityKey, User user)
        {
            if (!securityKey.HasValue)
                throw new SecurityException("Security key missing.");

            if (!user.IsSecure(securityKey.Value))
                throw new SecurityException("Security key violation: key corrupted or faked.");

            return true;
        }

        #region Methods
        public User AddUser(string userName, string password, double sumToDeposit)
        {
            if (Users.ContainsKey(userName))
            {
                throw new UserNameTakenException(string.Format("User name: {0} is taken, please try again",
                        userName));
            }

            String reason;
            if (!IsPasswordValid(password, out reason))
            {
                throw new InvalidPasswordException(reason + " please try again");
            }

            if (sumToDeposit < 0)
            {
                throw new InvalidDepositException("Deposit amount must be positive, please try again.");
            }

            User userToAdd = new User(userName, password, sumToDeposit);
            Users.Add(userName, userToAdd);

            return userToAdd;
        }

        public User Login(string userName, string password)
        {
            User user;
            if (!Users.TryGetValue(userName, out user))
            {
                throw new UserNotFoundException("User name: " + userName + " not found, please check for typing mistakes.");
            }

            if (!password.Equals(password, StringComparison.Ordinal))
            {
                throw new IncorrectPasswordException("Incorrect password entered. Please try again");
            }

            user.Connect(SecurityKeyGenerator.Next());

            return user;
        }

        public bool Logout(User userToLogout)
        {
            if (!userToLogout.IsConnected)
            {
                return false;
            }

            userToLogout.Disconnect();

            return true;
        }

        public void EditProfile(User user, string newUserName, string newPassword, byte[] newAvatar)
        {
            Users.Remove(user.UserName);

            user.UserName = newUserName;
            user.Password = newPassword;
            user.Avatar = newAvatar;

            Users.Add(newUserName, user);
        }

        /// <summary>
        /// Clears all class resources
        /// </summary>
        public void Clear()
        {
            Users.Clear();
        }
        #endregion

    }
}
