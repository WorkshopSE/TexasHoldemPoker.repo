using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Login : MonoBehaviour {
    public GameObject username;
    public GameObject password;
    public GameObject loginFeedback;
    public float messageDelay = 3f;

    private User current = new User();


    public void LoginAction()
    {
        if (current.password != "" && current.username != "")
        {
            string userJson = JsonUtility.ToJson(current);
            //TODO : HTTP REQ USING FORM (?)
            loginFeedback.GetComponent<Text>().text = "Login Sucessful!";
            Debug.Log(userJson);
            username.GetComponent<InputField>().text = "";
            password.GetComponent<InputField>().text = "";
        }
        else
        {
            loginFeedback.GetComponent<Text>().text = "Please Fill all Fields";
        }
        StartCoroutine(LateFlushFeedback());
    }

    private IEnumerator LateFlushFeedback()
    {
        yield return new WaitForSeconds(messageDelay);

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
        current.username = username.GetComponent<InputField>().text;
        current.password = password.GetComponent<InputField>().text;
    }
}
