﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AT.Domain;

namespace AT.Bridge
{
    class Real : TestsBridge
    {
		public bool Logout(string UserName, string Password)
		{
			throw new NotImplementedException();
		}
		public bool Login(string UserName, string Password)
		{
			throw new NotImplementedException();
		}

		public IList<Card> ShuffleCards(Deck TestDeck)
		{
			throw new NotImplementedException();
		}

		public User SignUp(string Name, string UserName, string Password)
		{
			throw new NotImplementedException();
		}

		//Here goes the Adapter implementation - for later use!
		public int testCase1(int someParam)
        {
            throw new NotImplementedException();
        }

        public string testCase2(string someParam)
        {
            throw new NotImplementedException();
        }

		public void EditProfilePassword(User User, string Password)
		{
			throw new NotImplementedException();
		}

		public void EditProfileEmail(User User, string Email)
		{
			throw new NotImplementedException();
		}

		public Image EditProfileAvatar(Image TestUserImage)
		{
			throw new NotImplementedException();
		}
	}
}
