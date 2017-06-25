using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public Deck player1;
	public Deck player2;
	public Deck player3;
	public Deck player4;
	public Deck player5;
	public Deck player6;
	public Deck player7;
	public Deck player8;
	public Deck player9;


	public Deck dealerFlop;
	public Deck dealerTurn;
	public Deck dealerRiver;

	public Deck deck;

	public Button distributeCard;
	public Button distributeFlop;
	public Button distributeTurn;
	public Button distributeRiver;

	Dictionary<int, Deck> playersArray;
	Dictionary<int, Button> buttonsArray;

	int playerIndex;
	IEnumerator coroutine;

	public void Burn(){
		HitDealerFlop();
	}

	public void GetPreFlopAnnimation(){
		//Debug.Log("Ariel Get PreFlopAnimation");

		coroutine = GetDealerIndexTurn(0);

		StartCoroutine (coroutine);
	//	Debug.Log("Ariel Start Coroutines");

	}

	public void GetFlopAnnimation(){
		StopCoroutine (coroutine);
		coroutine = GetDealerIndexTurn(1);
		StartCoroutine (coroutine);
	}

	public void GetTurnAnnimation(){
		StopCoroutine (coroutine);
		coroutine = GetDealerIndexTurn(2);
		StartCoroutine (coroutine);
	}

	public void GetRiverAnnimation(){
		StopCoroutine (coroutine);
		coroutine = GetDealerIndexTurn(3);
		StartCoroutine (coroutine);
	}

	void Start(){
		StartGame ();
		playerIndex = -1; //at the first interation it will be 0
	}

	void StartGame(){
		InitializeArray ();
		ShowButton (0);
	}

	int getNextPlayer(){
		playerIndex++;
		if (playerIndex == 9){
			playerIndex = 0;
		}
		Debug.Log ("Now is player "+playerIndex+" turn");
		return playerIndex;
	}

	void InitializeArray(){
		playersArray = new Dictionary<int, Deck> ();
		playersArray.Add (0, player1);
		playersArray.Add (1, player2);
		playersArray.Add (2, player3);
		playersArray.Add (3, player4);
		playersArray.Add (4, player5);
		playersArray.Add (5, player6);
		playersArray.Add (6, player7);
		playersArray.Add (7, player8);
		playersArray.Add (8, player9);

		buttonsArray = new Dictionary<int, Button> ();
		buttonsArray.Add (0, distributeCard);
		buttonsArray.Add (1, distributeFlop);
		buttonsArray.Add (2, distributeTurn);
		buttonsArray.Add (3, distributeRiver);
	}

	IEnumerator GetDealerIndexTurn (int indexA){
		//	Debug.Log("Ariel Get Dealer Index Turn");

		while (indexA != -1) {
			if (indexA == 0) {
				//		Debug.Log("Ariel Get Dealer Index Turn Index =0");

				ShowButton (0);
				DistributePreFlop ();
				yield return new WaitForSeconds (0.5f);
			}
			if (indexA == 1) {
				HitDealerFlop();
				yield return new WaitForSeconds (0.7f);
			}
			else if (indexA == 2) {
				HitDealerTurn();
				yield return new WaitForSeconds (0.7f);
			}
			if (indexA == 3) {
				HitDealerRiver();
				yield return new WaitForSeconds (0.7f);
			}
		}
	}

	void ShowButton(int indexB){
		for (int i = 0; i < buttonsArray.Count; i++) {
			if (i == indexB) {
				buttonsArray [i].interactable = true;
			}
			else {
				buttonsArray [i].interactable = false;
			}
		}
	}

	void DistributePreFlop(){
		//Debug.Log("Ariel Distribute Pre Flop");

		Deck player = playersArray [getNextPlayer ()];
		if (player.CardCount < 2) {
			//Debug.Log("Ariel Distribute Pre Flop Player Count < 2");
			//Debug.Log("Deck is null "+(deck==null));
		//	Debug.Log("BEFORE CardIndexPop ");
		//	Debug.Log("Deck name "+deck.name);

			int cardIndexPop = deck.Pop ();
		//	Debug.Log("Ariel CardIndexPop "+cardIndexPop);
			player.Push (cardIndexPop);
		//	Debug.Log("Ariel Distribute After Push");

		}  
		else {
			ShowButton (1);
		}
	}

	void HitDealerFlop(){
		if (dealerFlop.CardCount < 4) {
			int card = deck.Pop ();
			dealerFlop.Push (card);
			if (dealerFlop.CardCount >= 2) {
				DeckView view = dealerFlop.GetComponent<DeckView> ();
				view.Toggle (card, true);
			}
		}
		else {
			ShowButton (2);
			GetDealerIndexTurn (2);

		}
	}

	void HitDealerTurn(){
		if (dealerTurn.CardCount < 2) {
			int card = deck.Pop ();
			dealerTurn.Push (card);
			if (dealerTurn.CardCount >= 2) {
				DeckView view = dealerTurn.GetComponent<DeckView> ();
				view.Toggle (card, true);
			}
		}
		else {
			ShowButton (3);
			GetDealerIndexTurn (3);
		}
	}

	void HitDealerRiver(){
		if (dealerRiver.CardCount < 2) {
			int card = deck.Pop ();
			dealerRiver.Push (card);
			if (dealerRiver.CardCount >= 2) {
				DeckView view = dealerRiver.GetComponent<DeckView> ();
				view.Toggle (card, true);
			}
		}  
		else {
			ShowButton (4);
			GetDealerIndexTurn (-1);
			StopCoroutine (coroutine);
		}
	}
}

