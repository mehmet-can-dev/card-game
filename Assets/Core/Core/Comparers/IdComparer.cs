﻿using System.Collections.Generic;

namespace CardGame.Core.Comparer
{
    public class IdComparer: IComparer<NumericColoredCard>
    {
        public int Compare(NumericColoredCard x, NumericColoredCard y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (ReferenceEquals(null, y)) return 1;
            if (ReferenceEquals(null, x)) return -1;
            return x.Id.CompareTo(y.Id);
        }
    }
}