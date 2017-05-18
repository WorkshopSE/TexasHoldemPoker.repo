using Poker.BE.Domain.Game;
using Poker.BE.Service.Modules.Results;
using Poker.BE.Service.Modules.Requests;


namespace Poker.BE.Service.IServices
{
    /// <summary> Supports UCC03: Rooms Management </summary>
    /// <see cref="https://docs.google.com/document/d/1ob4bSynssE3UOfehUAFNv_VDpPbybhS4dW_O-v-QDiw/edit#heading=h.286w5j2ewu5c"/>
    public interface IRoomsService
    {
        CreateNewRoomResult CreateNewRoom(CreateNewRoomRequest request);
        EnterRoomResult EnterRoom(EnterRoomRequest request);
        JoinNextHandResult JoinNextHand(JoinNextHandRequest request);
        StandUpToSpactateResult StandUpToSpactate(StandUpToSpactateRequest request);
    }
}