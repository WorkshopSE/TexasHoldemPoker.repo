using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SampleRoomEntry : MonoBehaviour {
    public Button button;
    public Text capacity;
    public Text roomName;
    public Text leagueID;

    private HttpCallFactory http = new HttpCallFactory();
    private EnterRoomRequest request;
    private EnterRoomResult result;
    private int roomID;

    // Use this for initialization
    void Start () {
		
	}

    public void JoinRoom()
    {
        request = new EnterRoomRequest()
        {
            Room = roomID,
            UserName = GameProperties.user.userName,
            SecurityKey = GameProperties.user.SecurityKey
        };
        string enterRoomJson = JsonUtility.ToJson(request);
        StartCoroutine(http.POST(URL.EnterRoom, enterRoomJson, EnterRoomSuccess, EnterRoomFailed));
    }

    private void EnterRoomFailed(string failMessage)
    {
        if (failMessage.Equals("Server Error"))
        {
            StartCoroutine(ShowError(failMessage));
            Debug.Log(failMessage);
        }
        else
        {
            result = JsonUtility.FromJson<EnterRoomResult>(failMessage);
            StartCoroutine(ShowError(result.ErrorMessage));
            Debug.Log(result.ErrorMessage);
        }
    }

    private IEnumerator ShowError(string failMessage)
    {
        string temp = roomName.text;
        roomName.text = failMessage;
        yield return new WaitForSeconds(3);
        roomName.text = temp;

    }

    private void EnterRoomSuccess(string successMessage)
    {
        result = JsonUtility.FromJson<EnterRoomResult>(successMessage);
        if (result.Success)
        {
            GameProperties.CurrentRoom.id = result.RoomID;
            GameProperties.CurrentRoom.playerid = result.Player;
            GameProperties.CurrentRoom.roomName = result.Name;
            GameProperties.CurrentRoom.BuyInCost = result.BuyInCost;
            GameProperties.CurrentRoom.MinimumBet = result.MinimumBet;
            GameProperties.CurrentRoom.Antes = result.Antes;
            GameProperties.CurrentRoom.MinNumberOfPlayers = result.MinNumberOfPlayers;
            GameProperties.CurrentRoom.MaxNumberOfPlayers = result.MaxNumberOfPlayers;
            GameProperties.CurrentRoom.IsSpactatorsAllowed = result.IsSpactatorsAllowed;
            GameProperties.CurrentRoom.Limit = result.Limit;
            SceneManager.LoadSceneAsync("Room");
        }
        else
        {
            result = JsonUtility.FromJson<EnterRoomResult>(successMessage);
            StartCoroutine(ShowError(result.ErrorMessage));
            Debug.Log(result.ErrorMessage);
        }
    }

    internal void Setup(FindRoomsByCriteriaResult.RoomResult room, ChooseARoom chooseARoom)
    {
        this.roomID = room.RoomID;
        capacity.text = "Players: " + room.CurrentNumberOfPlayers + "/" + room.MaxNumberOfPlayers;
        roomName.text = room.RoomName;
        leagueID.text = "Min Bet " + room.MinimumBuyIn;
    }
}
