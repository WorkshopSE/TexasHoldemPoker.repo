﻿using System;
using System.Collections.Generic;
using System.Linq;
using Poker.BE.Service.Modules.Requests;
using Poker.BE.Service.Modules.Results;
using Poker.BE.Domain.Game;
using Poker.BE.Domain.Core;
using Poker.BE.CrossUtility.Loggers;
using Poker.BE.Domain.Security;
using Poker.BE.CrossUtility.Exceptions;
using Poker.BE.Service.Modules.Caches;

namespace Poker.BE.Service.Services
{
    /// <summary>
    /// UCC03 Rooms Management
    /// </summary>
    /// <see cref="https://docs.google.com/document/d/1OTee6BGDWK2usL53jdoeBOI-1Jh8wyNejbQ0ZroUhcA/edit#heading=h.286w5j2ewu5c"/>
    public class RoomsService : IServices.IRoomsService
    {
        #region Fields
        private CommonCache _cache;
        #endregion

        #region Properties
        /// <summary>
        /// Map for the player (user session) ID -> player at the given session.
        /// using the player.GetHashCode() for generating this ID.
        /// </summary>
        /// <remarks>
        /// session ID is the ID we give for a screen the user opens.
        /// this is a need because the user can play several screen at once.
        /// thus, to play with different players at the same time.
        /// 
        /// for now - the user cannot play as several players, at the same room.
        ///    - this option is blocked.
        /// </remarks>
        public IDictionary<int, Player> Players { get { return _cache.Players; } }
        public IDictionary<int, Room> Rooms { get { return _cache.Rooms; } }
        public IDictionary<string, User> Users { get { return _cache.Users; } }
        public UserManager UserManager { get { return _cache.UserManager; } }
        public GameCenter GameCenter { get { return _cache.GameCenter; } }
        public ILogger Logger { get { return CrossUtility.Loggers.Logger.Instance; } }
        #endregion

        #region Constructors
        public RoomsService()
        {
            _cache = CommonCache.Instance;
        }
        #endregion

        #region Private Functions

        #endregion

        #region Methods
        /// <summary>
        /// for testing
        /// </summary>
        public void Clear()
        {
            _cache.Clear();
        }

        public CreateNewRoomResult CreateNewRoom(CreateNewRoomRequest request)
        {
            var result = new CreateNewRoomResult();

            try
            {
                var user = _cache.RefreshAndGet(
                        Users,
                        request.UserName,
                        new UserNotFoundException("User was not found, can't create room")
                );
                UserManager.SecurityCheck(request.SecurityKey, user);

                Player creator = null;
                Room room = null;
                NoLimitHoldem noLimitPreferences = new NoLimitHoldem(request.Name, request.BuyInCost, request.MinimumBet, request.Antes,
                                                                        request.MinNumberOfPlayers, request.MaxNumberOfPlayers, request.IsSpactatorsAllowed);
                if (request.Limit == 0)
                {
                    room = user.CreateNewRoom(request.Level, noLimitPreferences, out creator);
                }
                else if (request.Limit == -1)
                {
                    PotLimitHoldem potPreferences = new PotLimitHoldem(noLimitPreferences);
                    room = user.CreateNewRoom(request.Level, potPreferences, out creator);
                }
                else if (request.Limit > 0)
                {
                    LimitHoldem limitPreferences = new LimitHoldem(noLimitPreferences, request.Limit);
                    room = user.CreateNewRoom(request.Level, limitPreferences, out creator);
                }

                if (creator == null || room == null)
                {
                    throw new WrongIOException("Limit field in the request is not valid");
                }
                Rooms.Add(room.GetHashCode(), room);
                Players.Add(creator.GetHashCode(), creator);
                result.Player = creator.GetHashCode();
                result.Room = room.GetHashCode();

                //Request's info
                result.Name = request.Name;
                result.BuyInCost = request.BuyInCost;
                result.MinimumBet = request.MinimumBet;
                result.Antes = request.Antes;
                result.MinNumberOfPlayers = request.MinNumberOfPlayers;
                result.MaxNumberOfPlayers = request.MaxNumberOfPlayers;
                result.IsSpactatorsAllowed = request.IsSpactatorsAllowed;
                result.Limit = request.Limit;

                result.Success = true;
            }
            catch (PokerException e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
                Logger.Error(e, this);
            }

            return result;
        }

