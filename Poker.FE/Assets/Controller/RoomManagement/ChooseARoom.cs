using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class ChooseARoom : MonoBehaviour {

    public SimpleObjectPool buttonObjectPool;
    public Transform contentPanel;
    public GameObject UIControl;

    private HttpCallFactory http = new HttpCallFactory();
    
    private FindRoomsByCriteriaResult result;

    // Use this for initialization
    void Start()
    {
        ChooseARooomAction();
    }
    public void ChooseARooomAction()
    {
        StartCoroutine(http.GET(URL.GetAllRooms, new Action<string>(GetSuccess), new Action<string>(GetFailed)));
    }

    private void GetFailed(string failMessage)
    {
        if (failMessage.Equals("Server Error"))
        {
            UIControl.GetComponent<UIControl>().PopMessageToScreen(failMessage);
            Debug.Log(failMessage);
        }
        else
        {
            result = JsonUtility.FromJson<FindRoomsByCriteriaResult>(failMessage);
            UIControl.GetComponent<UIControl>().PopMessageToScreen(result.ErrorMessage);
            Debug.Log(result.ErrorMessage);
        }
    }

    private void GetSuccess(string successMessage)
    {
        result = JsonUtility.FromJson<FindRoomsByCriteriaResult>(successMessage);
        if (result.Success)
        {
            foreach (FindRoomsByCriteriaResult.RoomResult room in result.Rooms)
            {
                GameObject newButton = buttonObjectPool.GetObject();
                newButton.transform.SetParent(contentPanel);
                SampleRoomEntry roomEntry = newButton.GetComponent<SampleRoomEntry>();
                roomEntry.Setup(room, this);
            }
        }
        else
        {
            UIControl.GetComponent<UIControl>().PopMessageToScreen(result.ErrorMessage);
            Debug.Log(result.ErrorMessage);
        }
    }
}
