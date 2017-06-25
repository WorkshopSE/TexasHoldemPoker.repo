using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour {
    public GameObject text;
    void Start () {
        text.GetComponent<Text>().text = "Player " + GameProperties.CurrentRoom.playerid +"\nRoom " + GameProperties.CurrentRoom.roomName + "\nCurrent Wallet " + GameProperties.CurrentRoom.PlayerWallet;
    }
}
