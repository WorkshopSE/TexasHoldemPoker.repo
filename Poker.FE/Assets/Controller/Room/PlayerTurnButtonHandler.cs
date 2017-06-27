using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTurnButtonHandler : MonoBehaviour
{
    public Slider slider;
    public Text sliderValue;
    public Text feedback;
    public GameObject playerTurnPanel;

    private HttpCallFactory http = new HttpCallFactory();
    private PlayMoveRequest request;
    private PlayMoveResult result;

    // Use this for initialization
    void Start()
    {

    }
    public void ButtonPress(string action)
    {
        request = new PlayMoveRequest()
        {
            Player = GameProperties.CurrentRoom.playerid,
            SecurityKey = GameProperties.user.SecurityKey,
            UserName = GameProperties.user.userName,
            AmountOfMoney = 0
        };
        if (action.Equals("Bet") || action.Equals("Raise"))
        {
            request.AmountOfMoney = slider.value;
        }
        string jsonMove = JsonUtility.ToJson(request);
        StartCoroutine(http.POST(URL.PlayMove, jsonMove, PlayMoveComplete, PlayMoveFailed));
    }

    private void PlayMoveFailed(string failMsg)
    {
        if (failMsg.Equals("Server Error"))
        {
            feedback.text = failMsg;
            Debug.Log(failMsg);
        }
        else
        {
            result = JsonUtility.FromJson<PlayMoveResult>(failMsg);
            feedback.text = result.ErrorMessage;
            Debug.Log(result.ErrorMessage);
        }
    }

    private void PlayMoveComplete(string completeMsg)
    {
        result = JsonUtility.FromJson<PlayMoveResult>(completeMsg);
        if (result.Success)
        {
            feedback.text = "Move Complete!";
            playerTurnPanel.SetActive(false);
        }
        else
        {
            feedback.text = result.ErrorMessage;
            Debug.Log(result.ErrorMessage);
        }
    }

    // Update is called once per frame
    void Update()
    {
        sliderValue.text = slider.value.ToString();
    }
}
