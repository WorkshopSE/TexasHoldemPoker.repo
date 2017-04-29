using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Utility
{
    public class RecordManager
    {
        #region Properties
        protected Dictionary<int, Record> RecordsDictionary;
        #endregion

        #region Methods
        public RecordManager()
        {
            RecordsDictionary = new Dictionary<int, List<Record>>();
        }

        public bool AddUser(int userId)
        {
            if(!IsUserExist(userId)) {
                List<Record> UserRecordList = new List<Record>();
                RecordsDictionary.Add(userId, UserRecordList);
                return true;
            }
            return false;
        }

        public bool RemoveUser(int userId)
        {
            return RecordsDictionary.Remove(userId);
        }

        public bool IsUserExist(int userId)
        {
            if(userId != null)
                return RecordsDictionary.ContainsKey(userId);
            return false;
        }

        public List<Record> GetUserRecords(int userId)
        {
            return RecordsDictionary[userId];
        }
        #endregion
    }

}
