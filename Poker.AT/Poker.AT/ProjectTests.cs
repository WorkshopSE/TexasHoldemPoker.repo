﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AT.Bridge;
using AT.Domain;
using NUnitLite;
using System.Drawing;

namespace AT.Tests
{
    public class ProjectTests
    {
        protected TestsBridge bridge;
        [SetUp]
        public void Setup()
        {
            this.bridge = Driver.getBridge();
        }
		[TearDown]
		public void TearDown()
		{
			this.bridge.TearDown();
		}

		public int CreateARoom(int level, string userName, int securityKey, out int? player)
		{
			return bridge.CreateARoom(level, userName, securityKey, out player);
		}

		public string SignUp(string Name, string UserName, string Password)
		{
			return bridge.SignUp(Name, UserName, Password);
		}
		public bool EditProfilePassword(string userName, string oldPassword , string password, int securityKey)
		{
			return bridge.EditProfilePassword(userName, oldPassword, password, securityKey);
		}
		public bool EditProfileUserName(string userName, string newUserName, string password, int securityKey)
		{
			return bridge.EditProfileUserName(userName, newUserName, password, securityKey);
		}
		public bool Logout(string UserName, int securityKey)
		{
			return bridge.Logout(UserName, securityKey);
		}

		public bool Login(string UserName, string Password, out int securityKey)
		{
			return bridge.Login( UserName, Password, out securityKey);
		}

		public string GetProfile(string userName, int securityKey, out string password, out int[] avatar)
		{
			return bridge.GetProfile(userName, securityKey, out password, out avatar);
		}

		public int EnterRoom(int room, string userName, int securityKey, out int? player)
		{
			return bridge.EnterRoom(room, userName, securityKey, out player);
		}

		public double JoinNextHand(string userName, int key, int? player, int seatIndex, double buyIn, out double wallet)
		{
			return bridge.JoinNextHand(userName, key, player, seatIndex, buyIn, out wallet);
		}

		public bool StandUpToSpectate(string userName, int securityKey, int? player)
		{
			return bridge.StandUpToSpectate(userName, securityKey, player);
		}

		public IList<Card> ShuffleCards(Deck TestDeck)
		{
			return bridge.ShuffleCards(TestDeck);
		}

	}
}
