using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poker.BE.Domain.Core;
using Poker.BE.Domain.Utility.Exceptions;

namespace Poker.BE.Domain.Security
{
	public class UserManager
	{
		#region Constants
		public const int MINIMUM_PASSWORD_LENGTH = 6;
		#endregion

		#region Properties
		public IDictionary<string, User> UsersDictionary;
		#endregion

		#region Methods
		public UserManager()
		{
			UsersDictionary = new Dictionary<string, User>();
		}

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
			UsersDictionary.Add(userName, UserToAdd);
			return UserToAdd;
		}


		public bool RemoveUser(string userName)
		{
			return UsersDictionary.Remove(userName);    //returns true if found and deleted
		}

		public bool IsUserExists(string userName)
		{
			if (userName != null)
			{
				return (UsersDictionary.ContainsKey(userName));
			}
			return false;
		}

		protected bool IsPasswordValid(string password)
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
			if (UsersDictionary.TryGetValue(userName, out UserToCheck))
			{ //Note: We take the User Object from our DB
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
			User userToUpdate = UsersDictionary[oldUserName];
			RemoveUser(oldUserName);

			if (IsUserExists(newUserName) || !IsPasswordValid(newPassword))  //Check new username and password validation
			{
				UsersDictionary.Add(oldUserName, userToUpdate);
				return false;
			}

			userToUpdate.UserName = newUserName;
			userToUpdate.Password = newPassword;
			userToUpdate.Avatar = newAvatar;
			UsersDictionary.Add(newUserName, userToUpdate);
			return true;
		}
		#endregion

	}
}
