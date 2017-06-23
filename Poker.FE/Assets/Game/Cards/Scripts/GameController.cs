using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public Deck player1;
	public Deck player2;
	public Deck player3;
	public Deck player4;

	public Deck dealer;
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
		HitDealer();
	}

	public void GetPreFlopAnnimation(){
		coroutine = GetDealerIndexTurn(0);
		StartCoroutine (coroutine);
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
	}

	void StartGame(){
		
		InitializeArray ();
		//StartPreFlop();
	}

	int getNextPlayer(){
		playerIndex++;
		if (playerIndex == 4){
			playerIndex = 0;
		}
		return playerIndex;
	}

	void InitializeArray(){
		playersArray = new Dictionary<int, Deck> ();
			playersArray.Add (0, player1);
			playersArray.Add (1, player2);
			playersArray.Add (2, player3);
			playersArray.Add (3, player4);
		buttonsArray = new Dictionary<int, Button> ();
			buttonsArray.Add (0, distributeCard);
			buttonsArray.Add (1, distributeFlop);
			buttonsArray.Add (2, distributeTurn);
			buttonsArray.Add (3, distributeRiver);
	}

	IEnumerator GetDealerIndexTurn (int index){
		while (index != -1) {
			if (index == 0) {
				ShowButton (0);
				DistributePreFlop ();
				yield return new WaitForSeconds (0.5f);
			}
			if (index == 1) {
				HitDealer();
				yield return new WaitForSeconds (0.7f);
			}
			else if (index == 2) {
				HitDealerTurn();
				yield return new WaitForSeconds (0.7f);
			}
			if (index == 3) {
				HitDealerRiver();
				yield return new WaitForSeconds (0.7f);
			}
		}
	}

	void ShowButton(int index){
		for (int i = 0; i < buttonsArray.Count; i++) {
			if (i == index) {
				buttonsArray [i].interactable = true;
			}
			else {
				buttonsArray [i].interactable = false;
			}
		}
	}

	void DistributePreFlop(){
		Deck player = playersArray [getNextPlayer ()];
		if (player.CardCount < 2) {
			player.Push (deck.Pop ());
		} 
		else {
			ShowButton (1);
		}
	}

	void HitDealer(){
		if (dealer.CardCount < 4) {
			int card = deck.Pop ();
			dealer.Push (card);
			if (dealer.CardCount >= 2) {
				DeckView view = dealer.GetComponent<DeckView> ();
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
