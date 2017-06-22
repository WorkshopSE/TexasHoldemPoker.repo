using UnityEngine;
using UnityEngine.UI;

public class WelcomeText : MonoBehaviour {
    public GameObject text;
    void Start () {
        text.GetComponent<Text>().text = "Welcome User " + GameProperties.user.id;
    }
}
