[System.Serializable]
public class Room {

    public int id;
    public int playerid;
    public string roomName;
    internal double BuyInCost;
    internal double MinimumBet;

    public Room()
    {
    }

    public double Antes { get; internal set; }
    public int MinNumberOfPlayers { get; internal set; }
    public int MaxNumberOfPlayers { get; internal set; }
    public bool IsSpactatorsAllowed { get; internal set; }
    public double Limit { get; internal set; }
    public int ChairIndex { get; internal set; }
    public bool IsTableFull { get; internal set; }
    public double PlayerWallet { get; internal set; }
}
