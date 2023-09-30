using System;
using System.Collections;
using System.Globalization;
using System.Linq;

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
    }
}