        public EnterRoomResult EnterRoom(EnterRoomRequest request)
        {
            var result = new EnterRoomResult();

            try
            {
                var room = _cache.RefreshAndGet(
                    Rooms,
                    request.Room,
                    new RoomNotFoundException(string.Format("Requested room ID {0} not found", request.Room))
                    );


                var user = _cache.RefreshAndGet(
                    Users,
                    request.UserName,
                    new UserNotFoundException(string.Format("User ID {0} not found", request.UserName))
                    );
                UserManager.SecurityCheck(request.SecurityKey, user);

                result.Player = user.EnterRoom(room).GetHashCode();
                result.RoomID = room.GetHashCode();
                result.Name = room.Preferences.Name;
                result.BuyInCost = room.Preferences.BuyInCost;
                result.MinimumBet = room.Preferences.MinimumBet;
                result.Antes = room.Preferences.AntesValue;
                result.MinNumberOfPlayers = room.Preferences.MinNumberOfPlayers;
                result.MaxNumberOfPlayers = room.Preferences.MaxNumberOfPlayers;
                result.IsSpactatorsAllowed = room.Preferences.IsSpactatorsAllowed;
                if (room.Preferences is GamePreferencesDecorator)
                {
                    result.Limit = ((GamePreferencesDecorator)room.Preferences).Limit;
                }
            }
            catch (PokerException e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
                Logger.Error(e, this);
            }

            return result;
        }

        public JoinNextHandResult JoinNextHand(JoinNextHandRequest request)
        {
            var result = new JoinNextHandResult();

            try
            {

                var player = _cache.RefreshAndGet(
                    Players,
                    request.Player,
                    new PlayerNotFoundException(string.Format("Cannot find player id: {0}, please exit and re-enter the room.", request.Player))
                    );

                var user = _cache.RefreshAndGet(
                    Users,
                    request.UserName,
                    new UserNotFoundException(string.Format("Cannot find user name: {0}, please login to the server again.", request.UserName))
                    );
                UserManager.SecurityCheck(request.SecurityKey, user);

                user.JoinNextHand(player, request.SeatIndex, request.BuyIn);
                result.UserBank = user.UserBank.Money;
                result.Wallet = player.Wallet.AmountOfMoney;
                result.Success = true;
            }
            catch (PokerException e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
                Logger.Error(e, this);
            }
            return result;
        }

        public StandUpToSpactateResult StandUpToSpactate(StandUpToSpactateRequest request)
        {
            var result = new StandUpToSpactateResult();

            try
            {
                var user = _cache.RefreshAndGet(
                    Users,
                    request.UserName,
                    new UserNotFoundException(string.Format("cannot find user name: {0}, please login again.", request.UserName))
                    );
                UserManager.SecurityCheck(request.SecurityKey, user);

                Player player = null;
                while (!Players.TryGetValue(request.Player, out player))
                {
                    if (_cache.Refresh()) continue;

                    throw new PlayerNotFoundException(string.Format("cannot find player id: {0}, please re-enter the room", request.Player));
                }

                result.RemainingMoney = user.StandUpToSpactate(player);
                result.UserBankMoney = user.UserBank.Money;

                //TODO - idan - check JSON of user statistics
                result.UserStatistics = user.UserStatistics;
                result.Success = true;
            }
            catch (PokerException e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
                Logger.Error(e, this);
            }

            return result;
        }

        public LeaveRoomResult LeaveRoom(LeaveRoomRequest request)
        {
            var result = new LeaveRoomResult();

            try
            {
                var user = _cache.RefreshAndGet(
                    Users,
                    request.UserName,
                    new UserNotFoundException(string.Format("cannot find user name: {0}, please login again.", request.UserName))
                    );
                UserManager.SecurityCheck(request.SecurityKey, user);

                var player = _cache.RefreshAndGet(
                    Players,
                    request.Player,
                    new PlayerNotFoundException(string.Format("player id: {0} not found, please re-enter the room.", request.Player))
                    );

                user.ExitRoom(player);
                result.Success = true;
                result.UserStatistics = user.UserStatistics;

                // update cache on removal
                Players.Remove(player.GetHashCode());
            }
            catch (PokerException e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
                Logger.Error(e, this);
            }

            return result;
        }

