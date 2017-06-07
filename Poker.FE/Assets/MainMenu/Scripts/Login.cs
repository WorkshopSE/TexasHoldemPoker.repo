using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Login : MonoBehaviour {
    public GameObject username;
    public GameObject password;
    public GameObject loginFeedback;
    public GameObject UIControl;
    public float messageDelay = 3f;

    private LoginRequest current = new LoginRequest();
    private HttpCallFactory http = new HttpCallFactory();
    private LoginResult result;

    public void LoginAction()
    {
        if (current.Password != "" && current.UserName != "")
        {
            string userJson = JsonUtility.ToJson(current);
            StartCoroutine(http.POST(URL.Login, userJson, new Action<string>(LoginSuccess), new Action<string>(LoginFail)));
            UIControl.GetComponent<UIControl>().ShowLoading();
            username.GetComponent<InputField>().text = "";
            password.GetComponent<InputField>().text = "";
        }
        else
        {
            loginFeedback.GetComponent<Text>().text = "Please Fill all Fields";
        }
            
    }

    private void LoginFail(string failMessage)
    {
        loginFeedback.GetComponent<Text>().text = "Login Failed!\n" + failMessage;
        UIControl.GetComponent<UIControl>().HideLoading();
    }

    private void LoginSuccess(string successMessage)
    {
        result = JsonUtility.FromJson<LoginResult>(successMessage);
        if (result.Success)
            loginFeedback.GetComponent<Text>().text = "Login Sucessful!";
        else
        {
            loginFeedback.GetComponent<Text>().text = "Login Failed!\n" + result.ErrorMessage;
        }
    }

    private IEnumerator LateFlushFeedback()
    {
        yield return new WaitForSeconds(messageDelay);
        if (loginFeedback.GetComponent<Text>().text.Contains("Login Sucessful!"))
        {
            UIControl.GetComponent<UIControl>().ChangeScene("MainMenu");
        }
        UIControl.GetComponent<UIControl>().HideLoading();
        loginFeedback.GetComponent<Text>().text = "";
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (username.GetComponent<InputField>().isFocused)
            {
                password.GetComponent<InputField>().Select();
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            LoginAction();
        }
        current.UserName = username.GetComponent<InputField>().text;
        current.Password = password.GetComponent<InputField>().text;
    }
}
