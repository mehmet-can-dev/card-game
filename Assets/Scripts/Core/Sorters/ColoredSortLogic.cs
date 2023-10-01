
namespace CardGame.Core.Sort
{
    public class ColoredSortLogic
    {
        public static NumericColoredCard[][] SortByColored(NumericColoredCard[] cards, int min, int max,
            out NumericColoredCard[] notSortableCard)
        {
            SortLogic.SortByCardNo(cards);
            var uniqNoCount = GetUniqNoCountFromSortedByCardNo(cards);
            var cardCountPerNo = GetCardCountPerNoFromSortedByNo(cards, uniqNoCount);
            var groupByNoCards = GroupNoCardsFromSortedByNo(cards, uniqNoCount, cardCountPerNo);

            var matchedSplitPickableCount =
                MatchedSplitPickableCount(min, max, groupByNoCards, out var notMatchedCardCount);

            var groupByUniqColorsNoCards = GroupByUniqColorsNoCards(min, max, out notSortableCard, matchedSplitPickableCount, notMatchedCardCount, groupByNoCards);

            return groupByUniqColorsNoCards;
        }

        private static NumericColoredCard[][] GroupByUniqColorsNoCards(int min, int max,
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
                    SortLogic.SortByCardColor(splitCards);
                    var numericSortPackage = NumericSortLogic.SortByNumeric(splitCards);
                    if (numericSortPackage.Length >= min && numericSortPackage.Length <= max)
                    {
                        groupByUniqColorsNoCards[groupByUniqNoCardsIndex] =
                            new NumericColoredCard[numericSortPackage.Length];

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
                        for (int j = 0; j < splitCards.Length; j++)
                        {
                            notSortableCard[notMatchedIndex] = splitCards[j];
                            notMatchedIndex++;
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < splitCards.Length; j++)
                    {
                        notSortableCard[notMatchedIndex] = splitCards[j];
                        notMatchedIndex++;
                    }
                }
            }

            return groupByUniqColorsNoCards;
        }

        private static int MatchedSplitPickableCount(int min, int max, NumericColoredCard[][] groupByNoCards,
            out int notMatchedCardCount)
        {
            int matchedSplitPickableCount = 0;
            notMatchedCardCount = 0;

            for (int i = 0; i < groupByNoCards.GetLength(0); i++)
            {
                var splitCards = groupByNoCards[i];

                if (splitCards.Length >= min)
                {
                    SortLogic.SortByCardColor(splitCards);
                    var lenght = NumericSortLogic.GetUniqColorCountFromSortedByCardColor(splitCards);
                    if (lenght >= min && lenght <= max)
                    {
                        matchedSplitPickableCount++;
                        int[] countSplitLenghtPerPackage =
                            NumericSortLogic.GetCardCountPerColorsFromSortedByColors(splitCards, lenght);

                        for (int j = 0; j < countSplitLenghtPerPackage.Length; j++)
                        {
                            if (countSplitLenghtPerPackage[j] > 1)
                            {
                                notMatchedCardCount += countSplitLenghtPerPackage[j] - 1;
                            }
                        }
                    }
                    else
                    {
                        notMatchedCardCount += splitCards.Length;
                    }
                }
                else
                {
                    notMatchedCardCount += splitCards.Length;
                }
            }

            return matchedSplitPickableCount;
        }

        private static int GetUniqNoCountFromSortedByCardNo(NumericColoredCard[] cards)
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

        private static int[] GetCardCountPerNoFromSortedByNo(NumericColoredCard[] cards, int uniqNoCount)
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

        private static NumericColoredCard[][] GroupNoCardsFromSortedByNo(NumericColoredCard[] cards,
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