using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugChangeCard : MonoBehaviour {

	CardFliper cardFliper;
	CardModel cardModel;
	int cardIndex = 0;

	public GameObject card;

	void Awake () {
		cardModel = card.GetComponent<CardModel> ();
		cardFliper = card.GetComponent<CardFliper> ();
	}

	void OnGUI(){
		if (GUI.Button (new Rect (10, 10, 100, 28), "Hit Me!")) {

			if (cardIndex >= cardModel.faces.Length) {
				cardIndex = 0;
				cardFliper.FlipCard (cardModel.faces [cardModel.faces.Length - 1], cardModel.cardBack, -1);
			} 
			else {
				if (cardIndex > 0) {
					cardFliper.FlipCard (cardModel.faces [cardIndex - 1], cardModel.faces [cardIndex], cardIndex);
				}
				else {
					cardFliper.FlipCard (cardModel.cardBack, cardModel.faces [cardIndex], cardIndex);
				}
				cardIndex++;
			}
		}
	}

}

