using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnterRoomResult : IResult
{
    /// <summary>
    /// player hash code (session id)
    /// </summary>
    public int Player;
    public int RoomID;

    //Game Preferences data
    public string Name;
    public double BuyInCost;
    public double MinimumBet;
    public double Antes;
    public int MinNumberOfPlayers;
    public int MaxNumberOfPlayers;
    public bool IsSpactatorsAllowed;

    public double Limit;       //this is for decorator (PotLimit/Limit game mode)


}
