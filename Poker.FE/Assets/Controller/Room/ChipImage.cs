using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ChipImage : MonoBehaviour
{
	Dictionary <int, List<Image>> listAllChips;
	Dictionary <int, List<Image>> listAllChipsTable;

	int i=0;

	public Image imageChipPlayer0_1;
	public Image imageChipPlayer0_2;
	public Image imageChipPlayer0_3;
	public Image imageChipPlayer0_4;
	public Image imageChipPlayer0_5;
	public Image imageChipPlayer1_1;
	public Image imageChipPlayer1_2;
	public Image imageChipPlayer1_3;
	public Image imageChipPlayer1_4;
	public Image imageChipPlayer1_5;
	public Image imageChipPlayer2_1;
	public Image imageChipPlayer2_2;
	public Image imageChipPlayer2_3;
	public Image imageChipPlayer2_4;
	public Image imageChipPlayer2_5;
	public Image imageChipPlayer3_1;
	public Image imageChipPlayer3_2;
	public Image imageChipPlayer3_3;
	public Image imageChipPlayer3_4;
	public Image imageChipPlayer3_5;
	public Image imageChipPlayer4_1;
	public Image imageChipPlayer4_2;
	public Image imageChipPlayer4_3;
	public Image imageChipPlayer4_4;
	public Image imageChipPlayer4_5;
	public Image imageChipPlayer5_1;
	public Image imageChipPlayer5_2;
	public Image imageChipPlayer5_3;
	public Image imageChipPlayer5_4;
	public Image imageChipPlayer5_5;
	public Image imageChipPlayer6_1;
	public Image imageChipPlayer6_2;
	public Image imageChipPlayer6_3;
	public Image imageChipPlayer6_4;
	public Image imageChipPlayer6_5;
	public Image imageChipPlayer7_1;
	public Image imageChipPlayer7_2;
	public Image imageChipPlayer7_3;
	public Image imageChipPlayer7_4;
	public Image imageChipPlayer7_5;
	public Image imageChipPlayer8_1;
	public Image imageChipPlayer8_2;
	public Image imageChipPlayer8_3;
	public Image imageChipPlayer8_4;
	public Image imageChipPlayer8_5;
	public Image imageChipPlayer9_1;
	public Image imageChipPlayer9_2;
	public Image imageChipPlayer9_3;
	public Image imageChipPlayer9_4;
	public Image imageChipPlayer9_5;

	public Image imageChipPotPreFlop1;
	public Image imageChipPotPreFlop2;
	public Image imageChipPotPreFlop3;
	public Image imageChipPotPreFlop4;
	public Image imageChipPotPreFlop5;
	public Image imageChipPotPreFlop6;
	public Image imageChipPotPreFlop7;
	public Image imageChipPotPreFlop8;
	public Image imageChipPotFlop1;
	public Image imageChipPotFlop2;
	public Image imageChipPotFlop3;
	public Image imageChipPotTurn1;
	public Image imageChipPotTurn2;
	public Image imageChipPotTurn3;
	public Image imageChipPotRiver1;
	public Image imageChipPotRiver2;
	public Image imageChipPotRiver3;



	public Sprite[] allChips;
	public int index;


	void Awake(){
		InitializeArray ();
	}

	void InitializeArray(){
		ItializeArrayPlayer ();
		ItializeArrayTable ();
	}

	void ItializeArrayTable(){
		listAllChipsTable = new Dictionary <int, List<Image>>();

		List<Image> listTable0 = new List<Image> ();
		AddToList (listTable0, imageChipPotPreFlop1, imageChipPotPreFlop2, imageChipPotPreFlop3, imageChipPotPreFlop4, imageChipPotPreFlop5);
		AddToList3 (listTable0, imageChipPotPreFlop6, imageChipPotPreFlop7, imageChipPotPreFlop8);
		listAllChipsTable.Add (0, listTable0);

		List<Image> listTable1 = new List<Image> ();
		AddToList3 (listTable1, imageChipPotFlop1, imageChipPotFlop2, imageChipPotFlop3);
		listAllChipsTable.Add (1, listTable1);

		List<Image> listTable2 = new List<Image> ();
		AddToList3 (listTable2, imageChipPotTurn1, imageChipPotTurn2, imageChipPotTurn3);
		listAllChipsTable.Add (2, listTable2);

		List<Image> listTable3 = new List<Image> ();
		AddToList3 (listTable3, imageChipPotRiver1, imageChipPotRiver2, imageChipPotRiver3);
		listAllChipsTable.Add (3, listTable3);

		RandomColor (listAllChipsTable);
	}


	void ItializeArrayPlayer(){
		listAllChips = new Dictionary <int, List<Image>>();

		List<Image> listPlayer0 = new List<Image> ();
		AddToList (listPlayer0, imageChipPlayer0_1, imageChipPlayer0_2, imageChipPlayer0_3, imageChipPlayer0_4, imageChipPlayer0_5);
		listAllChips.Add (0, listPlayer0);

		List<Image> listPlayer1 = new List<Image> ();
		AddToList (listPlayer1, imageChipPlayer1_1, imageChipPlayer1_2, imageChipPlayer1_3, imageChipPlayer1_4, imageChipPlayer1_5);
		listAllChips.Add (1, listPlayer1);

		List<Image> listPlayer2 = new List<Image> ();
		AddToList (listPlayer2, imageChipPlayer2_1, imageChipPlayer2_2, imageChipPlayer2_3, imageChipPlayer2_4, imageChipPlayer2_5);
		listAllChips.Add (2, listPlayer2);

		List<Image> listPlayer3 = new List<Image> ();
		AddToList (listPlayer3, imageChipPlayer3_1, imageChipPlayer3_2, imageChipPlayer3_3, imageChipPlayer3_4, imageChipPlayer3_5);
		listAllChips.Add (3, listPlayer3);

		List<Image> listPlayer4 = new List<Image> ();
		AddToList (listPlayer4, imageChipPlayer4_1, imageChipPlayer4_2, imageChipPlayer4_3, imageChipPlayer4_4, imageChipPlayer4_5);
		listAllChips.Add (4, listPlayer4);

		List<Image> listPlayer5 = new List<Image> ();
		AddToList (listPlayer5, imageChipPlayer5_1, imageChipPlayer5_2, imageChipPlayer5_3, imageChipPlayer5_4, imageChipPlayer5_5);
		listAllChips.Add (5, listPlayer5);

		List<Image> listPlayer6 = new List<Image> ();
		AddToList (listPlayer6, imageChipPlayer6_1, imageChipPlayer6_2, imageChipPlayer6_3, imageChipPlayer6_4, imageChipPlayer6_5);
		listAllChips.Add (6, listPlayer6);

		List<Image> listPlayer7 = new List<Image> ();
		AddToList (listPlayer7, imageChipPlayer7_1, imageChipPlayer7_2, imageChipPlayer7_3, imageChipPlayer7_4, imageChipPlayer7_5);
		listAllChips.Add (7, listPlayer7);

		List<Image> listPlayer8 = new List<Image> ();
		AddToList (listPlayer8, imageChipPlayer8_1, imageChipPlayer8_2, imageChipPlayer8_3, imageChipPlayer8_4, imageChipPlayer8_5);
		listAllChips.Add (8, listPlayer8);

		List<Image> listPlayer9 = new List<Image> ();
		AddToList (listPlayer9, imageChipPlayer9_1, imageChipPlayer9_2, imageChipPlayer9_3, imageChipPlayer9_4, imageChipPlayer9_5);
		listAllChips.Add (9, listPlayer9);

		RandomColor (listAllChips);
	}

	void AddToList (List<Image> list, Image img1, Image img2,Image img3,Image img4,Image img5){
		list.Add (img1);
		list.Add (img2);
		list.Add (img3);
		list.Add (img4);
		list.Add (img5);
	}

	void AddToList3 (List<Image> list, Image img1, Image img2, Image img3){
		list.Add (img1);
		list.Add (img2);
		list.Add (img3);
	}

	void RandomColor (Dictionary <int, List<Image>> dictio){
		Random r = new Random();
		for (int i = 0; i < dictio.Count; i++) {
			List<Image> listPlayer = dictio [i];
			foreach (Image img in listPlayer) {
				img.sprite = allChips [Random.Range (0, 10)];
			}
		}
	}
}
