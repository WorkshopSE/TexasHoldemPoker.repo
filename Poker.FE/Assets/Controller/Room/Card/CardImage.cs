using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class CardImage : MonoBehaviour
{

	Dictionary<int, List<Image>> dictionaryIndexCards;

	public Image imageCardPlayer0_1;
	public Image imageCardPlayer0_2;
	public Image imageCardPlayer1_1;
	public Image imageCardPlayer1_2;
	public Image imageCardPlayer2_1;
	public Image imageCardPlayer2_2;
	public Image imageCardPlayer3_1;
	public Image imageCardPlayer3_2;
	public Image imageCardPlayer4_1;
	public Image imageCardPlayer4_2;
	public Image imageCardPlayer5_1;
	public Image imageCardPlayer5_2;
	public Image imageCardPlayer6_1;
	public Image imageCardPlayer6_2;
	public Image imageCardPlayer7_1;
	public Image imageCardPlayer7_2;
	public Image imageCardPlayer8_1;
	public Image imageCardPlayer8_2;
	public Image imageCardPlayer9_1;
	public Image imageCardPlayer9_2;


	public Image imageCardDealerBurn_1;
	public Image imageCardDealerFlop_1;
	public Image imageCardDealerFlop_2;
	public Image imageCardDealerFlop_3;
	public Image imageCardDealerBurn_2;
	public Image imageCardDealerTurn_1;
	public Image imageCardDealerBurn_3;
	public Image imageCardDealerRiver_1;

	public Sprite backFace;
	public Sprite[] allCards;
	public int index;

	void Awake(){
		InitializeDictionnary ();
	}

	void InitializeDictionnary(){
		dictionaryIndexCards = new Dictionary<int, List<Image>> ();

		List<Image> listCardPlayer0 = new List<Image> ();
		AddToList2 (listCardPlayer0, imageCardPlayer0_1, imageCardPlayer0_1);
		dictionaryIndexCards.Add (0, listCardPlayer0);

		List<Image> listCardPlayer1 = new List<Image> ();
		AddToList2 (listCardPlayer1, imageCardPlayer1_1, imageCardPlayer1_1);
		dictionaryIndexCards.Add (1, listCardPlayer1);

		List<Image> listCardPlayer2 = new List<Image> ();
		AddToList2 (listCardPlayer2, imageCardPlayer2_1, imageCardPlayer2_1);
		dictionaryIndexCards.Add (2, listCardPlayer2);

		List<Image> listCardPlayer3 = new List<Image> ();
		AddToList2 (listCardPlayer3, imageCardPlayer3_1, imageCardPlayer3_1);
		dictionaryIndexCards.Add (3, listCardPlayer3);

		List<Image> listCardPlayer4 = new List<Image> ();
		AddToList2 (listCardPlayer4, imageCardPlayer4_1, imageCardPlayer4_1);
		dictionaryIndexCards.Add (4, listCardPlayer4);

		List<Image> listCardPlayer5 = new List<Image> ();
		AddToList2 (listCardPlayer5, imageCardPlayer5_1, imageCardPlayer5_1);
		dictionaryIndexCards.Add (5, listCardPlayer5);

		List<Image> listCardPlayer6 = new List<Image> ();
		AddToList2 (listCardPlayer6, imageCardPlayer6_1, imageCardPlayer6_1);
		dictionaryIndexCards.Add (6, listCardPlayer6);

		List<Image> listCardPlayer7 = new List<Image> ();
		AddToList2 (listCardPlayer7, imageCardPlayer7_1, imageCardPlayer7_1);
		dictionaryIndexCards.Add (7, listCardPlayer7);

		List<Image> listCardPlayer8 = new List<Image> ();
		AddToList2 (listCardPlayer8, imageCardPlayer8_1, imageCardPlayer8_1);
		dictionaryIndexCards.Add (8, listCardPlayer8);

		List<Image> listCardPlayer9 = new List<Image> ();
		AddToList2 (listCardPlayer9, imageCardPlayer9_1, imageCardPlayer9_1);
		dictionaryIndexCards.Add (9, listCardPlayer9);
	}

	void AddToList2 (List<Image> list, Image img1, Image img2){
		list.Add (img1);
		list.Add (img2);
	}


	public void GiveCardToPlayer(int[] cardsArray, int playerID, int playerIndexGUI){
		int card1 = -1;
		int card2 = -1;
		bool foundFirst = false;

		for (int i = 0; i < cardsArray.Length; i++) {
			if (cardsArray [i] == playerID) {
				if (!foundFirst) {
					card1 = i;
					foundFirst = true;
				}
				else {
					card2 = i;
				}
			}
		}
		if (card1 != -1 && card2 != -1 && dictionaryIndexCards.ContainsKey(playerIndexGUI)) {
			List<Image> listCardPlayer = dictionaryIndexCards[playerIndexGUI];
			listCardPlayer[0].sprite = allCards[card1];
			listCardPlayer [0].enabled = true;
			listCardPlayer[1].sprite = allCards[card2];
			listCardPlayer [1].enabled = true;
		}
	}

	public void GiveCardToOtherPlayer(int[] cardsArray, int otherPlayerIndexGUI){
		
	}

	public void ShowFlop(int[] cardsArray){
		int index1 = -1;
		int index2 = -1;
		int index3 = -1;

		for (int i = 0; i < cardsArray.Length; i++) {
			if (cardsArray [i] == -1) index1 = i;
			else if (cardsArray [i] == -2) index2 = i;
			else if (cardsArray [i] == -3) index3 = i;
		}
		if (index1 != -1 && index2 != -1 && index3 != -1) {
			imageCardDealerFlop_1.sprite = allCards [index1];
			imageCardDealerFlop_2.sprite = allCards [index2];
			imageCardDealerFlop_3.sprite = allCards [index3];
		}
	}
		
	public void ShowTurn(int[] cardsArray){
		int index4 = -1;
		bool found = false;

		for (int i = 0; i < cardsArray.Length && !found; i++) {
			if (cardsArray [i] == -4) index4 = i;
			found = true;
		}
		if (index4 != -1) {
			imageCardDealerTurn_1.sprite = allCards [index4];
		}
	}

	public void ShowRiver(int[] cardsArray){
		int index5 = -1;
		bool found = false;

		for (int i = 0; i < cardsArray.Length && !found; i++) {
			if (cardsArray [i] == -5) index5 = i;
			found = true;
		}
		if (index5 != -1) {
			imageCardDealerRiver_1.sprite = allCards [index5];
		}
	}

	public void Distribute (int indexPlayer){
		//imageCardPlayer1_1.sprite = allCards [index+i];
		//imageCardPlayer1_2.sprite = allCards [index+i+2];
	}

	public void waitCard(){
		System.Threading.Thread.Sleep(1000);
	}
}


