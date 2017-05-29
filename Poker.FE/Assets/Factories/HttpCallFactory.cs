using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HttpCallFactory : MonoBehaviour
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

    public WWW GET(string url, System.Action onComplete)
    {
        WWW www = new WWW(baseUrl + url);
        StartCoroutine(WaitForRequest(www, onComplete));
        return www;
    }

    public WWW POST(string url, Dictionary<string, string> post, System.Action onComplete)
    {
        WWWForm form = new WWWForm();

        foreach (KeyValuePair<string, string> post_arg in post)
        {
            form.AddField(post_arg.Key, post_arg.Value);
        }

        WWW www = new WWW(baseUrl + url, form);

        StartCoroutine(WaitForRequest(www, onComplete));
        return www;
    }

    private IEnumerator WaitForRequest(WWW www, System.Action onComplete)
    {
        yield return www;
        // check for errors

        if (www.error == null)
        {
            results = www.text;
            onComplete();
        }
        else
        {
            Debug.Log(www.error);
        }
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

} // class
