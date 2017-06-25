using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker.BE.CrossUtility.Exceptions;
using Poker.BE.Domain.Core;

using System;
using System.Linq;

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
            var preferences = new NoLimitHoldem();
            var expConfig = new NoLimitHoldem();

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
            Assert.AreEqual(expConfig.BuyInCost, actual.Preferences.BuyInCost, "default buy in");
            Assert.AreEqual(true, actual.Preferences.IsSpactatorsAllowed, "default spectators allowed");
            Assert.AreEqual(expConfig.MaxNumberOfPlayers, actual.Preferences.MaxNumberOfPlayers, "default max players number");
            Assert.AreEqual(expConfig.MinimumBet, actual.Preferences.MinimumBet, "minimum bet default");
            Assert.AreEqual(expConfig.MinNumberOfPlayers, actual.Preferences.MinNumberOfPlayers, "min players default");
            Assert.AreEqual(expConfig.Name, actual.Preferences.Name, "default name");
            // TODO: idan - add assert for default game preferences.

        }

        [TestMethod()]
        public void RoomTest2() // (:Player)
        {
            //Arrange
            var player = new Player();
            var expPlayer = player;
            var expConfig = new NoLimitHoldem();

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
            Assert.AreEqual(expConfig.BuyInCost, actual.Preferences.BuyInCost, "default buy in");
            Assert.AreEqual(true, actual.Preferences.IsSpactatorsAllowed, "default spectators allowed");
            Assert.AreEqual(expConfig.MaxNumberOfPlayers, actual.Preferences.MaxNumberOfPlayers, "default max players number");
            Assert.AreEqual(expConfig.MinimumBet, actual.Preferences.MinimumBet, "minimum bet default");
            Assert.AreEqual(expConfig.MinNumberOfPlayers, actual.Preferences.MinNumberOfPlayers, "min players default");
            Assert.AreEqual(expConfig.Name, actual.Preferences.Name, "default name");
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
            GamePreferences expGamePreferences = new NoLimitHoldem();
            const int expMinPlayers = 3;

            // changed parameters
            const double insertBuyinCost = 50.2;
            const int insertMaxNumberPlayers = 9;
            const double insertMinBet = 8.6;

            // expected results by the parameters
            double expBuyinCost = Math.Max(insertBuyinCost, insertMinBet);
            double expMinBet = Math.Min(insertMinBet, insertBuyinCost);
            int expMaxNumberPlayers = insertMaxNumberPlayers;


            var expConfig = new NoLimitHoldem()
            {
                BuyInCost = insertBuyinCost,
                IsSpactatorsAllowed = expIsSpecAllowed,
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
            Assert.AreEqual<double>(expBuyinCost, actual.Preferences.BuyInCost, "exp buy in");
            Assert.AreEqual(expIsSpecAllowed, actual.Preferences.IsSpactatorsAllowed, "exp spectators not allowed");

            Assert.AreEqual(expMaxNumberPlayers, actual.Preferences.MaxNumberOfPlayers, "max players number");

            Assert.AreEqual(expMinBet, actual.Preferences.MinimumBet, "minimum bet default");
            Assert.AreEqual(expMinPlayers, actual.Preferences.MinNumberOfPlayers, "min players default");
            Assert.IsFalse(actual.Preferences.MinimumBet > actual.Preferences.BuyInCost, "minBet <= buyIn");

            Assert.IsNotNull(actual.Preferences.Name, "name not null");
            Assert.AreEqual(expName, actual.Preferences.Name, "default name");
            // TODO: idan - add assert for default game preferences.
        }

        [TestMethod()]
        public void JoinPlayerToTableTest()
        {
            //Arrange
            var expPlayer = room.PassivePlayers.First();

            //Act
            var actual = room.JoinPlayerToTable(expPlayer, room.Preferences.BuyInCost + 10.3);

            //Assert
            Assert.IsTrue(actual);
            Assert.IsTrue(room.ActivePlayers.Contains(expPlayer));
        }

        [TestMethod()]
        public void RemovePlayerTest()
        {
            //Arrange
            var expected = room.Players.First();

            //Act
            var actual = room;
            room.RemovePlayer(expected);

            //Assert
            Assert.IsFalse(actual.Players.Contains(expected));
            Assert.IsFalse(actual.PassivePlayers.Contains(expected));
            Assert.IsFalse(actual.ActivePlayers.Contains(expected));
        }

        [TestMethod()]
        public void ClearAllTest()
        {
            //Arrange

            //Act
            room.ClearAll();
            var actual = room;

            //Assert
            Assert.AreEqual(0, actual.ActivePlayers.Count);
            Assert.AreEqual(0, actual.PassivePlayers.Count);
            Assert.AreEqual(0, actual.Players.Count);
            //Assert.AreEqual(0, actual.Preferences); // TODO
            Assert.AreEqual(false, actual.IsTableFull);
            Assert.AreEqual(null, actual.CurrentHand);
            Assert.IsNotNull(actual.Preferences.Name);

        }

        [TestMethod()]
        public void CreatePlayerTest()
        {
            //Arrange
            var expected = new Player() { Nickname = "test player" };

            //Act
            var actual = room.CreatePlayer();
            actual.Nickname = "test player";

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TakeChairTest()
        {
            //Arrange
            var expPlayer = new Player();

            //Act
            var actual1 = room.TakeChair(expPlayer, 2);
            var actTable = room.TableLocationOfActivePlayers.Count;
            var actual2 = room.TakeChair(roomCreator, 9);
            
            //Assert
            Assert.AreEqual(false, actual1);
            Assert.AreEqual(0, actTable);
            Assert.AreEqual(false, room.Chairs.ElementAt(2).IsTaken);

            Assert.AreEqual(true, actual2);
            Assert.AreEqual(1, room.TableLocationOfActivePlayers.Count);
            Assert.AreEqual(roomCreator, room.TableLocationOfActivePlayers[room.Chairs.ElementAt(9)]);
            Assert.AreEqual(true, room.Chairs.ElementAt(9).IsTaken);
        }

        [TestMethod()]
        public void LeaveChairTest()
        {
            //Arrange

            //Act
            room.TakeChair(roomCreator, 3);
            var actual1 = room.TableLocationOfActivePlayers.Count;
            var actChair = room.Chairs.ElementAt(3).IsTaken;
            roomCreator.CurrentState = Player.State.ActiveFolded;
            //Note: this is because the TakeChair doesn't set the player's state (a different method does it)
            room.LeaveChair(roomCreator);
            var actual2 = room.TableLocationOfActivePlayers.Count;
            var actChair2 = room.Chairs.ElementAt(3).IsTaken;

            //Assert
            Assert.AreEqual(1, actual1);
            Assert.AreEqual(0, actual2);
            Assert.AreEqual(true, actChair);
            Assert.AreEqual(false, actChair2);
        }

        [TestMethod()]
        public void StartNewHandTest()
        {
            //Arrange
            Player player1 = new Player();
            GamePreferences preferences = new NoLimitHoldem();
            GameCenter center = GameCenter.Instance;
            Room room = new Room(player1, preferences);
            Exception expectedExcetpion = null;
            //Act
            try
            {
                room.StartNewHand();
            }
            catch (NotEnoughPlayersException ex)
            {
                expectedExcetpion = ex;
            }
            // Assert - precondition lower than 2 players
            Assert.AreEqual(expectedExcetpion.Message, "Its should be at least 2 active players to start new hand!");

            Player player2 = center.EnterRoom(room);
            try
            {
                room.StartNewHand();
            }
            catch (NotEnoughPlayersException ex)
            {
                expectedExcetpion = ex;
            }
            // Assert - precondition no active players
            Assert.AreEqual(expectedExcetpion.Message, "Its should be at least 2 active players to start new hand!");

            //More Test when UC020: Join Next Hand completed, needed for make players Active

        }
    }
}