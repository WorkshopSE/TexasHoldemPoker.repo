using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeepAlive : MonoBehaviour
{
    public GameObject delayFeedback;
    public List<GameObject> chairs;
    public List<GameObject> players;
    public List<Image> dealers;
    public List<Text> playerStates;

    private int delayCounter = 0;
    private HttpCallFactory http;
    private KeepAliveRequest request;
    private KeepAliveResult result;
    private string requestJson;
    private int currentPlayerID = 0;

    // Use this for initialization
    void Start()
    {
        request = new KeepAliveRequest()
        {
            Room = GameProperties.CurrentRoom.id,
            User = GameProperties.user.userName,
            PlayerID = GameProperties.CurrentRoom.playerid
        };
        requestJson = JsonUtility.ToJson(request);
        http = new HttpCallFactory();
        InvokeRepeating("RequestUpdate", 0, GameProperties.pollingFrequency);
    }
    void RequestUpdate()
    {
        StartCoroutine(http.POST(URL.KeepAlive, requestJson, new System.Action<string>(UpdateComplete), new System.Action<string>(UpdateFailed)));
        
    }

    private void UpdateFailed(string failMessage)
    {
        delayFeedback.GetComponent<Text>().text = "Please wait [Delay count = " + ++delayCounter + " sec]";
        Debug.Log("Keep Alive Error " + failMessage);
    }

    private void UpdateComplete(string successMessage)
    {
        delayCounter = 0;
        delayFeedback.GetComponent<Text>().text = "";
        result = JsonUtility.FromJson<KeepAliveResult>(successMessage);
        if (!result.Success)
        {
            delayFeedback.GetComponent<Text>().text = "Error while trying to get update. \n Error message: " + result.ErrorMessage;
        }
        else
        {
            //TODO: Add update to table
            for (int chairIndex = 0; chairIndex < result.TableLocationOfActivePlayers.Length; chairIndex++)
            {
                playerStates[chairIndex].text = result.PlayersStates[chairIndex] == "passive" ? "" : result.PlayersStates[chairIndex];
                int playerID = result.TableLocationOfActivePlayers[chairIndex];
                players[chairIndex].GetComponent<Animator>().enabled = false;
                if (playerID > 0)
                {
                    players[chairIndex].SetActive(true);
                    chairs[chairIndex].GetComponent<Image>().enabled = false;
                    if (playerID == result.CurrentPlayerID && result.CurrentPlayerID != currentPlayerID)
                    {
                        currentPlayerID = playerID;
                        players[chairIndex].GetComponent<Animator>().enabled = true;
                    }
                }
                else
                {
                    players[chairIndex].SetActive(true);
                    chairs[chairIndex].GetComponent<Image>().enabled = false;
                }
            }
            GameProperties.CurrentRoom.IsTableFull = result.IsTableFull;
            GameProperties.CurrentRoom.PlayerWallet = result.PlayerWallet;
        }
        
    }
}
