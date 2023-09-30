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
            Array.Sort(cards, new IdComparer());
        }

        public static void SortByCardNo(NumericColoredCard[] cards)
        {
            Array.Sort(cards, new NoComparer());
        }
    }
}