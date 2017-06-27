using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FindRoomsByCriteriaResult : IResult
{
    /// <summary>
    /// Array of rooms id's.
    /// </summary>
    public RoomResult[] Rooms;

    [System.Serializable]
    public class RoomResult
    {
        public int RoomID;
        public string RoomName;
        public int LeagueID;
        public double PotLimit;
        public double MinimumBuyIn;
        public int MaxNumberOfPlayers;
        public int CurrentNumberOfPlayers;
    }
}
