﻿using System;
using System.Collections.Generic;

namespace Poker.BE.Domain.Game
{
    public class PickAWinner
    {

        #region Constants
        public const int POWER = 14;
        public const int FIVEBESTCARDS = 5;
        public const int NUMBERORDER = 10;
        public const int FALSERESULT = -1;

        #endregion

        #region Fields
        private ICollection<Player> Players;
        private Card[] FifthHandCard;
        private Dictionary<Player, Card[]> PlayerSevenCards;
        #endregion

        #region Properties
        public ICollection<Player> Winners { get; private set; }
        #endregion

        #region Constructors
        public PickAWinner(ICollection<Player> Players, Card[] FifthHandCard)
        {
            this.Players = Players;
            this.FifthHandCard = FifthHandCard;
            Winners = new List<Player>();
            InitializeDictionnary();
            GetWinner();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Find the player with the highest ranked hand.
        /// Start with checking for players with straight flush.
        /// If not found, check for four of a kind.
        /// If not found, etc...
        /// Then check for several winners.
        /// </summary>
        /// <returns>The winning player\s</returns>
        private ICollection<Player> GetWinner()
        {
            bool winnerFound = false;
            int numberOfPlayers = PlayerSevenCards.Count, k = 0, ans = 0;
            int[] arrayWinners = new int[numberOfPlayers];

            for (int i = NUMBERORDER; i > 0 && !winnerFound; i--)
            {
                foreach (Player player in Players)
                {
                    ans = CheckPlayerByOrder(player, i);
                    arrayWinners[k] = ans;
                    k++;
                    if (ans != FALSERESULT) winnerFound = true;
                }
                if (winnerFound)
                {
                    for (int j = 0; j < arrayWinners.Length; j++)
                    {
                        int maxResult = 0;
                        if (arrayWinners[i] > maxResult)
                        {
                            Winners = new List<Player>
                            {
                                GetPlayerByDictionnaryAndIndex(i)
                            };

                            maxResult = arrayWinners[i];
                        }
                        else if (arrayWinners[i] == maxResult)
                        {
                            Winners.Add(GetPlayerByDictionnaryAndIndex(i));
                        }
                    }
                    return Winners;
                }
                k = 0;
            }
            return null;
        }


        private int CheckPlayerByOrder(Player player, int numberRound)
        {
            bool isThereACorrectCardArray = PlayerSevenCards.TryGetValue(player, out Card[] playerCardArray);

            if (isThereACorrectCardArray)
            {
                switch (numberRound)
                {
                    case 1: return IsHighCard(playerCardArray);
                    case 2: return IsPair(playerCardArray);
                    case 3: return IsTwoPair(playerCardArray);
                    case 4: return IsThreeOfAKind(playerCardArray);
                    case 5: return IsStraight(playerCardArray);
                    case 6: return IsFlush(playerCardArray);
                    case 7: return IsFullHouse(playerCardArray);
                    case 8: return IsFourOfAKind(playerCardArray);
                    case 9: return IsStraightFlush(playerCardArray);
                    case 10: return IsRoyalFlush(playerCardArray);
                    default: return 0;
                }
            }
            return FALSERESULT;
        }

        private Player GetPlayerByDictionnaryAndIndex(int playerIndex)
        {
            Player winner = null;
            int currentIndex = 0;
            foreach (Player player in Players)
            {
                if (currentIndex == playerIndex)
                {
                    winner = player;
                }
                currentIndex = currentIndex + 1;
            }
            return winner;
        }

        private void InitializeDictionnary()
        {
            PlayerSevenCards = new Dictionary<Player, Card[]>();
            foreach (Player player in Players)
            {
                Card[] PlayerSevenCardArray = new Card[7];
                for (int i = 0; i < 2; i++)
                {
                    PlayerSevenCardArray[i] = player.PrivateCards[i];
                }
                for (int k = 0; k < FIVEBESTCARDS; k++)
                {
                    PlayerSevenCardArray[k + 2] = FifthHandCard[k];
                }
                SortSevenCardsByValue(PlayerSevenCardArray);
                PlayerSevenCards.Add(player, PlayerSevenCardArray);
            }
        }

        private void SortSevenCardsByValue(Card[] CardArray)
        {
            for (int i = 0; i < CardArray.Length; i++)
            {
                for (int k = i + 1; k < FIVEBESTCARDS + 1; k++)
                {
                    if (CardArray[i].Number > CardArray[k].Number)
                    {
                        CardArray = Swap(CardArray, i, k);
                    }
                }
            }
        }

        private Card[] Swap(Card[] CardArray, int i, int k)
        {
            Card tmp = CardArray[i];
            CardArray[i] = CardArray[k];
            CardArray[k] = tmp;
            return CardArray;
        }


        //We calculate the sum in this way - BestCard * POWER^ 4 + SecondBestCard * POWER^3..
        // NumberOrderIndex = 1
        private int IsHighCard(Card[] Player7Cards)
        {
            int exponant = FIVEBESTCARDS - 1;
            double sum = 0;
            int j = 6;
            for (int i = 0; i < FIVEBESTCARDS; i++)
            { // in case we have an or more AS
                sum += Math.Pow(POWER, exponant) * Player7Cards[i].CardValueStrength();
                exponant = exponant - 1;
                i = i + 1;
            }
            while (exponant >= 0)
            {
                sum = Math.Pow(POWER, exponant) * Player7Cards[j].Number;
                exponant = exponant - 1;
                j = j - 1;
            }
            return (int)sum;
        }

        // NumberOrderIndex = 2
        private int IsPair(Card[] Player7Cards)
        {
            int PairValue = FALSERESULT;
            for (int i = 0; i < Player7Cards.Length - 1 && PairValue == FALSERESULT; i++)
            {
                if (Player7Cards[i].CompareTo(Player7Cards[i + 1]) == 0)
                {
                    PairValue = Player7Cards[i].CardValueStrength();
                }
            }
            return PairValue;
        }

        // NumberOrderIndex = 3
        private int IsTwoPair(Card[] Player7Cards)
        {
            int TwoPairValue = FALSERESULT;
            for (int i = 0; i < Player7Cards.Length - 1 && TwoPairValue < (POWER + 1); i++)
            {
                if (Player7Cards[i].CompareTo(Player7Cards[i + 1]) == 0)
                {
                    if (TwoPairValue == FALSERESULT)
                    { // the value of the first pair x * POWER^0
                        TwoPairValue = Player7Cards[i].CardValueStrength();
                    }
                    else
                    { // the value of the second pair - TwoPairValue + (x * POWER^1)
                        TwoPairValue = TwoPairValue + (POWER * Player7Cards[i].CardValueStrength());
                    }
                }
            }
            if (TwoPairValue < (POWER + 1)) TwoPairValue = FALSERESULT;
            return TwoPairValue;

        }

        // NumberOrderIndex = 4
        private int IsThreeOfAKind(Card[] Player7Cards)
        {
            int ThreeOfAKindValue = FALSERESULT;
            for (int i = 0; i < Player7Cards.Length - 2 && ThreeOfAKindValue == FALSERESULT; i++)
            {
                if (Player7Cards[i].CompareTo(Player7Cards[i + 1]) == 0
                    && Player7Cards[i + 1].CompareTo(Player7Cards[i + 2]) == 0) // Three of a Kind
                {
                    ThreeOfAKindValue = Player7Cards[i].CardValueStrength();
                }
            }
            return ThreeOfAKindValue;
        }

        // NumberOrderIndex = 5
        private int IsStraight(Card[] Player7Cards)
        {
            if (IsAsStraight(Player7Cards) > 0) return IsAsStraight(Player7Cards);
            else return IsNormalStraight(Player7Cards);
        }

        private int IsNormalStraight(Card[] Player7Cards)
        {
            int i, MinimumStraightValueIndex = 0;
            int Straight = 1;
            for (i = 0; i < Player7Cards.Length - 1 && Straight < 5; i++)
            {
                if (Player7Cards[i].CompareTo(Player7Cards[i + 1]) == FALSERESULT)
                {
                    Straight = Straight + 1;
                }
                else
                {
                    MinimumStraightValueIndex = i;
                }
            }
            if (Straight < 5) return FALSERESULT;
            else return MinimumStraightValueIndex;
        }

        private int IsAsStraight(Card[] Player7Cards)
        {
            int[] asStraightIndex = new int[FIVEBESTCARDS];
            for (int i = 0; i < Player7Cards.Length; i++)
            {
                int CardValue = Player7Cards[i].CardValueStrength() - 10;
                if (CardValue >= 0)
                {
                    asStraightIndex[CardValue] = asStraightIndex[CardValue] + 1;
                }
            }
            bool ans = true;
            for (int k = 0; k < asStraightIndex.Length && ans; k++)
            {
                if (asStraightIndex[k] == 0) ans = false;
            }
            if (ans) return 10;
            else return FALSERESULT;
        }

        // NumberOrderIndex = 6
        private int IsFlush(Card[] Player7Cards)
        {
            int i;
            int[] FlushColor = new int[4];
            for (i = 0; i < Player7Cards.Length; i++)
            {
                FlushColor[Player7Cards[i].GetSuitNumber()] = FlushColor[Player7Cards[i].GetSuitNumber()] + 1;
            }
            for (i = 0; i < FlushColor.Length; i++)
            {
                if (FlushColor[i] >= 5)
                {
                    return 1;
                }
            }
            return FALSERESULT;
        }

        // NumberOrderIndex = 7
        private int IsFullHouse(Card[] Player7Cards)
        {
            int sum = FALSERESULT;
            if (IsTwoPair(Player7Cards) > 0 && IsThreeOfAKind(Player7Cards) > 0)
            {
                int PairValue = IsTwoPair(Player7Cards);
                int ThreeOfAKindValue = IsThreeOfAKind(Player7Cards);
                sum = PairValue + POWER * ThreeOfAKindValue;
            }
            return sum;
        }

        // NumberOrderIndex = 8
        private int IsFourOfAKind(Card[] Player7Cards)
        {
            int i;
            int Count = 1;
            for (i = 0; i < Player7Cards.Length - 1 && Count < FIVEBESTCARDS - 1; i++)
            {
                if (Player7Cards[i].CompareTo(Player7Cards[i + 1]) == 0)
                {
                    Count = Count + 1;
                }
                else
                {
                    Count = 1;
                }

            }
            if (Count < 4) return FALSERESULT;
            else return Player7Cards[i - 1].CardValueStrength();
        }

        // NumberOrderIndex = 9
        private int IsStraightFlush(Card[] Player7Cards)
        {
            if (IsStraight(Player7Cards) > 0)
            {
                int smallStraightCardValue = IsStraight(Player7Cards);
                int startStraightIndex = FALSERESULT;

                // we are looking for the last minimum straight value occurence in our array
                for (int i = 2; i > 0 && startStraightIndex == FALSERESULT; i--)
                {
                    if (Player7Cards[i].Number == smallStraightCardValue)
                    {
                        startStraightIndex = i;
                    }
                }

                // we check if the straight have the same suit
                bool ans = true;
                for (int k = 0; k < 4 && ans; k++)
                {
                    if (Player7Cards[startStraightIndex + k].GetSuitNumber() != Player7Cards[startStraightIndex + k].GetSuitNumber() + 1)
                    {
                        ans = false;
                    }
                }
                if (ans) return smallStraightCardValue;
                else return FALSERESULT;
            }
            return FALSERESULT;
        }

        // NumberOrderIndex = 10
        private int IsRoyalFlush(Card[] Player7Cards)
        {
            if (IsStraightFlush(Player7Cards) > 0 && IsAsStraight(Player7Cards) > 0)
            {
                return 1;
            }
            return FALSERESULT;
        }
        #endregion

    }
}