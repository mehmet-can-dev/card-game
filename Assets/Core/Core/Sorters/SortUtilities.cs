using System;
using System.Collections.Generic;
using System.Linq;
using CardGame.Core.Comparer;
using CardGame.Core.Sort.Recursive;
using UnityEngine;

namespace CardGame.Core.Sort
{
    public static class SortUtilities
    {
        public static void SortByCardColor(NumericColoredCard[] cards)
        {
            Array.Sort(cards, new ColorComparer());
        }

        public static void SortByCardId(NumericColoredCard[] cards)
        {
            Array.Sort(cards, new IdComparer());
        }

        public static void SortByCardNo(NumericColoredCard[] cards)
        {
            Array.Sort(cards, new NumberComparer());
        }

        public static bool IsOrderedColored(List<NumericColoredCard> cards)
        {
            List<Color> usedColorList = new List<Color>();

            var firstCard = cards.First();
            int tempNumber = firstCard.No;
            int startIndex = 1;
            if (firstCard is JokerCard)
            {
                var card = cards.First(p => p is not JokerCard);
                tempNumber = card.No;
                startIndex = cards.FindIndex(p => Equals(p, card)) + 1;
                usedColorList.Add(card.Color);
            }
            else
            {
                usedColorList.Add(firstCard.Color);
            }


            for (int i = startIndex; i < cards.Count; i++)
            {
                if (cards[i].No == tempNumber && !usedColorList.Contains(cards[i].Color))
                {
                    usedColorList.Add(cards[i].Color);
                }
                else if (cards[i] is JokerCard)
                {
                   
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsOrderedNumeric(List<NumericColoredCard> cards)
        {
            var firstCard = cards.First();
            int tempNumber = firstCard.No;
            int startIndex = 1;
            Color tempColor = firstCard.Color;
            if (firstCard is JokerCard)
            {
                var card = cards.First(p => p is not JokerCard);
                tempNumber = card.No;
                startIndex = cards.FindIndex(p => Equals(p, card)) + 1;
                tempColor = card.Color;
            }

            for (int i = startIndex; i < cards.Count; i++)
            {
                if (cards[i].No == tempNumber + 1 && tempColor == cards[i].Color)
                {
                    tempNumber++;
                }
                else if (cards[i] is JokerCard)
                {
                    tempNumber++;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
    }
}