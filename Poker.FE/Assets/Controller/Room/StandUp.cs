using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StandUp : MonoBehaviour {
    public Text feedback;
    public GameObject spectate;
    public GameObject active;
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
            User = GameProperties.user.userName
        };
        string standUpJson = JsonUtility.ToJson(request);
        StartCoroutine(http.POST(URL.StandUp, standUpJson, new System.Action<string>(standUpSuccess), new System.Action<string>(standUpFail)));
    }

    private void standUpFail(string failMessage)
    {
        feedback.text = failMessage + " Please Try Again";
    }

    private void standUpSuccess(string successMessage)
    {
        result = JsonUtility.FromJson<StandUpToSpactateResult>(successMessage);
        if (result.Success)
        {

            //TODO: Think about wallet;1
            spectate.SetActive(true);
            active.SetActive(false);
            players[GameProperties.CurrentRoom.ChairIndex].SetActive(false);
            chairs[GameProperties.CurrentRoom.ChairIndex].GetComponent<Image>().enabled = true;
        }
        else
        {
            feedback.text = result.ErrorMessage + " Please Try Again";
        }
        
    }
}
