using Poker.BE.Domain.Game;

namespace Poker.BE.Service.Modules.Requests
{
    public class ChoosePlayMoveRequest : IRequest
    {
        public Round.Move playMove { get; set; }    //do we prefer to change it to string?
        public int amountToBetOrCall { get; set; }
    }
}