        private FindRoomsByCriteriaResult.RoomResult[] DomainRoomsToRoomsResults(ICollection<Room> domainRooms)
        {
            var rooms = new List<FindRoomsByCriteriaResult.RoomResult>();

            _cache.Refresh();
            foreach (var room in domainRooms)
            {
                rooms.Add(new FindRoomsByCriteriaResult.RoomResult()
                {
                    RoomID = room.GetHashCode(),
                    LeagueID = _cache.RoomToLeague[room].GetHashCode(),
                    RoomName = room.Preferences.Name,
                    CurrentNumberOfPlayers = room.Players.Count,
                    MaxNumberOfPlayers = room.Preferences.MaxNumberOfPlayers,
                    MinimumBuyIn = room.Preferences.BuyInCost,
                    PotLimit = (room.Preferences as PotLimitHoldem)?.Limit,
                });
            }
            return rooms.ToArray();
        }

        public FindRoomsByCriteriaResult FindRoomsByCriteria(FindRoomsByCriteriaRequest request)
        {
            var result = new FindRoomsByCriteriaResult();

            try
            {
                // arrange search criteria
                double betSize = request.Criterias.Contains(FindRoomsByCriteriaRequest.BET_SIZE) ? request.BetSize : -1;

                GamePreferences preferences =
                    new NoLimitHoldem()
                    {
                        MaxNumberOfPlayers = request.Criterias.Contains(FindRoomsByCriteriaRequest.MAX_NUMBER_OF_PLAYERS) ? request.MaxNumberOfPlayers : -1,
                        AntesValue = request.Criterias.Contains(FindRoomsByCriteriaRequest.ANTES_VALUE) ? request.Antes : -1,
                        BuyInCost = request.Criterias.Contains(FindRoomsByCriteriaRequest.BUY_IN_COST) ? request.BuyInCost : -1,
                        Name = request.Criterias.Contains(FindRoomsByCriteriaRequest.NAME) ? request.Name : "",
                        MinimumBet = request.Criterias.Contains(FindRoomsByCriteriaRequest.MIN_BET) ? request.MinimumBet : -1,
                        MinNumberOfPlayers = request.Criterias.Contains(FindRoomsByCriteriaRequest.MIN_NUMBER_OF_PLAYERS) ? request.MinNumberOfPlayers : -1,
                    };

                Player player = null;
                if (request.Criterias.Contains(FindRoomsByCriteriaRequest.PLAYER))
                {
                    player = _cache.RefreshAndGet(
                         Players,
                         request.Player,
                         new PlayerNotFoundException(string.Format("player id: {0} not found, please re-enter the room.", request.Player))
                         );
                }

                int level = request.Criterias.Contains(FindRoomsByCriteriaRequest.LEVEL) ? request.Level : -1;

                // call search
                var domainRooms = GameCenter.FindRoomsByCriteria(level, player, preferences, betSize);

                // assemble result
                result.Rooms = DomainRoomsToRoomsResults(domainRooms);
                result.Success = true;
            }
            catch (PokerException e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
                Logger.Error(e, this);
            }

            return result;
        }

        public FindRoomsByCriteriaResult GetAllRooms()
        {
            var result = new FindRoomsByCriteriaResult();

            try
            {
                result.Rooms = DomainRoomsToRoomsResults(Rooms.Values);
                result.Success = true;
            }
            catch (PokerException e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
                Logger.Error(e, this);
            }

            return result;
        }

        public FindRoomsByCriteriaResult GetAllRoomsOfLeague(int leagueId)
        {
            var result = new FindRoomsByCriteriaResult();

            try
            {
                var league = _cache.RefreshAndGet(
                    _cache.Leagues,
                    leagueId,
                    new LeagueNotFoundException(string.Format("league id: {0} not found, please try again on different league", leagueId))
                    );

                result.Rooms = DomainRoomsToRoomsResults(league.Rooms);
                result.Success = true;
            }
            catch (PokerException e)
            {
                result.Success = false;
                result.ErrorMessage = e.Message;
                Logger.Error(e, this);
            }

            return result;
        }


        #endregion

    }// class
}
