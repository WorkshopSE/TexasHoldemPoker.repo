using System;
using UnityEngine;
using UnityEngine.UI;

public class WelcomeText : MonoBehaviour {
    public GameObject text;
    public GameObject editProfileButton;
    public GameObject connectionFeedback;
    public RawImage avatar;

    private HttpCallFactory http;
    private GetProfileRequest request;
    private GetProfileResult result;
    void Start () {
        http = new HttpCallFactory();
        GetProfile();
        text.GetComponent<Text>().text = "Welcome " + GameProperties.user.userName;
    }

    private void GetProfile()
    {
        request = new GetProfileRequest();
        request.UserName = GameProperties.user.userName;
        request.SecurityKey = GameProperties.user.SecurityKey;
        string profJson = JsonUtility.ToJson(request);
        StartCoroutine(http.POST(URL.GetProfile, profJson ,new Action<string>(GetProfileSuccess), new Action<string>(GetProfileFailed)));
    }

    private void GetProfileFailed(string failedMessage)
    {
        connectionFeedback.GetComponent<Text>().text = failedMessage;
        if (editProfileButton != null)
            editProfileButton.GetComponent<Button>().interactable = false;
    }

    private void GetProfileSuccess(string successMessage)
    {
        result = JsonUtility.FromJson<GetProfileResult>(successMessage);
        GameProperties.user.password = result.Password;
        GameProperties.user.userName = result.UserName;
        GameProperties.user.Avatar = result.Avatar;
        if (GameProperties.user.Avatar != null && GameProperties.user.Avatar.Length > 0)
        {
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(GameProperties.user.Avatar);
            avatar.texture = tex;
        }
        
    }
}
