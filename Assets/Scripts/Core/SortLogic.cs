using System;
using System.Linq;

namespace Core
{
    public static class SortLogic
    {
        public static void SortByCardColor(Hand h)
        {
            Array.Sort(h.Cards, new ColorComparer());
        }
    }
}