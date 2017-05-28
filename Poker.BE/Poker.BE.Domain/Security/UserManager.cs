using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Poker.BE.Domain.Core;

namespace Poker.BE.Domain.Security
{
	public class UserManager
	{
		#region Constants
		int WEEK_IN_MILISECOND = 1000 * 60 * 60 * 24 * 7;// second*minutes*hours*days
		#endregion

		#region Fields
		private static System.Timers.Timer aTimer;
		#endregion

		#region Properties
		protected Dictionary<string, User> UsersDictionary;

		#endregion

		#region Methods
		public UserManager()
		{
			UsersDictionary = new Dictionary<string, User>();
			aTimer = new System.Timers.Timer();
			aTimer.Interval = WEEK_IN_MILISECOND;
			aTimer.Elapsed += OnTimedEvent;
			aTimer.AutoReset = true;

		}
		//TODO finish implementation @gal
		/// <summary>
		/// once a week the users are divided to new leagues according to their current level.
		/// </summary>
		/// <returns>void?</returns>
		/// <remarks>UC040: update users leagues</remarks>
		/// <param name=""></param>
		/// <see cref="https://docs.google.com/document/d/1OTee6BGDWK2usL53jdoeBOI-1Jh8wyNejbQ0ZroUhcA/edit#heading=h.q38pe5gu8du4"/>
		private void OnTimedEvent(object sender, ElapsedEventArgs e)
		{
			throw new NotImplementedException();
		}

		public bool AddUser(string userName, string password, double sumToDeposit)
		{         //Sign up to the system
			if (!CheckExistingUser(userName) && CheckPasswordValidity(password) && sumToDeposit > 0)
			{
				User UserToAdd = new User(userName, password, sumToDeposit);
				UsersDictionary.Add(userName, UserToAdd);
				return true;
			}
			return false;
		}

		public bool RemoveUser(string userName)
		{
			return UsersDictionary.Remove(userName);    //returns true if found and deleted

		}

		protected bool CheckExistingUser(string userName)
		{
			if (userName != null)
				return (UsersDictionary.ContainsKey(userName));
			return false;
		}

		protected bool CheckPasswordValidity(string password)
		{
			if (password.Length >= 6) return true;
			return false;
		}

		public User LogIn(string userName, string password)
		{
			if (CheckExistingUser(userName))
			{ // We check that the user is existing in our DB
				User UserToCheck;
				if (UsersDictionary.TryGetValue(userName, out UserToCheck))
				{ // We take the User Object from our DB
					string GoodPassword = UserToCheck.Password;
					bool arePasswordMatching = GoodPassword.Equals(password, StringComparison.Ordinal); // We check that the password is correct
					if (arePasswordMatching)
					{
						UserToCheck.Connect();
						return UserToCheck;
					}
				}
			}
			return null;
		}

		public bool LogOut(User userToLogout)
		{
			userToLogout.Disconnect();
			return true;
		}

		public bool EditProfile(string oldUserName, string newUserName, string newPassword, string newAvatar)
		{
			if (!CheckExistingUser(oldUserName)) //Check user's existance
			{
				return false;
			}
			User userToUpdate = UsersDictionary[oldUserName];
			RemoveUser(oldUserName);

			if (CheckExistingUser(newUserName) || !CheckPasswordValidity(newPassword))  //Check new username and password validation
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
