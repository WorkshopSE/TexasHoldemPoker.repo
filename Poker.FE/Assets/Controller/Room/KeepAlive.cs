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
    public List<Image> smallBlinds;
    public List<Image> bigBlinds;
    public List<Text> playerStates;
    public List<Image> playerCards;
    public List<Image> playerChips;
    public GameObject playerTurnPanel;

    private int delayCounter = 0;
    private HttpCallFactory http;
    private KeepAliveRequest request;
    private KeepAliveResult result;
    private string requestJson;
    private int currentPlayerID = -1;

    private Button call;
    private Button bet;
    private Button check;
    private Button raise;
    private Button fold;
    private Button allIn;
    private Slider slider;

    // Use this for initialization
    void Start()
    {
        call = playerTurnPanel.transform.Find("Call").gameObject.GetComponent<Button>();
        bet = playerTurnPanel.transform.Find("Bet").gameObject.GetComponent<Button>();
        check = playerTurnPanel.transform.Find("Check").gameObject.GetComponent<Button>();
        raise = playerTurnPanel.transform.Find("Raise").gameObject.GetComponent<Button>();
        fold = playerTurnPanel.transform.Find("Fold").gameObject.GetComponent<Button>();
        allIn = playerTurnPanel.transform.Find("AllIn").gameObject.GetComponent<Button>();
        slider = playerTurnPanel.transform.Find("Slider").gameObject.GetComponent<Slider>();
        request = new KeepAliveRequest()
        {
            Room = GameProperties.CurrentRoom.id,
            UserName = GameProperties.user.userName,
            PlayerID = GameProperties.CurrentRoom.playerid,
            SecurityKey = GameProperties.user.SecurityKey
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
        Debug.Log("Keep Alive FAIL Error " + failMessage);
    }

    private void UpdateComplete(string successMessage)
    {
        delayCounter = 0;
        delayFeedback.GetComponent<Text>().text = "";
        result = JsonUtility.FromJson<KeepAliveResult>(successMessage);
        if (!result.Success)
        {
            delayFeedback.GetComponent<Text>().text = "Error while trying to get update. \n Error message: " + result.ErrorMessage;
            Debug.Log("Keep Alive SUCCESS Error " + result.ErrorMessage);
        }
        else
        {

            for (int chairIndex = 0; chairIndex < result.TableLocationOfActivePlayers.Length; chairIndex++)
            {
                smallBlinds[chairIndex].enabled = false;
                bigBlinds[chairIndex].enabled = false;
                dealers[chairIndex].enabled = false;
                playerStates[chairIndex].text = result.PlayersStates[chairIndex] == "Passive" ? "" : result.PlayersStates[chairIndex];
                int playerID = result.TableLocationOfActivePlayers[chairIndex];
                if (playerID != currentPlayerID)
                {
                    players[chairIndex].GetComponent<Animator>().enabled = false;
                }
                if (playerID > 0)
                {
                    players[chairIndex].SetActive(true);
                    chairs[chairIndex].GetComponent<Image>().enabled = false;
                    if (playerID == result.CurrentPlayerID && result.CurrentPlayerID != currentPlayerID)
                    {
                        currentPlayerID = playerID;
                        players[chairIndex].GetComponent<Animator>().enabled = true;
                        //cardImage.GiveCardToPlayer(result.PlayersAndTableCards, playerID, chairIndex);
                    }
                    if (playerID == result.DealerId)
                    {
                        dealers[chairIndex].enabled = true;
                    }
                    if (playerID == result.SmallBlindId)
                    {
                        smallBlinds[chairIndex].enabled = true;
                    }
                    if (playerID == result.BigBlindId)
                    {
                        bigBlinds[chairIndex].enabled = true;
                    }
                    //TODO: Ariel - show here cards and current chips to the spesific PlayerID at chair location chairs[chairIndex]
                }
                else
                {
                    //TODO: Ariel - here you disable chips and cards for players who stand up
                    players[chairIndex].SetActive(false);
                    chairs[chairIndex].GetComponent<Image>().enabled = true;
                }
            }
            GameProperties.CurrentRoom.IsTableFull = result.IsTableFull;
            GameProperties.CurrentRoom.PlayerWallet = result.PlayerWallet;
            if (result.CurrentPlayerID == GameProperties.CurrentRoom.playerid && !playerTurnPanel.activeSelf)
            {
                SetupButtons();
                if (result.LastRaise == 0)
                {
                    call.interactable = false;
                    raise.interactable = false;
                }
                else
                {
                    bet.interactable = false;
                    check.interactable = false;
                }
                playerTurnPanel.SetActive(true);
            }
        }

    }

    private void SetupButtons()
    {
        call.interactable = true;
        bet.interactable = true;
        check.interactable = true;
        raise.interactable = true;
        fold.interactable = true;
        allIn.interactable = true;
        slider.minValue = (float)result.LastRaise;
        slider.value = (float)result.LastRaise;
        slider.maxValue = (float)GameProperties.CurrentRoom.PlayerWallet;
    }
}
