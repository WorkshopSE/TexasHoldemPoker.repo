using UnityEngine;
using UnityEngine.UI;
using LitJson;


public class Login : MonoBehaviour {
    public GameObject username;
    public GameObject password;

    private User current = new User();


    public void LoginAction()
    {
        if (current.Password != "" && current.Username != "")
        {
            JsonData userJson = JsonMapper.ToJson(current);
            //TODO : HTTP REQ USING FORM (?)
            print("Login Sucessful");
            Debug.Log(userJson);
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
        current.Username = username.GetComponent<InputField>().text;
        current.Password = password.GetComponent<InputField>().text;
    }
}
