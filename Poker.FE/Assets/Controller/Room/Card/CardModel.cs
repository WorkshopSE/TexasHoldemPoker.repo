using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardModel : MonoBehaviour {

	SpriteRenderer spriteRenderer;

	public Sprite[] faces;
	public Sprite cardBack;
	public int cardIndex;
	public bool showFace;

	public void ToggleFace (){
		GameObject card = new GameObject ();
		card.AddComponent<SpriteRenderer> ();
		if (showFace) {
			card.AddComponent<SpriteRenderer> ().sprite = faces [15];
		}
		else {
			card.AddComponent<SpriteRenderer> ().sprite = faces [20];
		}
	}

	public void Awake(){
		spriteRenderer = GetComponent<SpriteRenderer> ();
		spriteRenderer.enabled = true;
	}
}
