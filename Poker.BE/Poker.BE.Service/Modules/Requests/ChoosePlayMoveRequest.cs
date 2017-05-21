using Poker.BE.Domain.Game;

namespace Poker.BE.Service.Modules.Requests
{
    //TODO - Add implementaion of IRequest
    public class ChoosePlayMoveRequest
    {
        public Round.Move playMove { get; set; }    //do we prefer to change it to string?
        public int amountToBetOrCall { get; set; }
    }
}
