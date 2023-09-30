using System;
using UnityEngine;

namespace Core
{
    public static class SortLogic
    {
        public static void SortByCardColor(NumericColoredCard[] cards)
        {
            Array.Sort(cards, new ColorComparer());
        }

        public static void SortByCardId(NumericColoredCard[] cards)
        {
            Array.Sort(cards, new IntComparer());
        }

        public static NumericColoredCard[][] SortByNumeric(NumericColoredCard[] cards)
        {
            SortByCardColor(cards);

            int uniqColorCount = 0;
            Color tempColor = null;

            for (int i = 0; i < cards.Length; i++)
            {
                if (tempColor != null)
                {
                    if (cards[i].Color != tempColor)
                    {
                        tempColor = cards[i].Color;
                        uniqColorCount++;
                    }
                }
                else
                {
                    tempColor = cards[i].Color;
                    uniqColorCount++;
                }
            }

            tempColor = null;

            int[] cardCountPerColors = new int[uniqColorCount];

            for (int i = 0, k = 0; i < cards.Length; i++)
            {
                if (tempColor != null)
                {
                    if (cards[i].Color != tempColor)
                    {
                        tempColor = cards[i].Color;
                        k++;
                    }
                }
                else
                {
                    tempColor = cards[i].Color;
                }

                cardCountPerColors[k]++;
            }

            NumericColoredCard[][] groupByColorCards = new NumericColoredCard[uniqColorCount][];

            int currentColorIndex = 0;

            for (int i = 0, groupIndex = 0; i < cards.Length; i++)
            {
                if (groupIndex >= cardCountPerColors[currentColorIndex])
                {
                    groupIndex = 0;
                    currentColorIndex++;
                }

                if (groupByColorCards[currentColorIndex] == null)
                {
                    groupByColorCards[currentColorIndex] =
                        new NumericColoredCard[cardCountPerColors[currentColorIndex]];
                }

                groupByColorCards[currentColorIndex][groupIndex] = cards[i];
                groupIndex++;
            }

            return groupByColorCards;
        }
    }
}