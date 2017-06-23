using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Deck))]
public class DeckView : MonoBehaviour {

	Deck deck;
	Dictionary <int, CardView> fetchedCards;
	int lastCount;

	public Vector3 start;
	public float cardOffset;
	public GameObject cardPrefab;
	public bool faceUp = false;
	public bool reverseLayerOrder = false;


	public void Toggle (int card, bool isFaceUp){
		fetchedCards [card].isFaceUp = isFaceUp;
	}

	void Start(){
		fetchedCards = new Dictionary <int, CardView> ();
		deck = GetComponent<Deck> ();
		ShowCards (); // dealer
		lastCount = deck.CardCount;

		deck.CardRemoved += Deck_CardRemoved;
		deck.CardAdded += Deck_CardAdded;
	}

	void Update(){
		if (lastCount != deck.CardCount) {
			lastCount = deck.CardCount;
			ShowCards(); // player
		}
	}

	void Deck_CardRemoved(object sender, CardEventArgs e){
		if (fetchedCards.ContainsKey (e.CardIndex)) {
			Destroy (fetchedCards [e.CardIndex].Card);
			fetchedCards.Remove (e.CardIndex);
		}
	}

	void Deck_CardAdded(object sender, CardEventArgs e){
		float co = cardOffset * deck.CardCount;
		Vector3 temp = start + new Vector3 (co, 0f);
		AddCard (temp, e.CardIndex, deck.CardCount);
	}

	void ShowCards(){
		int cardCount = 0;

		if (deck.HasCards) {
			
			foreach (int i in deck.GetCards()) {
				
				float co = cardOffset * cardCount;
				Vector3 temp = start + new Vector3 (co, 0f);
				AddCard (temp, i, cardCount);

				cardCount++;
			}
		}
	}

	void AddCard(Vector3 position, int cardIndex, int positionalIndex){
		if (fetchedCards.ContainsKey (cardIndex)) {
			if (!faceUp) {
				CardModel model = fetchedCards [cardIndex].Card.GetComponent<CardModel> ();
				model.ToggleFace (fetchedCards [cardIndex].isFaceUp);
			}
			return;
		}

		GameObject cardCopy = (GameObject)Instantiate (cardPrefab);
		cardCopy.transform.position = position;

		CardModel cardModel = cardCopy.GetComponent<CardModel> ();
		cardModel.cardIndex = cardIndex;
		cardModel.ToggleFace (faceUp);

		SpriteRenderer spriteRenderer = cardCopy.GetComponent<SpriteRenderer> ();
		if (reverseLayerOrder) {
			spriteRenderer.sortingOrder = 51 - positionalIndex;
		}
		else {
			spriteRenderer.sortingOrder = positionalIndex;
		}

		fetchedCards.Add (cardIndex, new CardView(cardCopy));

		Debug.Log ("Hand Value = " + deck.HandValue(cardIndex) + " "+deck.StringHandColorValue(deck.HandColorValue(cardIndex)));
	}

}
