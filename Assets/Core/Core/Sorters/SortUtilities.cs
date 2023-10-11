using System;
using CardGame.Core.Comparer;

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
        
    }
}