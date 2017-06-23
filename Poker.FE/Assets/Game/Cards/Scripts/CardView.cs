using System;
using UnityEngine;

public class CardView
{
	public GameObject Card { get; private set; }
	public bool isFaceUp;

	public CardView (GameObject card){
		Card = card;
		isFaceUp = false;
	}
}

