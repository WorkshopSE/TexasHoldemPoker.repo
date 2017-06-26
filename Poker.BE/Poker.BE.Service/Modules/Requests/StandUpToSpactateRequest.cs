namespace Poker.BE.Service.Modules.Requests
{
    public class StandUpToSpactateRequest : IRequest
    {
        public int Player { get; set; }
    }
}