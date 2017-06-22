using System;
using System.Collections.Generic;

namespace Poker.BE.Domain.Game
{
    public class PickAWinner
    {

        #region Constants
        public const int POWER = 14;
        public const int FIVEBESTCARDS = 5;
        public const int HANDSTRENGTH = 9;
        public const int FALSERESULT = -1;

        #endregion

        #region Fields
        private ICollection<Player> Players;
        private Card[] FifthHandCard;
        private Dictionary<Player, Card[]> PlayerSevenCards;
        #endregion

        #region Properties
        public List<Player> Winners { get; private set; }
        #endregion

        #region Constructors
        public PickAWinner(ICollection<Player> Players, Card[] FifthHandCard)
        {
            this.Players = Players;
            this.FifthHandCard = FifthHandCard;
            Winners = new List<Player>();
            InitializeDictionnary();
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
        public List<Player> GetWinners()
        {
            bool winnerFound = false;
            int ans = 0;
            int[] winnersArray = new int[PlayerSevenCards.Count];

            for (int i = HANDSTRENGTH; i > 0; i--)
            {
                int k = 0;
                foreach (Player player in Players)
                {
                    ans = CheckPlayerByOrder(player, i);
                    winnersArray[k] = ans;
                    if (ans != FALSERESULT)
                    {
                        winnerFound = true;
                    }
                    k++;
                }
                if (winnerFound)
                {
                    int maxResult = 0;
                    for (int j = 0; j < winnersArray.Length; j++)
                    {
                        if (winnersArray[j] > maxResult)
                        {
                            Winners = new List<Player>
                            {
                                GetPlayerByIndex(j)
                            };
                            maxResult = winnersArray[j];
                        }
                        else if (winnersArray[j] == maxResult)
                        {

                            Winners.Add(GetPlayerByIndex(j));
                        }
                    }
                    return Winners;
                }
            }
            return null;
        }
        #endregion

        #region Private Functions
        //Initialize the PlayerSevenCards field by a sorted array of the player's 7 cards.
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

        private int CheckPlayerByOrder(Player player, int numberRound)
        {
            Card[] player7CardsArray;
            bool isThereACorrectCardArray = PlayerSevenCards.TryGetValue(player, out player7CardsArray);

            if (isThereACorrectCardArray)
            {
                int pairValue, threeOfAKindValue, highCardIndex;
                switch (numberRound)
                {
                    case 1: return IsHighCard(player7CardsArray);
                    case 2: return IsPair(player7CardsArray, out pairValue);
                    case 3: return IsTwoPair(player7CardsArray);
                    case 4: return IsThreeOfAKind(player7CardsArray, out threeOfAKindValue);
                    case 5: return IsStraight(player7CardsArray, out highCardIndex);
                    case 6: return IsFlush(player7CardsArray);
                    case 7: return IsFullHouse(player7CardsArray);
                    case 8: return IsFourOfAKind(player7CardsArray);
                    case 9: return IsStraightFlush(player7CardsArray);
                    default: return 0;
                }
            }
            return FALSERESULT;
        }

        private Player GetPlayerByIndex(int playerIndex)
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

        //Sort all Player's 7 cards by decreasing order
        private void SortSevenCardsByValue(Card[] CardArray)
        {
            for (int i = 0; i < CardArray.Length; i++)
            {
                for (int k = i + 1; k < CardArray.Length; k++)
                {
                    if (CardArray[i].CardValueStrength() < CardArray[k].CardValueStrength())
                    {
                        CardArray = Swap(CardArray, i, k);
                    }
                }
            }
        }

        //Swap two cards in the card array
        private Card[] Swap(Card[] CardArray, int i, int k)
        {
            Card tmp = CardArray[i];
            CardArray[i] = CardArray[k];
            CardArray[k] = tmp;
            return CardArray;
        }

        /// <summary>
        /// For all of the following methods:
        /// We calculate the sum in this way - BestCard * POWER^4 + SecondBestCard * POWER^3...
        /// </summary>
        /// <returns>sum = The hand's strength (calculated by our way)</returns>

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
        private int IsPair(Card[] Player7Cards, out int pairValue)  //the out pairValue parameter is for FullHouse checking
        {
            pairValue = FALSERESULT;
            int exponant = 2;
            int sum = 0;

            for (int i = 0; i < Player7Cards.Length - 1 && (exponant >= 0 || pairValue == FALSERESULT); i++)
            {
                if (pairValue == FALSERESULT)
                {
                    if (Player7Cards[i].CompareTo(Player7Cards[i + 1]) == 0)
                    {
                        pairValue = Player7Cards[i + 1].CardValueStrength();
                        sum += pairValue * (int)Math.Pow(POWER, 3);
                    }
                    else if (exponant >= 0)
                    {
                        sum += Player7Cards[i].CardValueStrength() * (int)Math.Pow(POWER, exponant);
                        exponant--;
                    }
                }
                else
                {
                    //Can't have another number equals to the pair
                    if (pairValue == Player7Cards[i + 1].CardValueStrength())
                    {
                        pairValue = FALSERESULT;
                    }
                    else
                    {
                        sum += Player7Cards[i + 1].CardValueStrength() * (int)Math.Pow(POWER, exponant);
                        exponant--;
                    }
                }
            }
            return pairValue == FALSERESULT ? FALSERESULT : sum;
        }

        // NumberOrderIndex = 3
        private int IsTwoPair(Card[] Player7Cards)
        {
            int firstPairValue = FALSERESULT;
            int secondPairValue = FALSERESULT;
            int highCardValue = FALSERESULT;
            int sum = 0;

            for (int i = 1; i < Player7Cards.Length && (secondPairValue == FALSERESULT || highCardValue == FALSERESULT); i++)
            {
                if (firstPairValue == FALSERESULT)
                {
                    if (Player7Cards[i - 1].CompareTo(Player7Cards[i]) == 0)
                    {
                        firstPairValue = Player7Cards[i].CardValueStrength() * (int)Math.Pow(POWER, 2);
                        sum += firstPairValue;
                        i++;
                    }
                    else if (highCardValue == FALSERESULT)
                    {
                        highCardValue = Player7Cards[i - 1].CardValueStrength();
                        sum += highCardValue;
                    }
                }
                else
                {
                    //Can't have another number equals to the pair
                    if (firstPairValue == Player7Cards[i].CardValueStrength())
                    {
                        firstPairValue = FALSERESULT;
                    }
                    else if (secondPairValue == FALSERESULT)
                    {
                        if (Player7Cards[i - 1].CompareTo(Player7Cards[i]) == 0)
                        {
                            secondPairValue = Player7Cards[i].CardValueStrength() * (int)Math.Pow(POWER, 1);
                            sum += secondPairValue;
                        }
                        else if (highCardValue == FALSERESULT)
                        {
                            highCardValue = Player7Cards[i - 1].CardValueStrength();
                            sum += highCardValue;
                        }
                    }
                    else
                    {
                        //Can't have another number equals to the pair
                        if (firstPairValue == Player7Cards[i].CardValueStrength() || secondPairValue == Player7Cards[i].CardValueStrength())
                        {
                            secondPairValue = FALSERESULT;
                        }

                        highCardValue = Player7Cards[i].CardValueStrength();
                        sum += highCardValue;
                    }
                }

            }
            return (firstPairValue == FALSERESULT || secondPairValue == FALSERESULT) ? FALSERESULT : sum;
        }

        // NumberOrderIndex = 4
        private int IsThreeOfAKind(Card[] Player7Cards, out int threeOfAKindValue)  //the out threeOfAKindValue parameter is for FullHouse checking
        {
            threeOfAKindValue = FALSERESULT;
            int exponant = 1;
            int sum = 0;
            for (int i = 0; i < Player7Cards.Length - 2 && (exponant >= 0 || threeOfAKindValue == FALSERESULT); i++)
            {
                if (threeOfAKindValue == FALSERESULT)
                {
                    if (Player7Cards[i].CompareTo(Player7Cards[i + 1]) == 0
                        && Player7Cards[i].CompareTo(Player7Cards[i + 2]) == 0) // Three of a Kind
                    {
                        threeOfAKindValue = Player7Cards[i].CardValueStrength();
                        sum += threeOfAKindValue * (int)Math.Pow(POWER, 2);
                        i = i + 2;
                    }
                    else if (exponant >= 0)
                    {
                        sum += Player7Cards[i].CardValueStrength() * (int)Math.Pow(POWER, exponant);
                        exponant--;
                    }
                }
                else
                {
                    //Can't have another number equals to the three of a kind
                    if (threeOfAKindValue == Player7Cards[i].CardValueStrength())
                    {
                        threeOfAKindValue = FALSERESULT;
                    }
                    else
                    {
                        sum += Player7Cards[i].CardValueStrength() * (int)Math.Pow(POWER, exponant);
                        exponant--;
                    }
                }
            }
            return threeOfAKindValue == FALSERESULT ? FALSERESULT : sum;
        }

        // NumberOrderIndex = 5
        private int IsStraight(Card[] Player7Cards, out int highCardIndex)  //the out highCardValue parameter is for StraightFlush checking
        {
            highCardIndex = FALSERESULT;
            int straightHighCard = 0;
            int straight = 0;
            int duplicateNumbers = 0;
            int i;
            for (i = 0; i < Player7Cards.Length - 1 && straight < 4; i++)   //we have only 4 comparations
            {
                if (Player7Cards[i].CardValueStrength() == Player7Cards[i + 1].CardValueStrength() + 1)
                {
                    straight++;

                    //if A 2 3 4 5 straight
                    if (straight == 3 && Player7Cards[i + 1].Number == 2 && Player7Cards[0].Number == 1)
                    {
                        //jump each variable once for the Ace we're missing at the end of this straight
                        straight++;
                        i++;
                    }
                }
                else if (Player7Cards[i].CompareTo(Player7Cards[i + 1]) != 0)
                {
                    straight = 0;
                }
                else
                {
                    duplicateNumbers++;
                }
            }

            if (straight >= 4)
            {
                straightHighCard = Player7Cards[i - 4 - duplicateNumbers].CardValueStrength();
                highCardIndex = i - 4 - duplicateNumbers;
                return straightHighCard;
            }
            return FALSERESULT;
        }

        // NumberOrderIndex = 6
        private int IsFlush(Card[] Player7Cards)
        {
            int[] FlushColor = new int[4];
            int suitNumber = -1;

            //count all appearances for each suit
            for (int i = 0; i < Player7Cards.Length; i++)
            {
                FlushColor[Player7Cards[i].GetSuitNumber()] = FlushColor[Player7Cards[i].GetSuitNumber()] + 1;
            }
            //find the flush color
            for (int i = 0; i < FlushColor.Length; i++)
            {
                if (FlushColor[i] >= 5)
                {
                    suitNumber = i;

                    //find the highest flush value
                    for (int j = 0; j < Player7Cards.Length; j++)
                    {
                        if (Player7Cards[j].GetSuitNumber() == suitNumber)
                        {
                            return Player7Cards[j].CardValueStrength();
                        }
                    }
                }
            }
            return FALSERESULT;
        }

        // NumberOrderIndex = 7
        private int IsFullHouse(Card[] Player7Cards)
        {
            int fullHouseValue = FALSERESULT;
            int pairValue, threeOfAKindValue;
            if (IsPair(Player7Cards, out pairValue) > 0 && IsThreeOfAKind(Player7Cards, out threeOfAKindValue) > 0)
            {
                fullHouseValue = pairValue + POWER * threeOfAKindValue;
            }
            return fullHouseValue;
        }

        // NumberOrderIndex = 8
        private int IsFourOfAKind(Card[] Player7Cards)
        {
            int foureOfAKindValue = FALSERESULT;
            int highCardValue = FALSERESULT;
            int sum = 0;
            for (int i = 0; i < Player7Cards.Length - 3 && (foureOfAKindValue == FALSERESULT || highCardValue == FALSERESULT); i++)
            {
                if (Player7Cards[i].CompareTo(Player7Cards[i + 1]) == 0
                    && Player7Cards[i].CompareTo(Player7Cards[i + 2]) == 0
                    && Player7Cards[i].CompareTo(Player7Cards[i + 3]) == 0)
                {
                    foureOfAKindValue = Player7Cards[i].CardValueStrength();
                    sum += foureOfAKindValue * POWER;
                    i = i + 3;
                }
                else if (highCardValue == FALSERESULT)
                {
                    highCardValue = Player7Cards[i].CardValueStrength();
                    sum += highCardValue;
                }

                //Note: no need to check for another numbur equality to the four of a kind
            }
            return foureOfAKindValue == FALSERESULT ? FALSERESULT : sum;
        }

        // NumberOrderIndex = 9
        private int IsStraightFlush(Card[] Player7Cards)
        {
            int straightFlushValue = FALSERESULT;
            int highCardIndex;
            int straightHighCard = IsStraight(Player7Cards, out highCardIndex); //get the straight High Card

            if (straightHighCard > 0)
            {
                int[] flushColor = new int[4];

                // check if the straight we got is also a flush
                for (int i = highCardIndex; i < Player7Cards.Length && Player7Cards[i].Number > straightFlushValue - 5; i++)
                {
                    flushColor[Player7Cards[i].GetSuitNumber()]++;

                    if (flushColor[Player7Cards[i].GetSuitNumber()] == 5)
                    {
                        straightFlushValue = straightHighCard;
                    }
                }
            }
            return straightFlushValue;
        }
        #endregion

    }
}