using System;

namespace CardGame.Core.Sort.Forward
{
    public class ColoredForwardSort : ISort
    {
        public NumericColoredCard[][] Sort(NumericColoredCard[] cards, out NumericColoredCard[] notSortedCards,
            int minCardCount,
            int maxCardCount, int uniqColorCount, int minNumber, int maxNumber)
        {
            SortUtilities.SortByCardNo(cards);
            var uniqNoCount = ForwardSortUtilities.GetUniqNumberCountFromSortedByNumber(cards);
            var cardCountPerNo = ForwardSortUtilities.GetCardCountPerNumberFromSortedByNumber(cards, uniqNoCount);
            var groupByNoCards =
                ForwardSortUtilities.GroupByNumbersFromSortedByNumber(cards, uniqNoCount, cardCountPerNo);

            var matchedSplitPickableCount =
                ForwardSortUtilities.MatchedSplitPickablePackageCount(minCardCount, maxCardCount, groupByNoCards,
                    out var notMatchedCardCount);

            if (matchedSplitPickableCount <= 0)
            {
                notSortedCards = cards;
                return null;
            }

            var groupByUniqColorsByCardsNo = ForwardSortUtilities.GroupByUniqColorsByCardsNumberWithLimitedSize(
                minCardCount, maxCardCount, out notSortedCards, matchedSplitPickableCount, notMatchedCardCount,
                groupByNoCards);

            return groupByUniqColorsByCardsNo;
        }
    }
}