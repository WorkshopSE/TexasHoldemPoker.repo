using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
public static class URL
{
    public static string Login = "Authentication/Login";
    internal static string SignUp = "Authentication/SignUp";
}

public class HttpCallFactory
{
    private string results;

    /**
     * Note: base URL
     * this url change accoriding to the server location (environment)
     * 
     * TODO: make this use a Relative URL (if possible at C#).
     */
    private const string baseUrl = "http://localhost:51836/api/";

    public string Results
    {
        get
        {
            return results;
        }
    }

    public IEnumerator GET(string url, System.Action<string> onComplete, System.Action<string> onFail)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(baseUrl+url))
        {
            yield return www.Send();

            if (www.isError)
            {
                Debug.Log(www.error);
                onFail("Invalid Server");
            }
            else if (www.responseCode != 200)
            {
                Debug.Log(www.downloadHandler.text);
                onFail("Server Error");
            }
            else
            {
                results = www.downloadHandler.text;
                onComplete(www.downloadHandler.text);
            }
        }
    }

    public IEnumerator POST(string url, string jsonString, System.Action<string> onComplete, System.Action<string> onFail)
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
            onFail("Invalid Server");
        }
        else if (www.responseCode != 200)
        {
            Debug.Log(www.downloadHandler.text);
            onFail("Server Error");
        }
        else
        {
            results = www.downloadHandler.text;
            onComplete(www.downloadHandler.text);
        }
    }
    protected static byte[] GetBytes(string str)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(str);
        return bytes;
    }
}
