using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManagement : MonoBehaviour {
    public GameObject RoomOptions;
    public GameObject RoomCreatePanel;
    public GameObject roomListPanel;


    public void DoChooseARoom()
    {
        RoomOptions.SetActive(false);
        roomListPanel.SetActive(true);
    }

    public void ShowRoomCreatePanel()
    {
        RoomOptions.SetActive(false);
        RoomCreatePanel.SetActive(true);
    }
    public void ShowRoomOptions()
    {
        RoomCreatePanel.SetActive(false);
        RoomOptions.SetActive(true);
    }
}
