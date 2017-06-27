using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Service.Modules.Requests
{
    public abstract class IRequest
    {
        public string UserName { get; set; }
        public int? SecurityKey { get; set; }
    }
}
