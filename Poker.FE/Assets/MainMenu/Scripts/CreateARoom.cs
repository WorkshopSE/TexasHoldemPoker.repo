using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class CreateARoom : MonoBehaviour {
    public GameObject roomName;
    public GameObject antes;
    public GameObject buyInCost;
    public GameObject minimumBet;
    public GameObject minNumberOfPlayers;
    public GameObject maxNumberOfPlayers;
    public GameObject createFeedback;
    public GameObject limitInput;
    public GameObject UIControl;
    public float messageDelay = 3f;

    private CreateNewRoomRequest current;
    private HttpCallFactory http = new HttpCallFactory();
    private CreateNewRoomResult result;


    public void CreateRoomAction()
    {
        if (current.Name != "" && current.Antes >= 0 && current.BuyInCost > 0 && current.MinNumberOfPlayers > 0 && current.MaxNumberOfPlayers > 0 && (!limitInput.activeSelf || (limitInput.activeSelf && current.Limit > 0)))
        {
            limitInput.SetActive(false);
            createFeedback.GetComponent<Text>().text = "";
            if (current.MaxNumberOfPlayers >= current.MinNumberOfPlayers)
            {
                UIControl.GetComponent<UIControl>().ShowLoading();
                current.Level = GameProperties.user.level;
                current.User = GameProperties.user.userName;
                string userJson = JsonUtility.ToJson(current);
                StartCoroutine(http.POST(URL.RoomCreate, userJson, CreateRoomSuccess, CreateRoomFail));
                roomName.GetComponent<InputField>().text = "";
                antes.GetComponent<InputField>().text = "";
                buyInCost.GetComponent<InputField>().text = "";
                minimumBet.GetComponent<InputField>().text = "";
                minNumberOfPlayers.GetComponent<InputField>().text = "";
                maxNumberOfPlayers.GetComponent<InputField>().text = "";
                limitInput.GetComponent<InputField>().text = "";
            }
            else
            {
                createFeedback.GetComponent<Text>().text = "Max players should be more or equal to min players";
            }
        }
        else
        {
            createFeedback.GetComponent<Text>().text = "Invalid Fields";
        }
        StartCoroutine(LateFlushFeedback());
    }
    public void Awake()
    {
        current = new CreateNewRoomRequest()
        {
            Limit = 0,
            IsSpactatorsAllowed = true
        };
    }

    private IEnumerator LateFlushFeedback()
    {
        yield return new WaitForSeconds(messageDelay);
        if (createFeedback.GetComponent<Text>().text.Contains("Creation Sucessful!"))
        {
            UIControl.GetComponent<UIControl>().ChangeScene("Room");
        }
        UIControl.GetComponent<UIControl>().HideLoading();
    }

    private void CreateRoomFail(string failMessage)
    {
        createFeedback.GetComponent<Text>().text = "Creation Failed!\n" + failMessage;
        UIControl.GetComponent<UIControl>().HideLoading();
    }

    private void CreateRoomSuccess(string successMessage)
    {
        result = JsonUtility.FromJson<CreateNewRoomResult>(successMessage);
        if (result.Success)
        {
            GameProperties.CurrentRoom.id = result.Room;
            GameProperties.CurrentRoom.playerid = result.Player;
            GameProperties.CurrentRoom.roomName = result.RoomName;
            createFeedback.GetComponent<Text>().text = "Creation Sucessful!";
        }
        else
        {
            createFeedback.GetComponent<Text>().text = "Creation Failed!\n" + result.ErrorMessage;
        }
        UIControl.GetComponent<UIControl>().HideLoading();
    }
    public void FillLimit(int choose)
    {
        if (choose == 1)
        {
            limitInput.SetActive(true);
        }
        if (choose == 0)
        {
            current.Limit = 0;
            limitInput.SetActive(false);
        }
        if (choose == 2)
        {
            current.Limit = -1;
            limitInput.SetActive(false);
        }
        Debug.Log(choose);
    }
    public void ChooseSpectatorMode(int choose)
    {
        current.IsSpactatorsAllowed = choose == 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (roomName.GetComponent<InputField>().isFocused)
            {
                antes.GetComponent<InputField>().Select();
            }
            if (antes.GetComponent<InputField>().isFocused)
            {
                buyInCost.GetComponent<InputField>().Select();
            }
            if (buyInCost.GetComponent<InputField>().isFocused)
            {
                minimumBet.GetComponent<InputField>().Select();
            }
            if (minimumBet.GetComponent<InputField>().isFocused)
            {
                minNumberOfPlayers.GetComponent<InputField>().Select();
            }
            if (minNumberOfPlayers.GetComponent<InputField>().isFocused)
            {
                maxNumberOfPlayers.GetComponent<InputField>().Select();
            }
        }
        if (roomName.GetComponent<InputField>().isFocused || antes.GetComponent<InputField>().isFocused ||
            buyInCost.GetComponent<InputField>().isFocused || minimumBet.GetComponent<InputField>().isFocused
            || minimumBet.GetComponent<InputField>().isFocused)
        {
            createFeedback.GetComponent<Text>().text = "";
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            CreateRoomAction();
        }
        current.Name = roomName.GetComponent<InputField>().text;
        try
        {
            if (antes.GetComponent<InputField>().text.Length > 0)
                current.Antes = int.Parse(antes.GetComponent<InputField>().text);
            if (buyInCost.GetComponent<InputField>().text.Length > 0)
                current.BuyInCost = int.Parse(buyInCost.GetComponent<InputField>().text);
            if (minimumBet.GetComponent<InputField>().text.Length > 0)
                current.MinimumBet = int.Parse(minimumBet.GetComponent<InputField>().text);
            if (minNumberOfPlayers.GetComponent<InputField>().text.Length > 0)
                current.MinNumberOfPlayers = int.Parse(minNumberOfPlayers.GetComponent<InputField>().text);
            if (maxNumberOfPlayers.GetComponent<InputField>().text.Length > 0)
                current.MaxNumberOfPlayers = int.Parse(maxNumberOfPlayers.GetComponent<InputField>().text);
            if (limitInput.activeSelf && limitInput.GetComponent<InputField>().text.Length > 0)
                current.Limit = int.Parse(limitInput.GetComponent<InputField>().text);
        }
        catch (FormatException)
        {
            createFeedback.GetComponent<Text>().text = "We support only positive numbers";
        }
    }

}
