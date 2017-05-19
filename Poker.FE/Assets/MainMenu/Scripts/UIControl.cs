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
        ShowLoading();
        SceneManager.LoadSceneAsync(sceneName);
    }
    public void DoExitGame()
    {
        Application.Quit();
    }
    public void ShowLoading()
    {
        if (loadingText.activeSelf || loadingImage.activeSelf)
        {
            return;
        }
        loadingText.SetActive(true);
        loadingImage.SetActive(true);
    }
}
