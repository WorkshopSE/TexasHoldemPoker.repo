﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker.BE.Domain.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Game.Tests
{
    [TestClass()]
    public class RoomTests
    {

        #region Setup
        public TestContext TestContext { get; set; }
        private Room room;
        private Player roomCreator;

        [TestInitialize]
        public void Before()
        {
            roomCreator = new Player();
            room = new Room(roomCreator);
        }

        [TestCleanup]
        public void After()
        {
            room = null;
        }
        #endregion

        [TestMethod]
        public void RoomTest()// (:Player, :Preferences)
        {
            //Arrange
            var expPlayer = new Player();
            var preferences = new GamePreferences();
            var expConfig = new GameConfig();

            //Act
            var actual = new Room(expPlayer, preferences);

            //Assert
            Assert.AreEqual(1, actual.Players.Count, "one player in new room");
            Assert.AreEqual(expPlayer, actual.Players.First(), "the creator is first");
            Assert.AreEqual(Player.State.Passive, expPlayer.CurrentState, "the creator is passive");
            Assert.AreEqual(1, actual.PassivePlayers.Count, "one passive player");
            Assert.AreEqual(0, actual.ActivePlayers.Count, "zero active players");
            Assert.IsTrue(actual.PassivePlayers.Contains(expPlayer), "the creator found in passive players collection at room");
            Assert.AreEqual(null, actual.CurrentHand, "current hand null");
            Assert.AreEqual(false, actual.IsTableFull, "table not full");

            // 8 Configurations of game-config
            Assert.AreEqual(expConfig.BuyInCost, actual.BuyInCost, "default buy in");
            Assert.AreEqual(true, actual.IsSpactatorsAllowd, "default spectators allowed");
            Assert.AreEqual(expConfig.MaxNumberOfActivePlayers, actual.MaxNumberOfActivePlayers, "max active players default");
            Assert.AreEqual(expConfig.MaxNumberOfPlayers, actual.MaxNumberOfPlayers, "default max players number");
            Assert.AreEqual(expConfig.MinimumBet, actual.MinimumBet, "minimum bet default");
            Assert.AreEqual(expConfig.MinNumberOfPlayers, actual.MinNumberOfPlayers, "min players default");
            Assert.AreEqual(expConfig.Name, actual.Name, "default name");
            // TODO: idan - add assert for default game preferences.

        }

        [TestMethod()]
        public void RoomTest2() // (:Player)
        {
            //Arrange
            var player = new Player();
            var expPlayer = player;
            var expConfig = new GameConfig();

            //Act
            var actual = new Room(player);

            //Assert
            Assert.AreEqual(1, actual.Players.Count, "one player in new room");
            Assert.AreEqual(expPlayer, actual.Players.First(), "the creator is first");
            Assert.AreEqual(Player.State.Passive, player.CurrentState, "the creator is passive");
            Assert.AreEqual(1, actual.PassivePlayers.Count, "one passive player");
            Assert.AreEqual(0, actual.ActivePlayers.Count , "zero active players");
            Assert.IsTrue(actual.PassivePlayers.Contains(expPlayer), "the creator found in passive players collection at room");
            Assert.AreEqual(null, actual.CurrentHand, "current hand null");
            Assert.AreEqual(false , actual.IsTableFull, "table not full");

            // 8 Configurations of game-config
            Assert.AreEqual(expConfig.BuyInCost, actual.BuyInCost, "default buy in");
            Assert.AreEqual(true, actual.IsSpactatorsAllowd, "default spectators allowed");
            Assert.AreEqual(expConfig.MaxNumberOfActivePlayers, actual.MaxNumberOfActivePlayers, "max active players default");
            Assert.AreEqual(expConfig.MaxNumberOfPlayers, actual.MaxNumberOfPlayers, "default max players number");
            Assert.AreEqual(expConfig.MinimumBet, actual.MinimumBet, "minimum bet default");
            Assert.AreEqual(expConfig.MinNumberOfPlayers, actual.MinNumberOfPlayers, "min players default");
            Assert.AreEqual(expConfig.Name, actual.Name, "default name");
            // TODO: idan - add assert for default game preferences.
        }

        [TestMethod()]
        public void RoomTest3() // (:Player, :GameConfig)
        {
            //Arrange
            var player = new Player();
            const string expName = "test room 3";
            const double expBuyinCost = 50.2;
            GamePreferences expGamePreferences = new GamePreferences();
            const bool expIsSpecAllowed = false;
            const int expNActive = 8;
            const int expMaxNumberPlayers = 9;
            const double expMinBet = 8.6;
            const int expMinPlayers = 3;
            var confing = new GameConfig() {
                BuyInCost = expBuyinCost,
                GamePrefrences = expGamePreferences,
                IsSpactatorsAllowed = expIsSpecAllowed,
                MaxNumberOfActivePlayers = expNActive,
                MaxNumberOfPlayers = expMaxNumberPlayers,
                MinimumBet = expMinBet,
                MinNumberOfPlayers = expMinPlayers,
                Name = expName,
            };

            //Act
            var actual = new Room(player, confing);

            //Assert
            Assert.AreEqual(expName, actual.Name);
            Assert.AreEqual(expNActive, actual.MinNumberOfPlayers);
        }

        [TestMethod()]
        public void JoinPlayerToTableTest()
        {
            // TODO
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void RemovePlayerTest()
        {
            // TODO
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void ClearAllTest()
        {
            // TODO
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void CreatePlayerTest()
        {
            // TODO
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void StartNewHandTest()
        {
            // TODO
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void TakeChairTest()
        {
            // TODO
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void LeaveChairTest()
        {
            // TODO
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void SendMessageTest()
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}