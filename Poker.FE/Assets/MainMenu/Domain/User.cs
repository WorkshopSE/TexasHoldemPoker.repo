using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User {

    private string username;
    private string password;

#region Properties
    public string Username
    {
        get
        {
            return username;
        }

        set
        {
            username = value;
        }
    }

    public string Password
    {
        get
        {
            return password;
        }

        set
        {
            password = value;
        }
    }
#endregion


    public User()
    {
    }

    public bool confirmPassword(string otherPassword)
    {
        return Password.Equals(otherPassword);
    }
}
