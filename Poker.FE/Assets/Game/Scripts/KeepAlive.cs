using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeepAlive : MonoBehaviour
{
    public GameObject delayFeedback;

    private int delayCounter = 0;
    private HttpCallFactory http;
    private KeepAliveRequest request;
    private KeepAliveResult result;
    private string requestJson;

    // Use this for initialization
    void Start()
    {
        request = new KeepAliveRequest()
        {
            Room = GameProperties.CurrentRoom.id,
            User = GameProperties.user.userName
        };
        requestJson = JsonUtility.ToJson(request);
        http = new HttpCallFactory();
        InvokeRepeating("RequestUpdate", 0, GameProperties.pollingFrequency);
    }
    void RequestUpdate()
    {
        StartCoroutine(http.POST(URL.KeepAlive, requestJson, new System.Action<string>(updateComplete), new System.Action<string>(updateFailed)));
        
    }

    private void updateFailed(string failMessage)
    {
        delayFeedback.GetComponent<Text>().text = "Please wait [Delay count = " + ++delayCounter + " sec]";
        Debug.Log("Keep Alive Error " + failMessage);
    }

    private void updateComplete(string successMessage)
    {
        delayCounter = 0;
        delayFeedback.GetComponent<Text>().text = "";
        result = JsonUtility.FromJson<KeepAliveResult>(successMessage); 
        if (!result.Success)
        {
            delayFeedback.GetComponent<Text>().text = "Error while trying to get update. \n Error message: " + result.ErrorMessage;
        }
        //TODO: Add update to table
        throw new NotImplementedException();
    }
}
