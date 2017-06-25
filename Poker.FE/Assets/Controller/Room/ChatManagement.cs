using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatManagement : MonoBehaviour {
	public GameObject LeaveAndSoundOptions;
	public GameObject ChatCreatePanel;

	public void ShowChatCreatePanel()
	{
		if (!ChatCreatePanel.activeSelf) {
			LeaveAndSoundOptions.SetActive (false);
			ChatCreatePanel.SetActive (true);
		}
		else {
			LeaveAndSoundOptions.SetActive (true);
			ChatCreatePanel.SetActive (false);
		}
	}


}
