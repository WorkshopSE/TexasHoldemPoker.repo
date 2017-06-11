using UnityEngine;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour {
    public GameObject toggle;
    public void Mute(bool toMute)
    {
        PlayerPrefs.SetString("SoundToggle", toMute.ToString());
        AudioListener.volume = toMute  ? 1 : 0;

    }
    void Start()
    {
        if (PlayerPrefs.HasKey("SoundToggle"))
            toggle.GetComponent<Toggle>().isOn = System.Convert.ToBoolean(PlayerPrefs.GetString("SoundToggle"));
    }
}
