using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinButton : MonoBehaviour
{
    public GameObject spectate;
    public GameObject active;
    public Button joinButton;
    public InputField buyInInput;
    public List<GameObject> chairs;
    public List<GameObject> players;
    public Text feedback;

    private bool flash;
    private HttpCallFactory http = new HttpCallFactory();
    private JoinNextHandRequest request;
    private JoinNextHandResult result;

    public void ChooseFreeChair()
    {
        if (GameProperties.CurrentRoom.IsTableFull)
        {
            feedback.text = "Table Is Full";
            return;
        }
        request = new JoinNextHandRequest()
        {
            buyIn = int.Parse(buyInInput.text),
            Player = GameProperties.CurrentRoom.playerid,
            User = GameProperties.user.userName
        };
        chairs.ForEach(chair =>
        {
            if (chair.GetComponent<Image>().enabled)
            {
                chair.GetComponent<Button>().enabled = true;
            }
        });
        joinButton.interactable = false;
        buyInInput.interactable = false;
        flash = true;
    }

    void Update()
    {
        if (flash)
        {
            chairs.ForEach(chair =>
            {
                if (chair.GetComponent<Image>().enabled)
                {
                    float t = Mathf.PingPong(Time.time, 0.3f) / 0.3f;
                    chair.GetComponent<Image>().color = Color.Lerp(Color.yellow, Color.white, t);
                }
            });
        }
        joinButton.interactable = buyInInput.text.Length > 0;
    }
    public void ChairChoosed(int i)
    {
        flash = false;
        chairs.ForEach(chair =>
        {
            if (chair.GetComponent<Image>().enabled)
            {
                chair.GetComponent<Image>().color = Color.white;
                chair.GetComponent<Button>().enabled = false;
            }
        });
        request.seatIndex = i;
        string joinJson = JsonUtility.ToJson(request);
        StartCoroutine(http.POST(URL.JoinNextHand, joinJson, new Action<string>(JoinSuccess), new Action<string>(JoinFail)));
        GameProperties.CurrentRoom.ChairIndex = i;
    }

    private void JoinFail(string failmsg)
    {
        feedback.text = failmsg;
        Debug.Log(failmsg);
        joinButton.interactable = true;
        buyInInput.interactable = true;
    }

    private void JoinSuccess(string successmsg)
    {
        result = JsonUtility.FromJson<JoinNextHandResult>(successmsg);
        if (result.Success)
        {
            //TODO: Add Wallet here (wait for idan to do it)
            GameProperties.user.deposit = result.UserBank;
            spectate.SetActive(false);
            active.SetActive(true);
            players[GameProperties.CurrentRoom.ChairIndex].SetActive(true);
            chairs[GameProperties.CurrentRoom.ChairIndex].GetComponent<Image>().enabled = false;
        }
        else
        {
            feedback.text = result.ErrorMessage;
            joinButton.interactable = true;
            buyInInput.interactable = true;
        }
    }
}
