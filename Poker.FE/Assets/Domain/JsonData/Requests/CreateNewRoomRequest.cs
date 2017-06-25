[System.Serializable]
public class CreateNewRoomRequest
{
    public int Level;
    public string User;
    public string Name;
    public double BuyInCost;
    public double MinimumBet;
    public double Antes;
    public int MinNumberOfPlayers;
    public int MaxNumberOfPlayers;
    public bool IsSpactatorsAllowed;
    public double Limit;
}
