using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Attached to Main Camera
/// </summary>
public class MainMenu : MonoBehaviour {
    public Texture backgroundTexture;

    private void OnGUI()
    {
        // Display our Background Texture
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundTexture);
    }
}
