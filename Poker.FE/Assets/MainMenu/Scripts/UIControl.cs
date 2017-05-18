using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour {
    public GameObject loadingText;
    public GameObject loadingImage;
    bool isMute;
    public void ChangeScene(string sceneName)
    {
        loadingText.SetActive(true);
        loadingImage.SetActive(true);
        SceneManager.LoadSceneAsync(sceneName);
    }
    public void DoExitGame()
    {
        Application.Quit();
    }
}
