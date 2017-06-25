using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    public GameObject loadingText;
    public GameObject loadingImage;
    public void ChangeScene(string sceneName)
    {
        loadingText.SetActive(true);
        loadingImage.SetActive(true);
        SceneManager.LoadSceneAsync(sceneName);
    }
    // Use this for initialization
    void Start()
    {
        ChangeScene("MainMenu");
    }

}
