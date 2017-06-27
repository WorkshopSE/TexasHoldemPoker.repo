[System.Serializable]
public class JoinNextHandRequest : IRequest
{
    public int Player;
    public int SeatIndex;
    public double BuyIn;
}
