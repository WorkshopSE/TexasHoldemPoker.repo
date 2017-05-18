using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Register : MonoBehaviour {
    public GameObject username;
    public GameObject password;
    public GameObject confirmPassword;

    private string Username;
    private string Password;
    private string ConfirmPassword;
    private string form;


    public void RegisterAction()
    {
        if (Password != "" && Username != "" && ConfirmPassword != "")
        {

            //TODO : HTTP REQ USING FORM (?)
            if (!Password.Equals(ConfirmPassword))
            {
                print("Password doesnt match!");
            }
            else
            {
                print("Registration Sucessful");
            }
            username.GetComponent<InputField>().text = "";
            password.GetComponent<InputField>().text = "";
            confirmPassword.GetComponent<InputField>().text = "";
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
            if (password.GetComponent<InputField>().isFocused)
            {
                confirmPassword.GetComponent<InputField>().Select();
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            RegisterAction();
        }
        Username = username.GetComponent<InputField>().text;
        Password = password.GetComponent<InputField>().text;
        ConfirmPassword = confirmPassword.GetComponent<InputField>().text;
    }
}
