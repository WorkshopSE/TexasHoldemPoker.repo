using UnityEngine;
using UnityEngine.UI;
using LitJson;


public class Register : MonoBehaviour {
    public GameObject username;
    public GameObject password;
    public GameObject confirmPassword;

    private string ConfirmPassword;
    private User current = new User();


    public void RegisterAction()
    {
        if (current.Password != "" && current.Username != "" && ConfirmPassword != "")
        {

            //TODO : HTTP REQ USING FORM (?)
            if (!current.confirmPassword(ConfirmPassword))
            {
                print("Password doesnt match!");
            }
            else
            {
                JsonData userJson = JsonMapper.ToJson(current);
                print("Registration Sucessful");
                Debug.Log(userJson);
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
        current.Username = username.GetComponent<InputField>().text;
        current.Password = password.GetComponent<InputField>().text;
        ConfirmPassword = confirmPassword.GetComponent<InputField>().text;
    }
}
