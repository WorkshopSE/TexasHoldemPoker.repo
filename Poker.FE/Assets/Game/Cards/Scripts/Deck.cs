using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {

	List<int> cards;

	public bool isGameDeck; // in order to make a difference between dealer and player

	public IEnumerable<int> GetCards(){
		foreach (int i in cards) {
			yield return i;
		}
	}

	public event CardEventHandler CardRemoved;
	public event CardEventHandler CardAdded;

	public bool HasCards
	{
		get { return cards != null && cards.Count > 0; }
	}

	public int CardCount{
		get{ 
			if (cards == null) {
				return 0;
			}
			else{
				return cards.Count;
			}
		
		}
	}

	void Awake () {
		cards = new List<int> ();
		if (isGameDeck) {
			CreateDeck ();
		}
	}

	public int Pop(){
		int temp = cards [0];
		cards.RemoveAt (0);

		if (CardRemoved != null) {
			CardRemoved (this, new CardEventArgs (temp));
		}

		return temp;
	}

	public void Push (int card){
		if (CardAdded != null) {
			CardAdded (this, new CardEventArgs (card));
		}

		cards.Add (card);
	}

	public int HandValue (int card){
		return ((card% 13)+1);
	}

	public int HandColorValue (int card){
		
		if (card <= 12)
			return 1;
		else if (card <= 25)
			return 2;
		else if (card <= 38)
			return 3;
		else 
			 return 4;
	}

	public string StringHandColorValue(int index){
		if (index == 1)
			return "Heart";
		else if (index == 2)
			return "Diamond";
		else if (index == 3)
			return "Clubs";
		else 
			return "Spades";
	}

	public void CreateDeck(){
		cards.Clear();	

		for (int i = 0; i < 52; i++) {
			cards.Add (i);
		}

		int n = cards.Count;
		while (n > 1) {
			n--;
			int k = Random.Range(0, n+1);
			int temp = cards [k];
			cards [k] = cards[n];
			cards [n] = temp;
		}
	}


	void Update(){}
	

}
