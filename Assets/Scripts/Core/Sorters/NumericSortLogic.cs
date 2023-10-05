using System;
using UnityEngine;

namespace CardGame.Core.Sort
{
    public static class NumericSortLogic
    {
        public static NumericColoredCard[][] SortByNumeric(NumericColoredCard[] cards,
            out NumericColoredCard[] notSortedCards, int min)
        {
            var groupByColorCards = SortByNumericWithoutSizeLimitsAndUniqIds(cards);

            for (int i = 0; i < groupByColorCards.Length; i++)
            {
                SortUtilities.SortByCardNo(groupByColorCards[i]);
            }
            
            int packageCount = 0;
            CheckOrderedPackages(groupByColorCards, (arg0) => { packageCount++; });

            if (packageCount <= 0)
            {
                notSortedCards = cards;
                return null;
            }

            var packagedCards =
                SplitByOrderedNumericWithoutSizeFromSortedByCardNo(packageCount, groupByColorCards,
                    out var notSortableCards);

            int packagedCardsCount = 0;
            int notSortableCardCount = notSortableCards.Length;

            for (int i = 0; i < packagedCards.Length; i++)
            {
                if (packagedCards[i].Length >= min)
                {
                    packagedCardsCount++;
                }
                else
                {
                    notSortableCardCount += packagedCards[i].Length;
                }
            }

            var notSortableCardsAfterSizeLimited = SplitByOrderedNumericWithSizeFromSortedByNumber(min,
                notSortableCardCount, packagedCardsCount, notSortableCards, packagedCards,
                out var packagedCardsWithSizeLimits);

            notSortedCards = notSortableCardsAfterSizeLimited;
            return packagedCardsWithSizeLimits;
        }

        private static NumericColoredCard[] SplitByOrderedNumericWithSizeFromSortedByNumber(int min,
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

        private static NumericColoredCard[][] SplitByOrderedNumericWithoutSizeFromSortedByCardNo(int packageCount,
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

        private static void CheckOrderedPackages(NumericColoredCard[][] groupByColorCards,
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

        public static NumericColoredCard[][] SortByNumericWithoutSizeLimitsAndUniqIds(NumericColoredCard[] cards)
        {
            SortUtilities.SortByCardColor(cards);

            var uniqColorCount = GetUniqColorCountFromSortedByColor(cards);

            var cardCountsPerColors = GetUniqCardCountPerColorsFromSortedByColors(cards, uniqColorCount);

            var groupByColorCards = GroupByColorFromSortedByColors(cards, uniqColorCount, cardCountsPerColors);

            return groupByColorCards;
        }

        private static NumericColoredCard[][] GroupByColorFromSortedByColors(NumericColoredCard[] cards,
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
    }
}