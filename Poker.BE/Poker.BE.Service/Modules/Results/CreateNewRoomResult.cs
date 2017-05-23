using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Service.Modules.Results
{
    public class CreateNewRoomResult : IResult
    {
        /// <summary>
        /// room hash code
        /// </summary>
        public int Room { get; set; }
    }
}
