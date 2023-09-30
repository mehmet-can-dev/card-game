using System.Collections;
using System.Collections.Generic;

namespace Core
{
    public class ColorComparer : IComparer<NumericColoredCard>
    {
        public int Compare(NumericColoredCard x, NumericColoredCard y)
        {
            return x.Color.CompareTo(y.Color);
        }
    }
}