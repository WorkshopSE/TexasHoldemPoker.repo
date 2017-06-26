using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poker.BE.Service.Modules.Requests;
using Poker.BE.Service.Modules.Results;

namespace Poker.BE.Service.IServices
{
    /// <summary>
    /// Getting information to show for GUI
    /// </summary>
    public interface IKeepAliveService
    {
        KeepAliveResult KeepAlive(KeepAliveRequest request);
    }
}
