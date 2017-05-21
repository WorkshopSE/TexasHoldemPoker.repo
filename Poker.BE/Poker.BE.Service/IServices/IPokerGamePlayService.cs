using Poker.BE.Domain.Game;
using Poker.BE.Domain.Utility.Logger;
using Poker.BE.Service.Modules.Requests;
using Poker.BE.Service.Modules.Results;

namespace Poker.BE.Service.IServices
{
    /// <summary>
    /// UCC06: Poker Game-play
    /// </summary>
    /// <see cref="https://docs.google.com/document/d/1ob4bSynssE3UOfehUAFNv_VDpPbybhS4dW_O-v-QDiw/edit#heading=h.kaxnrwixytqc"/>
    public interface IPokerGamePlayService
    {
        StartNewHandResult StartNewHand(StartNewHandRequest request);
        ChoosePlayMoveResult ChoosePlayMove(ChoosePlayMoveRequest request);
    }
}
