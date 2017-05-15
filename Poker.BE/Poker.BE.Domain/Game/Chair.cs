using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Game
{
    public class Chair
    {
        

        #region Fields
        private int index;
        private bool isBusy;
        #endregion

        #region Constructors
        public Chair(int index)
        {
            this.index = index;
            lock (this) { isBusy = false; }
        }

        #endregion

        #region Methods
        public bool Take()
        {
            if (isBusy) return false;
            lock (this)
            {
                if (isBusy) return false;
                isBusy = true;
            }
            return true;
        }

        public void Release()
        {
            lock (this) { isBusy = false; }
        }
        #endregion

    }
}
