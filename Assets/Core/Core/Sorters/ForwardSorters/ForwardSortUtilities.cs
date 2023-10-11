using System;

namespace CardGame.Core.Sort.Forward
{
    public class ForwardSortUtilities
    {
        public static NumericColoredCard[][] GroupByUniqColorsByCardsNumberWithLimitedSize(int min, int max,
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
                    var numericSortPackage = SortByNumericWithoutSizeLimitsAndUniqIds(splitCards);
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

        public static int MatchedSplitPickablePackageCount(int min, int max, NumericColoredCard[][] groupByNoCards,
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
                int uniqueColorCount = GetUniqColorCountFromSortedByColor(splitCards);

                if (min <= uniqueColorCount && uniqueColorCount <= max)
                {
                    matchedSplitPickableCount++;

                    var colorLengths = GetUniqCardCountPerColorsFromSortedByColors(splitCards,
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

        public static int GetUniqNumberCountFromSortedByNumber(NumericColoredCard[] cards)
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

        public static int[] GetCardCountPerNumberFromSortedByNumber(NumericColoredCard[] cards, int uniqNoCount)
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

        public static NumericColoredCard[][] GroupByNumbersFromSortedByNumber(NumericColoredCard[] cards,
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

        public static NumericColoredCard[] SplitByOrderedNumericWithSizeFromSortedByNumber(int min,
            int notSortableCardCount,
            int packagedCardsCount, NumericColoredCard[] notSortableCards, NumericColoredCard[][] packagedCards,
            out NumericColoredCard[][] packagedCardsWithSizeLimits)
        {
            var notSortableCardsAfterSizeLimited = new NumericColoredCard[notSortableCardCount];
            packagedCardsWithSizeLimits = new NumericColoredCard[packagedCardsCount][];

            int lastFilledIndex = 0;
            for (int i = 0; i < notSortableCards.Length; i++)
            {
                notSortableCardsAfterSizeLimited[i] = notSortableCards[i];
                lastFilledIndex++;
            }

            int tempIndexPackageCards = 0;

            for (int i = 0; i < packagedCards.Length; i++)
            {
                if (packagedCards[i].Length >= min)
                {
                    packagedCardsWithSizeLimits[tempIndexPackageCards] =
                        new NumericColoredCard[packagedCards[i].Length];

                    for (int j = 0; j < packagedCards[i].Length; j++)
                    {
                        packagedCardsWithSizeLimits[tempIndexPackageCards][j] = packagedCards[i][j];
                    }

                    tempIndexPackageCards++;
                }
                else
                {
                    for (int j = 0; j < packagedCards[i].Length; j++)
                    {
                        notSortableCardsAfterSizeLimited[lastFilledIndex] = packagedCards[i][j];
                        lastFilledIndex++;
                    }
                }
            }

            return notSortableCardsAfterSizeLimited;
        }

        public static NumericColoredCard[][] SplitByOrderedNumericWithoutSizeFromSortedByCardNo(int packageCount,
            NumericColoredCard[][] groupByColorCards, out NumericColoredCard[] notSortableCards)
        {
            int[] packageCardsCounts = new int[packageCount];
            int notSortableCardCount = 0;
            int tempIndex = 0;

            CheckOrderedPackages(groupByColorCards, (arg0) =>
            {
                packageCardsCounts[tempIndex] = arg0;
                tempIndex++;
            }, null, card => { notSortableCardCount++; });

            NumericColoredCard[][] packagedCards = new NumericColoredCard[packageCount][];
            notSortableCards = new NumericColoredCard[notSortableCardCount];

            packagedCards[0] = new NumericColoredCard[packageCardsCounts[0]];

            int tempIndexPackagedDimensionZero = 0;
            int tempIndexPackagedDimensionOne = 0;
            int tempIndexNotSortable = 0;

            var cards = notSortableCards;
            CheckOrderedPackages(groupByColorCards, (packageCount) =>
            {
                tempIndexPackagedDimensionZero++;

                if (tempIndexPackagedDimensionZero < packagedCards.Length)
                    packagedCards[tempIndexPackagedDimensionZero] =
                        new NumericColoredCard[packageCardsCounts[tempIndexPackagedDimensionZero]];
                tempIndexPackagedDimensionOne = 0;
            }, card =>
            {
                packagedCards[tempIndexPackagedDimensionZero][tempIndexPackagedDimensionOne] = card;
                tempIndexPackagedDimensionOne++;
            }, card =>
            {
                cards[tempIndexNotSortable] = card;
                tempIndexNotSortable++;
            });
            return packagedCards;
        }


        public static NumericColoredCard[][] SortByNumericWithoutSizeLimitsAndUniqIds(NumericColoredCard[] cards)
        {
            SortUtilities.SortByCardColor(cards);

            var uniqColorCount = GetUniqColorCountFromSortedByColor(cards);

            var cardCountsPerColors = GetUniqCardCountPerColorsFromSortedByColors(cards, uniqColorCount);

            var groupByColorCards = GroupByColorFromSortedByColors(cards, uniqColorCount, cardCountsPerColors);

            return groupByColorCards;
        }

        public static NumericColoredCard[][] GroupByColorFromSortedByColors(NumericColoredCard[] cards,
            int uniqColorCount,
            int[] cardCountPerColors)
        {
            NumericColoredCard[][] groupByColorCards = new NumericColoredCard[uniqColorCount][];

            int currentColorIndex = 0;

            for (int i = 0, groupIndex = 0; i < cards.Length; i++)
            {
                if (groupIndex >= cardCountPerColors[currentColorIndex])
                {
                    groupIndex = 0;
                    currentColorIndex++;
                }

                groupByColorCards[currentColorIndex] ??= new NumericColoredCard[cardCountPerColors[currentColorIndex]];

                groupByColorCards[currentColorIndex][groupIndex] = cards[i];
                groupIndex++;
            }

            return groupByColorCards;
        }

        public static int[] GetUniqCardCountPerColorsFromSortedByColors(NumericColoredCard[] cards, int uniqColorCount)
        {
            Color tempColor = null;

            int[] cardCountPerColors = new int[uniqColorCount];

            for (int i = 0, k = 0; i < cards.Length; i++)
            {
                if (tempColor != null)
                {
                    if (cards[i].Color != tempColor)
                    {
                        tempColor = cards[i].Color;
                        k++;
                    }
                }
                else
                {
                    tempColor = cards[i].Color;
                }

                cardCountPerColors[k]++;
            }

            return cardCountPerColors;
        }

        public static int GetUniqColorCountFromSortedByColor(NumericColoredCard[] cards)
        {
            int uniqColorCount = 0;
            Color tempColor = null;

            for (int i = 0; i < cards.Length; i++)
            {
                if (tempColor != null)
                {
                    if (cards[i].Color != tempColor)
                    {
                        tempColor = cards[i].Color;
                        uniqColorCount++;
                    }
                }
                else
                {
                    tempColor = cards[i].Color;
                    uniqColorCount++;
                }
            }

            return uniqColorCount;
        }

        public static void CheckOrderedPackages(NumericColoredCard[][] groupByColorCards,
            Action<int> onPackageFounded, Action<NumericColoredCard> onPackageCardAdded = null,
            Action<NumericColoredCard> onNotMatchedCardFind = null)
        {
            for (int i = 0; i < groupByColorCards.Length; i++)
            {
                int sortedCardCount = 0;
                int selectedNumber = groupByColorCards[i][0].No;
                bool isLastLookingOrdered = false;

                void CheckAndProcessSortedCount()
                {
                    if (sortedCardCount != 0)
                    {
                        onPackageFounded?.Invoke(sortedCardCount);
                    }

                    isLastLookingOrdered = false;
                    sortedCardCount = 0;
                }

                void IncreaseSortedCardCount(NumericColoredCard card)
                {
                    sortedCardCount++;
                    onPackageCardAdded?.Invoke(card);
                }

                for (int j = 1; j < groupByColorCards[i].Length; j++)
                {
                    var cardNumber = groupByColorCards[i][j].No;

                    if (selectedNumber + 1 == cardNumber)
                    {
                        IncreaseSortedCardCount(groupByColorCards[i][j - 1]);
                        isLastLookingOrdered = true;
                    }
                    else if (isLastLookingOrdered)
                    {
                        IncreaseSortedCardCount(groupByColorCards[i][j - 1]);
                        CheckAndProcessSortedCount();
                    }
                    else
                    {
                        CheckAndProcessSortedCount();
                        onNotMatchedCardFind?.Invoke(groupByColorCards[i][j - 1]);
                    }

                    selectedNumber = cardNumber;
                }

                if (isLastLookingOrdered)
                {
                    IncreaseSortedCardCount(groupByColorCards[i][^1]);
                    CheckAndProcessSortedCount();
                }
                else
                {
                    onNotMatchedCardFind?.Invoke(groupByColorCards[i][^1]);
                }
            }
        }
    }
}