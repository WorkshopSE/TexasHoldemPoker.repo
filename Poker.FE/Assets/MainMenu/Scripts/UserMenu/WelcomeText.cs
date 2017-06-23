using System;
using UnityEngine;
using UnityEngine.UI;

public class WelcomeText : MonoBehaviour {
    public GameObject text;
    public GameObject editProfileButton;
    public GameObject connectionFeedback;

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
        string profJson = JsonUtility.ToJson(request);
        StartCoroutine(http.POST(URL.GetProfile, profJson ,new Action<string>(GetProfileSuccess), new Action<string>(GetProfileFailed)));
    }

    private void GetProfileFailed(string failedMessage)
    {
        connectionFeedback.GetComponent<Text>().text = failedMessage;
        editProfileButton.GetComponent<Button>().interactable = false;
    }

    private void GetProfileSuccess(string successMessage)
    {
        result = JsonUtility.FromJson<GetProfileResult>(successMessage);
        GameProperties.user.password = result.Password;
        GameProperties.user.userName = result.UserName;
        GameProperties.user.Avatar = result.Avatar;
    }
}
