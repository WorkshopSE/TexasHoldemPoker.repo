using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Service.Modules.Requests
{
    public class KeepAliveRequest : IRequest
    {
        public string User { get; set; }
        public int Room { get; set; }
    }
}
