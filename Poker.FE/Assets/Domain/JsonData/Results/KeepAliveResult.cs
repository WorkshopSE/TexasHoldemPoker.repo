using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepAliveResult : IResult
{
    //Room's info
    public List<int> ActivePlayers;
    public int[] TableLocationOfActivePlayers;
    public string[] PlayersStates; //by table location: passive means the seat is empty
    public bool IsTableFull;

    //Hand's info
    /// <summary>
    /// this field is an array with size of 52, according to cards indexes.
    /// Each cell contains onr of the followings:
    /// The player's hash code (the one that holds this card)
    /// A negative number represent each of the table cards (if there are any): -1 for the first card, -2 for the second...
    /// Zero, meanning this card is not in use (still in the deck)
    /// </summary>
    public int[] PlayersAndTableCards;
    public int DealerId;

    //Round's info
    public int CurrentPlayerID;
    public List<double> PotsValues;
    public List<double> PotsAmountToClaim;
    public double[] PlayersBets;   //by table location
    public double TotalRaise;
    public double LastRaise;

    //Player's info
    public double PlayerWallet;

    public KeepAliveResult() { }
}
