using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugDealer : MonoBehaviour {

	public Deck player1;
	public Deck player2;
	public Deck player3;
	public Deck player4;

	public Deck dealer;
	public Deck deck;

	List<Deck> playersArray;


	void Start(){
		StartGame ();
	}

	void StartGame(){
		InitializeArray ();
	}

	void InitializeArray(){
		playersArray = new List<Deck>();
		playersArray.Add (player1);
		playersArray.Add (player2);
		playersArray.Add (player3);
		playersArray.Add (player4);

	}

	void OnGUI(){
		if (GUI.Button (new Rect (10, 10, 256, 28), "Press Here!")) {
			foreach (Deck player in playersArray) {
				player.Push (deck.Pop ());
			}

		}

	}
}
