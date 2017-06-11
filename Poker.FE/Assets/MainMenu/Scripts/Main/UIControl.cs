using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIControl : MonoBehaviour {
    private Dictionary<string,bool> implementedScenes = new Dictionary<string, bool>
    {
        { "ContactUs", true }, { "LoadingGame", true },{ "MainMenu", true }, { "SignIn", true }, { "SignUp", true }, { "UserMenu", true }
    };
    public GameObject loadingText;
    public GameObject loadingImage;
    public GameObject NotImplementedTextObjet;
    public HttpCallFactory http = new HttpCallFactory();
    bool isMute;
    public void ChangeScene(string sceneName)
    {
        if (implementedScenes.ContainsKey(sceneName) && implementedScenes[sceneName])
        {
            ShowLoading();
            SceneManager.LoadSceneAsync(sceneName);
        }
        else
            ShowNotImplementedText();
    }
    public void DoExitGame()
    {
        Application.Quit();
    }
    public void DoLogout()
    {
        LogoutRequest logout = new LogoutRequest();
        logout.User = CurrentUser.user.id;
        string userJson = JsonUtility.ToJson(logout);
        StartCoroutine(http.POST(URL.Logout, userJson, LogoutCompleted,LogoutFaild));
    }

    private void LogoutFaild(string obj)
    {
        //Fake It
        ChangeScene("MainMenu");
    }

    private void LogoutCompleted(string result)
    {
        ChangeScene("MainMenu");
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
    public void HideLoading()
    {
        if (!loadingText.activeSelf && !loadingImage.activeSelf)
        {
            return;
        }
        loadingText.SetActive(false);
        loadingImage.SetActive(false);
    }
    public void ShowNotImplementedText()
    {
        GameObject canvas = GameObject.Find("Canvas");
        if (NotImplementedTextObjet == null)
        {
            NotImplementedTextObjet = new GameObject();
            NotImplementedTextObjet.transform.SetParent(canvas.transform);
            RectTransform rt = NotImplementedTextObjet.AddComponent<RectTransform>();
            rt.sizeDelta = new Vector2(500, 500);
            Outline ol = NotImplementedTextObjet.AddComponent<Outline>();
            ol.effectColor = Color.black;

            Text NotImplementedText = NotImplementedTextObjet.AddComponent<Text>();
            NotImplementedText.text = "Not Implemented Yet :)";

            Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
            NotImplementedText.font = ArialFont;
            
            NotImplementedText.material = ArialFont.material;
            NotImplementedText.fontSize = 60;
            NotImplementedText.transform.position = new Vector2(Screen.width / 2, Screen.height / 2);
            NotImplementedText.color = Color.red;
            NotImplementedText.fontStyle = FontStyle.Bold;
            NotImplementedText.alignment = TextAnchor.MiddleCenter;
            StartCoroutine(WaitAndDestroy(NotImplementedTextObjet));
            
        }
        
    }

    private IEnumerator WaitAndDestroy(GameObject notImplementedTextObjet)
    {
        yield return new WaitForSeconds(3);
        Destroy(NotImplementedTextObjet);
    }
}
