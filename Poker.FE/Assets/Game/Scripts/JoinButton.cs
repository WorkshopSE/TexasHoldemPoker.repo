using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinButton : MonoBehaviour {
    public GameObject chairs;
    public GameObject players;
    public GameObject spectate;
    public GameObject active;

    private bool flash;
    private List<GameObject> chairList;
    private List<GameObject> playerList;

    public void ChooseFreeChair()
    {
        chairList.ForEach(chair =>
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
        chairList = new List<GameObject>();
        foreach (Transform t in chairs.transform)
            chairList.Add(t.gameObject);
        playerList = new List<GameObject>();
        foreach (Transform t in players.transform)
            playerList.Add(t.gameObject);
    }

    void Update()
    {
        if (flash)
        {
            chairList.ForEach(chair =>
            {
                if (chair.GetComponent<Image>().enabled)
                {
                    float t = Mathf.PingPong(Time.time, 0.5f) / 0.5f;
                    chair.GetComponent<Image>().color = Color.Lerp(Color.yellow, Color.white, t);
                }
            });
        }
    }
    public void ChairChoosed(int i)
    {
        chairList.ForEach(chair =>
        {
            if (chair.GetComponent<Image>().enabled)
            {
                chair.GetComponent<Image>().color = Color.white;
                chair.GetComponent<Button>().enabled = false;
            }
        });
        playerList[i].SetActive(true);
        chairList[i].GetComponent<Image>().enabled = false;
        spectate.SetActive(false);
        active.SetActive(true);
        flash = false;

    }


}
