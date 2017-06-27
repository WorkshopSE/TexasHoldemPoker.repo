using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayMoveResult : IResult
{
    public int NextPlayer;
    public double TotalRaise;
    public double LastRaise;       //this is for making sure the player doesn't raise more than last raise made

    //Note: this is the amount of money the player has invested until now.
    //      for calculating the amount of money he needs to call:
    //      TotalRaise - NextPlayerInvest = CallAmount
    public double NextPlayerInvest;
}
