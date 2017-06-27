using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
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
    public int[] PlayersAndTableCards;                          //ARIEL
    public int DealerId;
    public int SmallBlindId;
    public int BigBlindId;

    //Round's info
    public int CurrentPlayerID;
    public List<double> PotsValues;                             //ARIEL
    public List<double> PotsAmountToClaim;                      //ARIEL
    public double[] PlayersBets;   //by table location          //ARIEL
    public double TotalRaise;                                   //ARIEL
    public double LastRaise;                                    //Asaf - LastRaise == 0 -> {Check, All in, Bet, Fold}, LastRaise > 0 -> {All in, Fold, Call, Raise}, 

    //Player's info
    public double PlayerWallet;

    public KeepAliveResult() { }
}
