[System.Serializable]
public class PlayMoveRequest : IRequest
{
    public int Player;
    public string PlayMove; //Null,Check,Call,Bet,Fold,Raise,Allin
    public double AmountOfMoney; //this is for raise\bet - otherwise its value should be 0
}
