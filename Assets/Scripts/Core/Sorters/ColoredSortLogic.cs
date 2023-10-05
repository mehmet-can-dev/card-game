
namespace CardGame.Core.Sort
{
    public class ColoredSortLogic
    {
        public static NumericColoredCard[][] SortByColored(NumericColoredCard[] cards, int min, int max,
            out NumericColoredCard[] notSortableCard)
        {
            SortUtilities.SortByCardNo(cards);
            var uniqNoCount = GetUniqNumberCountFromSortedByNumber(cards);
            var cardCountPerNo = GetCardCountPerNumberFromSortedByNumber(cards, uniqNoCount);
            var groupByNoCards = GroupByNumbersFromSortedByNumber(cards, uniqNoCount, cardCountPerNo);

            var matchedSplitPickableCount =
                MatchedSplitPickablePackageCount(min, max, groupByNoCards, out var notMatchedCardCount);

            if (matchedSplitPickableCount <= 0)
            {
                notSortableCard = cards;
                return null;
            }

            var groupByUniqColorsByCardsNo = GroupByUniqColorsByCardsNumberWithLimitedSize(min, max, out notSortableCard, matchedSplitPickableCount, notMatchedCardCount, groupByNoCards);

            return groupByUniqColorsByCardsNo;
        }

        private static NumericColoredCard[][] GroupByUniqColorsByCardsNumberWithLimitedSize(int min, int max,
            out NumericColoredCard[] notSortableCard, int matchedSplitPickableCount, int notMatchedCardCount,
            NumericColoredCard[][] groupByNoCards)
        {
            NumericColoredCard[][] groupByUniqColorsNoCards = new NumericColoredCard[matchedSplitPickableCount][];
            notSortableCard = new NumericColoredCard[notMatchedCardCount];

            for (int i = 0, notMatchedIndex = 0, groupByUniqNoCardsIndex = 0; i < groupByNoCards.GetLength(0); i++)
            {
                var splitCards = groupByNoCards[i];

                if (splitCards.Length >= min)
                {
                    var numericSortPackage = NumericSortLogic.SortByNumericWithoutSizeLimitsAndUniqIds(splitCards);
                    if (numericSortPackage.Length >= min && numericSortPackage.Length <= max)
                    {
                        groupByUniqColorsNoCards[groupByUniqNoCardsIndex] =
                            new NumericColoredCard[numericSortPackage.Length];
                        
                        //  numericSortPackage.SelectMany(subArray => subArray.Take(1)).ToArray();
                        //  numericSortPackage.SelectMany(subArray => subArray.Skip(1)).ToArray();

                        for (int j = 0; j < numericSortPackage.Length; j++)
                        {
                            if (numericSortPackage[j].Length > 1)
                            {
                                for (int k = 1; k < numericSortPackage[j].Length; k++)
                                {
                                    notSortableCard[notMatchedIndex] = numericSortPackage[j][k];
                                    notMatchedIndex++;
                                }
                            }

                            groupByUniqColorsNoCards[groupByUniqNoCardsIndex][j] = numericSortPackage[j][0];
                        }

                        groupByUniqNoCardsIndex++;
                    }
                    else
                    {
                        
                        //Linq Concat
                        for (int j = 0; j < splitCards.Length; j++)
                        {
                            notSortableCard[notMatchedIndex] = splitCards[j];
                            notMatchedIndex++;
                        }
                    }
                }
                else
                {
                    //Linq Concat
                    for (int j = 0; j < splitCards.Length; j++)
                    {
                        notSortableCard[notMatchedIndex] = splitCards[j];
                        notMatchedIndex++;
                    }
                }
            }

            return groupByUniqColorsNoCards;
        }

        private static int MatchedSplitPickablePackageCount(int min, int max, NumericColoredCard[][] groupByNoCards,
            out int notMatchedCardCount)
        {
            int matchedSplitPickableCount = 0;
            notMatchedCardCount = 0;

            for (var i = 0; i < groupByNoCards.Length; i++)
            {
                var splitCards = groupByNoCards[i];
                if (splitCards.Length < min)
                {
                    notMatchedCardCount += splitCards.Length;
                    continue;
                }

                SortUtilities.SortByCardColor(splitCards);
                int uniqueColorCount = NumericSortLogic.GetUniqColorCountFromSortedByColor(splitCards);

                if (min <= uniqueColorCount && uniqueColorCount <= max)
                {
                    matchedSplitPickableCount++;

                    var colorLengths = NumericSortLogic.GetUniqCardCountPerColorsFromSortedByColors(splitCards,
                                 uniqueColorCount);
                    for (var j = 0; j < colorLengths.Length; j++)
                    {
                        var count = colorLengths[j];
                        if (count > 1)
                        {
                            notMatchedCardCount += count - 1;
                        }
                    }
                }
                else
                {
                    notMatchedCardCount += splitCards.Length;
                }
            }

            return matchedSplitPickableCount;
        }

        private static int GetUniqNumberCountFromSortedByNumber(NumericColoredCard[] cards)
        {
            int uniqNoCount = 0;
            int? tempNo = null;

            for (int i = 0; i < cards.Length; i++)
            {
                if (tempNo != null)
                {
                    if (cards[i].No != tempNo)
                    {
                        tempNo = cards[i].No;
                        uniqNoCount++;
                    }
                }
                else
                {
                    tempNo = cards[i].No;
                    uniqNoCount++;
                }
            }

            return uniqNoCount;
        }

        private static int[] GetCardCountPerNumberFromSortedByNumber(NumericColoredCard[] cards, int uniqNoCount)
        {
            int? tempNo = null;

            int[] cardCountPerNo = new int[uniqNoCount];

            for (int i = 0, k = 0; i < cards.Length; i++)
            {
                if (tempNo != null)
                {
                    if (cards[i].No != tempNo)
                    {
                        tempNo = cards[i].No;
                        k++;
                    }
                }
                else
                {
                    tempNo = cards[i].No;
                }

                cardCountPerNo[k]++;
            }

            return cardCountPerNo;
        }

        private static NumericColoredCard[][] GroupByNumbersFromSortedByNumber(NumericColoredCard[] cards,
            int uniqNoCount,
            int[] cardCountPerNo)
        {
            NumericColoredCard[][] groupByNoCards = new NumericColoredCard[uniqNoCount][];

            int currentNoIndex = 0;

            for (int i = 0, groupIndex = 0; i < cards.Length; i++)
            {
                if (groupIndex >= cardCountPerNo[currentNoIndex])
                {
                    groupIndex = 0;
                    currentNoIndex++;
                }

                if (groupByNoCards[currentNoIndex] == null)
                {
                    groupByNoCards[currentNoIndex] =
                        new NumericColoredCard[cardCountPerNo[currentNoIndex]];
                }

                groupByNoCards[currentNoIndex][groupIndex] = cards[i];
                groupIndex++;
            }

            return groupByNoCards;
        }
    }
}