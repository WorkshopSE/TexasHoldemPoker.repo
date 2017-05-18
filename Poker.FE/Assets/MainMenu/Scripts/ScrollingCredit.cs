using UnityEngine;
using System.Collections;

public class ScrollingGUIText : MonoBehaviour
{

    /* Public Variables    */

    // The array of GUIText elements to display and scroll
    public GUIText[] textElements;

    // The delay time before displaying the GUIText elements
    public float displayTime = 5.0f;

    // The delay time before starting the GUIText scroll
    public float scrollTime = 5.0f;

    // The scrolling speed
    public float scrollSpeed = 0.2f;

    // Update is called once per frame
    void Update()
    {

        // start display count down
        displayTime -= Time.deltaTime;

        if (displayTime < 0)
        {
            // if it is time to display, start the scrolling count down timer
            scrollTime -= Time.deltaTime;
        }

        // if it is time to scroll, cycle through the GUIElements and
        // increase their Y position by the desired speed
        if (scrollTime < 0)
        {
            foreach (GUIText text in textElements)
            {
                text.transform.Translate(Vector3.up * scrollSpeed);
            }
        }
    }
}