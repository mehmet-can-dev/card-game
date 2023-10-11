using System;

namespace CardGame.Core.Sort
{
    public class NumericForwardSort : ISort
    {
        public NumericColoredCard[][] Sort(NumericColoredCard[] cards, out NumericColoredCard[] notSortedCards, int minCardCount,
            int maxCardCount, int uniqColorCount, int minNumber, int maxNumber)
        {
            var groupByColorCards = ForwardSortUtilities.SortByNumericWithoutSizeLimitsAndUniqIds(cards);

            for (int i = 0; i < groupByColorCards.Length; i++)
            {
                SortUtilities.SortByCardNo(groupByColorCards[i]);
            }
            
            int packageCount = 0;
            ForwardSortUtilities.CheckOrderedPackages(groupByColorCards, (arg0) => { packageCount++; });

            if (packageCount <= 0)
            {
                notSortedCards = cards;
                return null;
            }

            var packagedCards =
                ForwardSortUtilities.SplitByOrderedNumericWithoutSizeFromSortedByCardNo(packageCount, groupByColorCards,
                    out var notSortableCards);

            int packagedCardsCount = 0;
            int notSortableCardCount = notSortableCards.Length;

            for (int i = 0; i < packagedCards.Length; i++)
            {
                if (packagedCards[i].Length >= minCardCount)
                {
                    packagedCardsCount++;
                }
                else
                {
                    notSortableCardCount += packagedCards[i].Length;
                }
            }

            var notSortableCardsAfterSizeLimited = ForwardSortUtilities.SplitByOrderedNumericWithSizeFromSortedByNumber(minCardCount,
                notSortableCardCount, packagedCardsCount, notSortableCards, packagedCards,
                out var packagedCardsWithSizeLimits);

            notSortedCards = notSortableCardsAfterSizeLimited;
            return packagedCardsWithSizeLimits;
        }
        
    }
}