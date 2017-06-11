using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundToggle : MonoBehaviour {
    public void Mute(bool toMute)
    {
        AudioListener.volume = toMute  ? 1 : 0;
    }
}
