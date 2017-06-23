using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinButton : MonoBehaviour {
    public GameObject spectate;
    public GameObject active;
    public List<GameObject> chairs;
    public List<GameObject> players;

    private bool flash;
    private int tick = 0;

    public void ChooseFreeChair()
    {
        chairs.ForEach(chair =>
        {
            if (chair.GetComponent<Image>().enabled)
            {
                chair.GetComponent<Button>().enabled = true;
            }
        });
        flash = true;
    }
    void Start()
    {
        InvokeRepeating("RequestUpdate", 0, GameProperties.pollingFrequency);
    }
    void RequestUpdate()
    {
        Debug.Log("tick " + tick++);
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
        players[i].SetActive(true);
        chairs[i].GetComponent<Image>().enabled = false;
        spectate.SetActive(false);
        active.SetActive(true);
    }


}
