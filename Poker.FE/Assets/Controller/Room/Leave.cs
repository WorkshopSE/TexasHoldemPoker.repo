using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leave : MonoBehaviour {
    public Text feedback;
    public GameObject UIControl;

    private HttpCallFactory http = new HttpCallFactory();
    private LeaveRoomRequest request;
    private LeaveRoomResult result;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void doLeave()
    {
        request = new LeaveRoomRequest()
        {
            Player = GameProperties.CurrentRoom.playerid,
            Room = GameProperties.CurrentRoom.id,
            SecurityKey = GameProperties.user.SecurityKey,
            UserName = GameProperties.user.userName
        };
        string jsonLeave = JsonUtility.ToJson(request);
        StartCoroutine(http.POST(URL.LeaveRoom, jsonLeave, LeaveComplete, LeaveFailed));
    }

    private void LeaveFailed(string failMsg)
    {
        if (failMsg.Equals("Server Error"))
        {
            feedback.text = failMsg;
        }
        else
        {
            result = JsonUtility.FromJson<LeaveRoomResult>(failMsg);
            feedback.text = result.ErrorMessage;
        }
    }

    private void LeaveComplete(string completeMsg)
    {
        result = JsonUtility.FromJson<LeaveRoomResult>(completeMsg);
        if (result.Success)
        {
            UIControl.GetComponent<UIControl>().ChangeScene("RoomManagement");
        }
        else
        {
            feedback.text = result.ErrorMessage;
        }

    }
}
