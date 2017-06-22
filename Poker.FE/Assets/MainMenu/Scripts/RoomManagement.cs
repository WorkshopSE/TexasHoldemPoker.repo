using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManagement : MonoBehaviour {
    public HttpCallFactory http = new HttpCallFactory();
    public GameObject UIControl;

    public void CreateARoom()
    {
        CreateNewRoomRequest roomCreate = new CreateNewRoomRequest()
        {
            Level = GameProperties.user.level,
            User = GameProperties.user.userName
        };
        string roomCreateJson = JsonUtility.ToJson(roomCreate);
        StartCoroutine(http.POST(URL.RoomCreate, roomCreateJson, RoomCreateComplete, UIControl.GetComponent<UIControl>().PopMessageToScreen));

    }

    private void RoomCreateComplete(string successMessage)
    {
        CreateNewRoomResult result = JsonUtility.FromJson<CreateNewRoomResult>(successMessage);
        if (result.Success)
        {
            UIControl.GetComponent<UIControl>().ChangeScene("Room");
        }
        else
        {
            UIControl.GetComponent<UIControl>().PopMessageToScreen("Our Falta, Please try again");
        }
        //FOR STUB
        UIControl.GetComponent<UIControl>().ChangeScene("Room");

    }
}
