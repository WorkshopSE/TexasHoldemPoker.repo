using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Register : MonoBehaviour {
    public GameObject username;
    public GameObject password;
    public GameObject deposit;
    public GameObject confirmPassword;
    public GameObject registerFeedback;
    public GameObject UIControl;
    private string ConfirmPassword;
    public float messageDelay = 3f;

    private SignUpRequest current = new SignUpRequest();
    private HttpCallFactory http = new HttpCallFactory();
    private SignUpResult result;


    public void RegisterAction()
    {
        if (current.Password != "" && current.UserName != "" && ConfirmPassword != "" && current.Deposit > 0)
        {
            registerFeedback.GetComponent<Text>().text = "";
            if (ValidatePassword(ConfirmPassword))
            {
                UIControl.GetComponent<UIControl>().ShowLoading();
                string userJson = JsonUtility.ToJson(current);
                StartCoroutine(http.POST(URL.SignUp, userJson, SignUpSuccess, SignUpFail));
                username.GetComponent<InputField>().text = "";
                deposit.GetComponent<InputField>().text = "";
            }
            else
            {
                registerFeedback.GetComponent<Text>().text = "Password Not Match!";
            }
            password.GetComponent<InputField>().text = "";
            confirmPassword.GetComponent<InputField>().text = "";
        }
        else
        {
            registerFeedback.GetComponent<Text>().text = "Invalid Fields";
        }
        StartCoroutine(LateFlushFeedback());
    }

    private IEnumerator LateFlushFeedback()
    {
        yield return new WaitForSeconds(messageDelay);
        if (registerFeedback.GetComponent<Text>().text.Contains("SignUp Sucessful!"))
        {
            UIControl.GetComponent<UIControl>().ChangeScene("MainMenu");
        }
        UIControl.GetComponent<UIControl>().HideLoading();
    }

    private void SignUpFail(string failMessage)
    {
        registerFeedback.GetComponent<Text>().text = "SignUp Failed!\n" + failMessage;
        UIControl.GetComponent<UIControl>().HideLoading();
    }

    private void SignUpSuccess(string successMessage)
    {
        result = JsonUtility.FromJson<SignUpResult>(successMessage);
        if (result.Success)
            registerFeedback.GetComponent<Text>().text = "SignUp Sucessful!";
        else
        {
            registerFeedback.GetComponent<Text>().text = "SignUp Failed!\n" + result.ErrorMessage;
        }
        UIControl.GetComponent<UIControl>().HideLoading();
    }

    private bool ValidatePassword(string otherPassword)
    {
        return current.Password.Equals(otherPassword);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (username.GetComponent<InputField>().isFocused)
            {
                deposit.GetComponent<InputField>().Select();
            }
            if (deposit.GetComponent<InputField>().isFocused)
            {
                password.GetComponent<InputField>().Select();
            }
            if (password.GetComponent<InputField>().isFocused)
            {
                confirmPassword.GetComponent<InputField>().Select();
            }
            
        }
        if (username.GetComponent<InputField>().isFocused || deposit.GetComponent<InputField>().isFocused ||
            password.GetComponent<InputField>().isFocused || confirmPassword.GetComponent<InputField>().isFocused)
        {
            registerFeedback.GetComponent<Text>().text = "";
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            RegisterAction();
        }
        current.UserName = username.GetComponent<InputField>().text;
        current.Password = password.GetComponent<InputField>().text;
        if (deposit.GetComponent<InputField>().text.Length > 0)
            current.Deposit = int.Parse(deposit.GetComponent<InputField>().text);
        ConfirmPassword = confirmPassword.GetComponent<InputField>().text;
    }
}
