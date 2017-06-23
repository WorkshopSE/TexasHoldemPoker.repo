using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepAliveResult : IResult
{
    //Hand's info
    public int[] PlayersAndTableCards;
    public int DealerId;

    //Round's info
    public string[] PlayersStates; //by table location: passive means the seat is empty
    public int CurrentPlayerID;
    public List<int> PotsValues;
    public List<int> PotsAmountToClaim;
    public int[] PlayersBets;   //by table location
    public int TotalRaise;
    public int LastRaise;
}
