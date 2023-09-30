﻿using UnityEngine;

namespace Core
{
    public class ColoredSortLogic
    {
        public static NumericColoredCard[][] SortByColored(NumericColoredCard[] cards, int min, int max,
            out NumericColoredCard[] notSortableCard)
        {
            SortLogic.SortByCardNo(cards);
            var uniqNoCount = GetUniqNoCountFromSortedByCardNo(cards);
            var cardCountPerNo = GetCardCountPerNoFromSortedByNo(cards, uniqNoCount);
            var groupByNoCards = GroupNoCardsFromSortedByNo(cards, uniqNoCount, cardCountPerNo);

            int matchedSplitPickableCount = 0;
            int notMatchedCardCount = 0;

            for (int i = 0; i < groupByNoCards.GetLength(0); i++)
            {
                var splitCards = groupByNoCards[i];

                if (splitCards.Length >= min)
                {
                    SortLogic.SortByCardColor(splitCards);
                    var lenght = NumericSortLogic.GetUniqColorCountFromSortedByCardColor(splitCards);
                    if (lenght >= min && lenght <= max)
                    {
                        matchedSplitPickableCount++;
                        int[] countSplitLenghtPerPackage =
                            NumericSortLogic.GetCardCountPerColorsFromSortedByColors(splitCards, lenght);

                        for (int j = 0; j < countSplitLenghtPerPackage.Length; j++)
                        {
                            if (countSplitLenghtPerPackage[j] > 1)
                            {
                                notMatchedCardCount += countSplitLenghtPerPackage[j] - 1;
                            }
                        }
                    }
                    else
                    {
                        notMatchedCardCount += splitCards.Length;
                    }
                }
                else
                {
                    notMatchedCardCount += splitCards.Length;
                }
            }

            NumericColoredCard[][] groupByUniqColorsNoCards = new NumericColoredCard[matchedSplitPickableCount][];
            notSortableCard = new NumericColoredCard[notMatchedCardCount];

            return groupByNoCards;
        }

        private static int GetUniqNoCountFromSortedByCardNo(NumericColoredCard[] cards)
        {
            int uniqNoCount = 0;
            int? tempNo = null;

            for (int i = 0; i < cards.Length; i++)
            {
                if (tempNo != null)
                {
                    if (cards[i].No != tempNo)
                    {
                        tempNo = cards[i].No;
                        uniqNoCount++;
                    }
                }
                else
                {
                    tempNo = cards[i].No;
                    uniqNoCount++;
                }
            }

            return uniqNoCount;
        }

        private static int[] GetCardCountPerNoFromSortedByNo(NumericColoredCard[] cards, int uniqNoCount)
        {
            int? tempNo = null;

            int[] cardCountPerNo = new int[uniqNoCount];

            for (int i = 0, k = 0; i < cards.Length; i++)
            {
                if (tempNo != null)
                {
                    if (cards[i].No != tempNo)
                    {
                        tempNo = cards[i].No;
                        k++;
                    }
                }
                else
                {
                    tempNo = cards[i].No;
                }

                cardCountPerNo[k]++;
            }

            return cardCountPerNo;
        }

        private static NumericColoredCard[][] GroupNoCardsFromSortedByNo(NumericColoredCard[] cards,
            int uniqNoCount,
            int[] cardCountPerNo)
        {
            NumericColoredCard[][] groupByNoCards = new NumericColoredCard[uniqNoCount][];

            int currentNoIndex = 0;

            for (int i = 0, groupIndex = 0; i < cards.Length; i++)
            {
                if (groupIndex >= cardCountPerNo[currentNoIndex])
                {
                    groupIndex = 0;
                    currentNoIndex++;
                }

                if (groupByNoCards[currentNoIndex] == null)
                {
                    groupByNoCards[currentNoIndex] =
                        new NumericColoredCard[cardCountPerNo[currentNoIndex]];
                }

                groupByNoCards[currentNoIndex][groupIndex] = cards[i];
                groupIndex++;
            }

            return groupByNoCards;
        }
    }
}