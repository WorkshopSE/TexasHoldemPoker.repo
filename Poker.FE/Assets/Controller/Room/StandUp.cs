using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StandUp : MonoBehaviour {
    public Text feedback;
    public GameObject spectate;
    public GameObject active;
    public Button leaveButton;
    public List<GameObject> chairs;
    public List<GameObject> players;

    HttpCallFactory http = new HttpCallFactory();
    StandUpToSpactateRequest request;
    StandUpToSpactateResult result;

	public void DoStandUp()
    {
        request = new StandUpToSpactateRequest()
        {
            Player = GameProperties.CurrentRoom.playerid,
            UserName = GameProperties.user.userName,
            SecurityKey = GameProperties.user.SecurityKey
        };
        string standUpJson = JsonUtility.ToJson(request);
        StartCoroutine(http.POST(URL.StandUp, standUpJson, new System.Action<string>(standUpSuccess), new System.Action<string>(standUpFail)));
    }
    private void Start()
    {
        leaveButton.interactable = false;
    }

    private void standUpFail(string failMessage)
    {
        feedback.text = failMessage + " Please Try Again";
        Debug.Log("STANDUP FAILED Error " + result.ErrorMessage);
    }

    private void standUpSuccess(string successMessage)
    {
        result = JsonUtility.FromJson<StandUpToSpactateResult>(successMessage);
        if (result.Success)
        {

            //TODO: maybe add banner of result.RemainingMoney
            GameProperties.user.deposit = result.UserBankMoney;
            players[GameProperties.CurrentRoom.ChairIndex].SetActive(false);
            chairs[GameProperties.CurrentRoom.ChairIndex].GetComponent<Image>().enabled = true;
            leaveButton.interactable = true;
            spectate.SetActive(true);
            active.SetActive(false);
        }
        else
        {
            feedback.text = result.ErrorMessage + " Please Try Again";
            Debug.Log("STANDUP SUCCESS Error " + result.ErrorMessage);
        }
        
    }
}
