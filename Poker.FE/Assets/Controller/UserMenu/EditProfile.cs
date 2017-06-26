﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using SFB;
using System.IO;

public class EditProfile : MonoBehaviour {
    public GameObject username;
    public GameObject password;
    public GameObject avatar;
    public GameObject EditProfileFeedback;
    public GameObject UIControl;

    public float messageDelay = 3f;

    private EditProfileRequest current = new EditProfileRequest();
    private HttpCallFactory http = new HttpCallFactory();
    private EditProfileResult result;

    //for SFB
    public ExtensionFilter[] extensions = new ExtensionFilter[] { new ExtensionFilter("Image Files", "png", "jpg", "jpeg") };

    public void EditProfileAction()
    {
        if (current.newPassword != "" && current.newUserName != "")
        {
            current.UserName = GameProperties.user.userName;
            current.Password = GameProperties.user.password;
            EditProfileFeedback.GetComponent<Text>().text = "";
            UIControl.GetComponent<UIControl>().ShowLoading();
            string profileJson = JsonUtility.ToJson(current);
            StartCoroutine(http.POST(URL.EditProfile, profileJson, EditProfileSuccess, EditProfileFail));
        }
        else
        {
            EditProfileFeedback.GetComponent<Text>().text = "Empty Fields";
        }
        StartCoroutine(LateFlushFeedback());
    }

    private IEnumerator LateFlushFeedback()
    {
        yield return new WaitForSeconds(messageDelay);
        if (EditProfileFeedback.GetComponent<Text>().text.Contains("Edit Profile Sucessful!"))
        {
            UIControl.GetComponent<UIControl>().ChangeScene("UserMenu");
        }
        UIControl.GetComponent<UIControl>().HideLoading();
    }

    private void EditProfileFail(string failMessage)
    {
        EditProfileFeedback.GetComponent<Text>().text = "Edit Profile Failed!\n" + failMessage;
        UIControl.GetComponent<UIControl>().HideLoading();
    }

    private void EditProfileSuccess(string successMessage)
    {
        result = JsonUtility.FromJson<EditProfileResult>(successMessage);
        if (result.Success)
        {
            EditProfileFeedback.GetComponent<Text>().text = "Edit Profile Sucessful!";
            GameProperties.user.Avatar = result.newAvatar;
            GameProperties.user.password = result.newPassword;
            GameProperties.user.userName = result.newUserName;
        }
        else
        {
            EditProfileFeedback.GetComponent<Text>().text = "Edit Profile Failed!\n" + result.ErrorMessage;
        }
        UIControl.GetComponent<UIControl>().HideLoading();
    }
    void Start()
    {
        if (GameProperties.user.Avatar != null)
        {
            Texture2D tex = new Texture2D(2,2);
            tex.LoadImage(GameProperties.user.Avatar);
            avatar.GetComponent<RawImage>().texture = tex;
        }
        username.GetComponent<InputField>().text = GameProperties.user.userName;
        password.GetComponent<InputField>().text = GameProperties.user.password;
    }
    public void UpdateAvater()
    {
        var paths = StandaloneFileBrowser.OpenFilePanel("Open Avatar", "", extensions, false);
        if (paths.Length > 0)
        {
            StartCoroutine(OutputRoutine(new System.Uri(paths[0]).AbsoluteUri));
        }
    }
    private IEnumerator OutputRoutine(string url)
    {
        Debug.Log("URL: " + url);
        var loader = new WWW(url);
        yield return loader;
        avatar.GetComponent<RawImage>().texture = loader.texture;
        current.newAvatar = loader.texture.EncodeToPNG();
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
        if (username.GetComponent<InputField>().isFocused || password.GetComponent<InputField>().isFocused)
        {
            EditProfileFeedback.GetComponent<Text>().text = "";
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            EditProfileAction();
        }
        current.newUserName = username.GetComponent<InputField>().text;
        current.newPassword = password.GetComponent<InputField>().text;
    }
}