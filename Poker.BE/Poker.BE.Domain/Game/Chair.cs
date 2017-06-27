﻿
namespace Poker.BE.Domain.Game
{
    public class Chair
    {
        

        #region Fields
        private int index;
        private bool isTaken;
        #endregion

        #region Properties
        public bool IsTaken { get { return isTaken; } }
        public int Index { get { return index; } }
        #endregion

        #region Constructors
        public Chair(int index)
        {
            this.index = index;
            lock (this) { isTaken = false; }
        }

        #endregion

        #region Methods
        public bool Take()
        {
            if (isTaken) return false;
            lock (this)
            {
                if (isTaken) return false;
                isTaken = true;
            }
            return true;
        }

        public void Release()
        {
            lock (this) { isTaken = false; }
        }
        #endregion

    }
}
