using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Register : MonoBehaviour {
    public GameObject username;
    public GameObject password;
    public GameObject confirmPassword;
    public GameObject registerFeedback;
    private string ConfirmPassword;
    private User current = new User();
    public float messageDelay = 3f;


    public void RegisterAction()
    {
        if (current.password != "" && current.username != "" && ConfirmPassword != "")
        {

            //TODO : HTTP REQ USING FORM (?)
            if (current.ConfirmPassword(ConfirmPassword))
            {
                string userJson = JsonUtility.ToJson(current);
                registerFeedback.GetComponent<Text>().text = "Registration Sucessful";
                Debug.Log(userJson);
                username.GetComponent<InputField>().text = "";
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
            registerFeedback.GetComponent<Text>().text = "Please Fill all Fields";
        }
        StartCoroutine(LateFlushFeedback());
    }

    private IEnumerator LateFlushFeedback()
    {
        yield return new WaitForSeconds(messageDelay);
        registerFeedback.GetComponent<Text>().text = "";
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
            if (password.GetComponent<InputField>().isFocused)
            {
                confirmPassword.GetComponent<InputField>().Select();
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            RegisterAction();
        }
        current.username = username.GetComponent<InputField>().text;
        current.password = password.GetComponent<InputField>().text;
        ConfirmPassword = confirmPassword.GetComponent<InputField>().text;
    }
}
