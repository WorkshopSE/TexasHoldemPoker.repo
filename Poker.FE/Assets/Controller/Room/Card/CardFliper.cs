using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFliper : MonoBehaviour {

	SpriteRenderer spriteRenderer;
	CardModel cardModel;

	public AnimationCurve scaleCurve;
	public float duration = 0.5f;

	void Awake(){
		spriteRenderer = GetComponent<SpriteRenderer> ();
		cardModel = GetComponent<CardModel> ();
	}

	public void FlipCard (Sprite startImage, Sprite endImage, int cardIndex){
		StopCoroutine(Flip(startImage, endImage, cardIndex));
		StartCoroutine(Flip(startImage, endImage, cardIndex));
	}

	IEnumerator Flip (Sprite startImage, Sprite endImage, int cardIndex){
		spriteRenderer.sprite = startImage;
		float time = 0f;
		while (time <= 1f) {
			float scale = scaleCurve.Evaluate (time);
			time = time + Time.deltaTime / duration;

			Vector3 localScale = transform.localScale;
			localScale.x = scale;
			transform.localScale = localScale;

			if (time >= 0.5f) {
				spriteRenderer.sprite = endImage;
			}

			yield return new WaitForFixedUpdate();
		}
		if (cardIndex == -1) {
			cardModel.ToggleFace (false);
		} 
		else {
			cardModel.cardIndex = cardIndex;
			cardModel.ToggleFace (true);
		}
	
	}

}
