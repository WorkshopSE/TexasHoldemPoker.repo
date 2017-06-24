namespace Poker.BE.Service.Modules.Requests
{
    public class StandUpToSpactateRequest : IRequest
    {
        public string User { get; set; }
        public int Player { get; set; }
    }
}