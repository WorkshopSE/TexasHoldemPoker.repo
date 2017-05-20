using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            Assert.AreEqual(true, actual.IsSpactatorsAllowed, "default spectators allowed");
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
            Assert.AreEqual(true, actual.IsSpactatorsAllowed, "default spectators allowed");
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
            #region Arrange
            var expPlayer = new Player();
            const string expName = "test room 3";
            const bool expIsSpecAllowed = false;
            GamePreferences expGamePreferences = new GamePreferences();
            const int expMinPlayers = 3;

            // changed parameters
            const double insertBuyinCost = 50.2;
            const int insertNActive = 8;
            const int insertMaxNumberPlayers = 9;
            const double insertMinBet = 8.6;

            // expected results by the parameters
            double expBuyinCost = Math.Max(insertBuyinCost, insertMinBet);
            double expMinBet = Math.Min(insertMinBet, insertBuyinCost);
            int expNActive = expIsSpecAllowed ? insertNActive : insertMaxNumberPlayers;
            int expMaxNumberPlayers = insertMaxNumberPlayers;


            var expConfig = new GameConfig()
            {
                BuyInCost = insertBuyinCost,
                Preferences = expGamePreferences,
                IsSpactatorsAllowed = expIsSpecAllowed,
                MaxNumberOfActivePlayers = insertNActive,
                MaxNumberOfPlayers = insertMaxNumberPlayers,
                MinimumBet = insertMinBet,
                MinNumberOfPlayers = expMinPlayers,
                Name = expName,
            }; 
            #endregion

            //Act
            var actual = new Room(expPlayer, expConfig);

            //Assert
            #region Default asserts
            Assert.AreEqual(1, actual.Players.Count, "one player in new room");
            Assert.AreEqual(expPlayer, actual.Players.First(), "the creator is first");
            Assert.AreEqual(Player.State.Passive, expPlayer.CurrentState, "the creator is passive");
            Assert.AreEqual(1, actual.PassivePlayers.Count, "one passive player");
            Assert.AreEqual(0, actual.ActivePlayers.Count, "zero active players");
            Assert.IsTrue(actual.PassivePlayers.Contains(expPlayer), "the creator found in passive players collection at room");
            Assert.AreEqual(null, actual.CurrentHand, "current hand null");
            Assert.AreEqual(false, actual.IsTableFull, "table not full"); 
            #endregion

            // 8 Configurations of game-config
            Assert.AreEqual<double>(expBuyinCost, actual.BuyInCost, "exp buy in");
            Assert.AreEqual(expIsSpecAllowed, actual.IsSpactatorsAllowed, "exp spectators not allowed");
            Assert.AreEqual(expNActive, actual.MaxNumberOfActivePlayers, "exp max active players");

            Assert.AreEqual(expMaxNumberPlayers, actual.MaxNumberOfPlayers, "max players number");
            Assert.IsTrue(!expIsSpecAllowed && actual.MaxNumberOfActivePlayers == actual.MaxNumberOfPlayers, "number of players without spectators");
            Assert.IsFalse(actual.MaxNumberOfActivePlayers > actual.MaxNumberOfPlayers, "active players equal or less to all players at the room");

            Assert.AreEqual(expMinBet, actual.MinimumBet, "minimum bet default");
            Assert.AreEqual(expMinPlayers, actual.MinNumberOfPlayers, "min players default");
            Assert.IsFalse(actual.MinimumBet > actual.BuyInCost, "minBet <= buyIn");

            Assert.IsNotNull(actual.Name, "name not null");
            Assert.AreEqual(expName, actual.Name, "default name");
            // TODO: idan - add assert for default game preferences.
        }

        [TestMethod()]
        public void JoinPlayerToTableTest()
        {
            //Arrange
            var expPlayer = room.PassivePlayers.First();

            //Act
            var actual = room.JoinPlayerToTable(expPlayer);

            //Assert
            Assert.IsTrue(actual);
            Assert.IsTrue(room.ActivePlayers.Contains(expPlayer));
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