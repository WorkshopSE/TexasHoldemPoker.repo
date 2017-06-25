using System;
using UnityEngine;

public class CardView
{
	public GameObject Card { get; private set; }
	public bool isFaceUp;

	public CardView (GameObject card){
		Card = card;
		Card.SetActive (true);
		Card.GetComponent<Renderer>().enabled = true;
		isFaceUp = false;
	}
}

