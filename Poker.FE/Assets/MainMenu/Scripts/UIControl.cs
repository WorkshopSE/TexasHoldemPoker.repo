using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour {
    public GameObject loadingText;
    public GameObject loadingImage;
	public void ChangeScene(string sceneName)
    {
        loadingText.SetActive(true);
        loadingImage.SetActive(true);
        Application.LoadLevel(sceneName);
    }
}
