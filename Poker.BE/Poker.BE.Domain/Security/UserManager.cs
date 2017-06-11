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
		private IDictionary<string, User> _usersCache;
		private bool _isUpdated = false;
		#endregion

		#region Properties
		public IDictionary<string, User> Users
		{
			get
			{
				return _isUpdated ? _usersCache : RetrieveUsers();
			}
		}

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

		#endregion

		#region Singleton Constructor
		// Note: for c# implementation
		static UserManager() { }

		// Note: Singleton private constructor
		private UserManager()
		{
			_usersCache = new Dictionary<string, User>();
		}

		private static readonly UserManager _instance = new UserManager();

		public static UserManager Instance { get { return _instance; } }
		#endregion

		#region Private Functions
		private IDictionary<string, User> RetrieveUsers()
		{
			// undone
			_isUpdated = true;
			return _usersCache;
		}

		private void StoreUsers(IDictionary<string, User> value)
		{
			// undone
			_usersCache = value;
		}
		#endregion

		#region Methods
		public User AddUser(string userName, string password, double sumToDeposit)
		{
			if (IsUserExists(userName))
			{
				throw new UserNameTakenException(string.Format("the user name: {0} is taken, please try again",
						userName));
			}
			String reason;
			if (!IsPasswordValid(password, out reason))
			{
				throw new InvalidPasswordException(reason + " please try again");
			}

			if (sumToDeposit < 0)
			{
				throw new InvalidDepositException("deposit amount must be positive. Please try again");
			}

			User userToAdd = new User(userName, password, sumToDeposit);
			_usersCache.Add(userName, userToAdd);
			return userToAdd;
		}

		/// <summary>
		/// remove the user? summery missing
		/// </summary>
		/// <param name="userName">user name</param>
		/// <returns>true if found and deleted</returns>
		public bool RemoveUser(string userName)
		{
			return _usersCache.Remove(userName);
		}

		public bool IsUserExists(string userName)
		{
			if (userName != null)
			{
				return (_usersCache.ContainsKey(userName));
			}
			return false;
		}

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

		public User LogIn(string userName, string password)
		{
			if (!IsUserExists(userName))
			{
				throw new UserNotFoundException("User not found. Please try again od sign up");
			}

			User UserToCheck;
			if (_usersCache.TryGetValue(userName, out UserToCheck))
			{
				/**
                 * Note: We take the User Object from our DB
                 *      - answer - no need at this point of coding.
                 *      - UNDONE - change this.
                 * */
				string GoodPassword = UserToCheck.Password;

				// We check that the password is correct
				bool arePasswordMatching = GoodPassword.Equals(password, StringComparison.Ordinal);
				if (!arePasswordMatching)
				{
					throw new IncorrectPasswordException("Incorrect password entered. Please try again");
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
				throw new UserNotFoundException("user " + userToLogout.UserName + " not found. Can't log out");
			}

			userToLogout.Disconnect();
			RemoveUser(userToLogout.UserName);

			return true;
		}

		public bool EditProfile(string oldUserName, string newUserName, string newPassword, string newAvatar)
		{
			//Check user's existence
			if (!IsUserExists(oldUserName))
			{
				return false;
			}
			User userToUpdate = _usersCache[oldUserName];
			RemoveUser(oldUserName);

			// New user-name and password validation
			string notValidReason;
			if (IsUserExists(newUserName) || !IsPasswordValid(newPassword, out notValidReason))
			{
				_usersCache.Add(oldUserName, userToUpdate);
				return false;
			}

			userToUpdate.UserName = newUserName;
			userToUpdate.Password = newPassword;
			userToUpdate.AvatarImage = newAvatar;

			_usersCache.Add(newUserName, userToUpdate);
			return true;
		}

		/// <summary>
		/// Clears all class resources
		/// </summary>
		public void Clear()
		{
			_usersCache.Clear();
		}
		#endregion

	}
}
