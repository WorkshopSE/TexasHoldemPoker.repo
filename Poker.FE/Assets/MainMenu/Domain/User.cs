using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class User {

    public string username;
    public string password;

    public User()
    {
    }

    public bool ConfirmPassword(string otherPassword)
    {
        return this.password.Equals(otherPassword);
    }
}
