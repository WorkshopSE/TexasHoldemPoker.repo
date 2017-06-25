using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class CardImage : MonoBehaviour
{
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

	int i=0;


	void Awake(){
		//imageCardPlayer1_1.sprite = allCards [index+i];
		//imageCardPlayer1_2.sprite = allCards [index+i+2];
	}

	public void DisplayCard(){
		imageCardPlayer1_1.sprite = allCards [index+i];
		imageCardPlayer1_2.sprite = allCards [index+i+2];
		i++;
	}


}


