using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour {
    public GameObject text;
    void Start () {
        text.GetComponent<Text>().text = "Player " + GameProperties.CurrentRoom.Player +"\n Room " + GameProperties.CurrentRoom.id;
    }
}
