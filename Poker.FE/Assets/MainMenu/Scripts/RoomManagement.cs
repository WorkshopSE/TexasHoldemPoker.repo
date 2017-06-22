using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManagement : MonoBehaviour {
    public GameObject RoomOptions;
    public GameObject RoomCreatePanel;

    public void ShowRoomCreatePanel()
    {
        RoomOptions.SetActive(false);
        RoomCreatePanel.SetActive(true);
    }
}
