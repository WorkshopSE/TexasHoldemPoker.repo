using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNewRoomResult : IResult
{
    public int Room;   //room hash code
    public int Player; //player hash code 

    // Request's Game Preferences info
    public string Name;
    public double BuyInCost;
    public double MinimumBet;
    public double Antes;
    public int MinNumberOfPlayers;
    public int MaxNumberOfPlayers;
    public bool IsSpactatorsAllowed;

    public double Limit;       //this is for decorator (PotLimit/Limit game mode)
}
