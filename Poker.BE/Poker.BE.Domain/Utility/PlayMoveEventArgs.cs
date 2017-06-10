using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Domain.Utility
{
    public class PlayMoveEventArgs : EventArgs
    {
        public string PlayMove { get; private set; }
        public double AmountToBetOrCall { get; private set; }

        public PlayMoveEventArgs(string playMove, double amountToBetOrCall)
        {
            PlayMove = playMove;
            AmountToBetOrCall = amountToBetOrCall;
        }
    }
}
