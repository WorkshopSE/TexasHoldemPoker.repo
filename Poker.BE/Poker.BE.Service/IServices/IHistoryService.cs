using Poker.BE.Service.Modules.Requests;
using Poker.BE.Service.Modules.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.BE.Service.IServices
{
    /// <summary>
    /// UCC05: History - Replay & Statistics & Game Information
    /// </summary>
    /// <see cref="https://docs.google.com/document/d/1ob4bSynssE3UOfehUAFNv_VDpPbybhS4dW_O-v-QDiw/edit#heading=h.abxv4a36ufy8"/>
    public interface IHistoryService
    {
        GetStatisticsResult GetStatistics(GetStatisticsRequest request);
    }
}
