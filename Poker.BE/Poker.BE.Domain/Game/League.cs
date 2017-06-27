using Poker.BE.CrossUtility.Exceptions;
using Poker.BE.Domain.Core;
using System;
using System.Collections.Generic;

namespace Poker.BE.Domain.Game
{
    public class League
    {
        public const int MAX_LEVEL = 600;
        public const int MIN_LEVEL = 1;

        #region Properties
        public ICollection<Room> Rooms { get; private set; }
        public ICollection<User> Users { get; private set; }
        public int MinLevel { get; set; }
        public int MaxLevel { get; set; }
        public bool IsFull { get; set; }
        public string Name { get; set; }
        #endregion

        #region Constructors
        public League()
        {
            Rooms = new List<Room>();
            Users = new List<User>();
            IsFull = false;
        }
        #endregion

        #region Methods
        public void AddRoom(Room room)
        {
            Rooms.Add(room);
        }

        public void RemoveRoom(Room room)
        {
            Rooms.Remove(room);
        }

        public void AddUser(User user)
        {
            Users.Add(user);
        }

        public void RemoveUser(User user)
        {
            Users.Remove(user);
        }
        
        public void SetMinMaxLevel()
        {
            if (Users.Count < 2)
                throw new WrongIOException("Can't have less than 2 users in a league");

            foreach (User user in Users)
            {
                if (user.Level < MinLevel)
                    MinLevel = user.Level;
                if (user.Level > MaxLevel)
                    MaxLevel = user.Level;
            }
        }
        #endregion

    }
}
