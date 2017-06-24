using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
public static class URL
{
    public static string Login = "Authentication/Login";
    public static string Logout = "Authentication/Logout";
    public static string SignUp = "Authentication/SignUp";
    public static string RoomCreate = "Room/CreateNewRoom";
    public static string GetProfile = "Profile/GetProfile";
    public static string EditProfile = "Profile/EditProfile";
    public static string KeepAlive = "KeepAlive/KeepAlive";
}

public class HttpCallFactory
{
    private string results;

    /**
     * Note: base URL
     * this url change accoriding to the server location (environment)
     * 
     */
    private const string baseUrl = "http://localhost:9000/api/";

    public string Results
    {
        get
        {
            return results;
        }
    }

    public IEnumerator GET(string url, System.Action<string> onComplete = null, System.Action<string> onFail = null)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(baseUrl+url))
        {
            yield return www.Send();

            if (www.isError)
            {
                Debug.Log(www.error);
                if (onFail != null)
                    onFail(www.error);
            }
            else if (www.responseCode != 200)
            {
                Debug.Log(www.downloadHandler.text);
                if (onFail != null)
                    onFail("Server Error");
            }
            else
            {
                results = www.downloadHandler.text;
                if (onComplete != null)
                    onComplete(www.downloadHandler.text);
            }
        }
    }

    public IEnumerator POST(string url, string jsonString, System.Action<string> onComplete = null, System.Action<string> onFail = null)
    {
        var www = new UnityWebRequest(baseUrl+url, "POST");
        byte[] bodyRaw = GetBytes(jsonString);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.Send();
        if (www.isError)
        {
            Debug.Log(www.error);
            if (onFail != null)
                onFail(www.error);
        }
        else if (www.responseCode != 200)
        {
            Debug.Log(www.downloadHandler.text);
            if (onFail != null)
                onFail("Server Error");
        }
        else
        {
            results = www.downloadHandler.text;
            if (onComplete != null)
                onComplete(www.downloadHandler.text);
        }
    }
    protected static byte[] GetBytes(string str)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(str);
        return bytes;
    }
}
