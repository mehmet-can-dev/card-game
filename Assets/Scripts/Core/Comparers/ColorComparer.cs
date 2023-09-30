using System.Collections.Generic;

namespace Core
{
    public class ColorComparer : IComparer<NumericColoredCard>
    {
        public int Compare(NumericColoredCard x, NumericColoredCard y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (ReferenceEquals(null, y)) return 1;
            if (ReferenceEquals(null, x)) return -1;
            return Comparer<Color>.Default.Compare(x.Color, y.Color);
        }
    }
}