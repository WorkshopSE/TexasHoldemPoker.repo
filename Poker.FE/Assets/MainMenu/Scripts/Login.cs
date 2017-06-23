﻿using System;
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

    public IEnumerator GotoGame()
    {
        yield return new WaitForSeconds(messageDelay);
        UIControl.GetComponent<UIControl>().ChangeScene("UserMenu");
    }

    public void LoginAction()
    {
        if (current.Password != "" && current.UserName != "")
        {
            loginFeedback.GetComponent<Text>().text = "";
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
        {
            GameProperties.user.userName = result.User;
            loginFeedback.GetComponent<Text>().text = "Login Sucessful!";
        }
        else
        {
            loginFeedback.GetComponent<Text>().text = "Login Failed!\n" + result.ErrorMessage;
            UIControl.GetComponent<UIControl>().HideLoading();
        }
    }


    // Update is called once per frame
    void Update()
    { 
        if (loginFeedback.GetComponent<Text>().text.Contains("Login Sucessful!"))
        {
            StartCoroutine(GotoGame());
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (username.GetComponent<InputField>().isFocused)
            {
                password.GetComponent<InputField>().Select();
            }
        }
        if (username.GetComponent<InputField>().isFocused || password.GetComponent<InputField>().isFocused)
        {
            loginFeedback.GetComponent<Text>().text = "";
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            LoginAction();
        }
        current.UserName = username.GetComponent<InputField>().text;
        current.Password = password.GetComponent<InputField>().text;
    }
}
