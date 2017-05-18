using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Login : MonoBehaviour {
    public GameObject username;
    public GameObject password;

    private string Username;
    private string Password;
    private string form;


    public void LoginAction()
    {
        if (Password != "" && Username != "")
        {

            //TODO : HTTP REQ USING FORM (?)
            print("Login Sucessful");
            username.GetComponent<InputField>().text = "";
            password.GetComponent<InputField>().text = "";
        }
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
        Username = username.GetComponent<InputField>().text;
        Password = password.GetComponent<InputField>().text;
    }
}
