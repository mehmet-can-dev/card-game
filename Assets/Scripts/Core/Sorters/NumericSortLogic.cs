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
                SortLogic.SortByCardNo(groupByColorCards[i]);
            }

            Debug.Log("  - ");

            int packageCount = 0;
            CheckOrderedPackages(min, groupByColorCards, (arg0) => { packageCount++; });

            Debug.Log(packageCount);

            if (packageCount <= 0)
            {
                notSortedCards = cards;
                return null;
            }

            int[] packageCardsCounts = new int[packageCount];
            int tempIndex = 0;


            CheckOrderedPackages(min, groupByColorCards, (arg0) =>
            {
                packageCardsCounts[tempIndex] = arg0;
                tempIndex++;
            });

            for (int i = 0; i < packageCardsCounts.Length; i++)
            {
                Debug.Log(packageCardsCounts[i]);
            }


            NumericColoredCard[][] packagedCards = new NumericColoredCard[packageCount][];
            
            int tempDimensionZero = 0;
            int tempDimensionOne = 0;


            for (int i = 0; i < groupByColorCards.Length; i++)
            {
                int sortedCardCount = 0;
                int selectedNumber = groupByColorCards[i][0].No;

                NumericColoredCard[] tempArray = new NumericColoredCard[groupByColorCards[i].Length];
                Debug.Log("List Cleared");

                for (int j = 0; j < groupByColorCards[i].Length; j++)
                {
                    var cardNo = groupByColorCards[i][j].No;

                    if (selectedNumber + 1 == cardNo)
                    {
                        tempArray[sortedCardCount] = groupByColorCards[i][j - 1];

                        Debug.Log("first " + groupByColorCards[i][j - 1].ToStringBuilder());

                        if (j == groupByColorCards[i].Length - 1)
                        {
                            tempArray[sortedCardCount + 1] = groupByColorCards[i][j];
                            Debug.Log("sec " + groupByColorCards[i][j].ToStringBuilder());
                        }

                        sortedCardCount++;
                    }
                    else
                    {
                        if (sortedCardCount >= min)
                        {
                            if (packagedCards[tempDimensionZero] == null)
                            {
                                packagedCards[tempDimensionZero] =
                                    new NumericColoredCard[packageCardsCounts[tempDimensionZero]];
                                tempDimensionOne = 0;
                            }

                            for (int k = 0; k < tempArray.Length; k++)
                            {
                                if (tempArray[k] != null)
                                {
                                    packagedCards[tempDimensionZero][tempDimensionOne] = tempArray[k];
                                    tempDimensionOne++;
                                }
                            }

                            tempDimensionZero++;

                            //  onPackageFounded?.Invoke(sortedCardCount + 1, tempArray);
                            Debug.Log("List Cleared");
                            tempArray = new NumericColoredCard[groupByColorCards[i].Length];
                        }

                        sortedCardCount = 0;
                    }

                    selectedNumber = cardNo;
                }

                if (sortedCardCount >= min)
                {
                    if (packagedCards[tempDimensionZero] == null)
                    {
                        packagedCards[tempDimensionZero] =
                            new NumericColoredCard[packageCardsCounts[tempDimensionZero]];
                        tempDimensionOne = 0;
                    }

                    for (int k = 0; k < tempArray.Length; k++)
                    {
                        if (tempArray[k] != null)
                        {
                            packagedCards[tempDimensionZero][tempDimensionOne] = tempArray[k];
                            tempDimensionOne++;
                        }
                    }

                    tempDimensionZero++;
                }
            }


            for (int i = 0; i < packagedCards.Length; i++)
            {
                Debug.Log("");
                for (int j = 0; j < packagedCards[i].Length; j++)
                {
                    Debug.Log(packagedCards[i][j].ToStringBuilder());
                }
            }

            notSortedCards = null;
            return packagedCards;
        }

        private static void CheckOrderedPackages(int min, NumericColoredCard[][] groupByColorCards,
            Action<int> onPackageFounded, Action onPackageCardAdded = null)
        {
            for (int i = 0; i < groupByColorCards.Length; i++)
            {
                int sortedCardCount = 0;
                int selectedNumber = groupByColorCards[i][0].No;


                for (int j = 0; j < groupByColorCards[i].Length; j++)
                {
                    var cardNo = groupByColorCards[i][j].No;

                    if (selectedNumber + 1 == cardNo)
                    {
                        sortedCardCount++;
                        onPackageCardAdded?.Invoke();
                    }
                    else
                    {
                        if (sortedCardCount >= min)
                        {
                            onPackageFounded?.Invoke(sortedCardCount + 1);
                        }

                        sortedCardCount = 0;
                    }

                    selectedNumber = cardNo;
                }

                if (sortedCardCount >= min)
                {
                    onPackageFounded?.Invoke(sortedCardCount + 1);
                }
            }
        }

        // private static int[] UniqCardCountsPerCardNumbers(NumericColoredCard[][] groupByColorCards,
        //     out int sameCardCount)
        // {
        //     int[] uniqCardCountsPerCardNos = new int[groupByColorCards.Length];
        //     int tempNo = Int32.MinValue;
        //     sameCardCount = 0;
        //     for (int i = 0; i < groupByColorCards.Length; i++)
        //     {
        //         for (int j = 0; j < groupByColorCards[i].Length; j++)
        //         {
        //             var cardNo = groupByColorCards[i][j].No;
        //             if (cardNo != tempNo)
        //             {
        //                 tempNo = cardNo;
        //                 uniqCardCountsPerCardNos[i]++;
        //             }
        //             else
        //             {
        //                 sameCardCount++;
        //             }
        //         }
        //     }
        //
        //     return uniqCardCountsPerCardNos;
        // }

        public static NumericColoredCard[][] SortByNumericWithoutSizeLimitsAndUniqIds(NumericColoredCard[] cards)
        {
            SortLogic.SortByCardColor(cards);

            var uniqColorCount = GetUniqColorCountFromSortedByCardColor(cards);

            var cardCountsPerColors = GetUniqCardCountsPerColorsFromSortedByColors(cards, uniqColorCount);

            var groupByColorCards = GroupColorCardsFromSortedByColors(cards, uniqColorCount, cardCountsPerColors);

            return groupByColorCards;
        }

        private static NumericColoredCard[][] GroupColorCardsFromSortedByColors(NumericColoredCard[] cards,
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

        public static int[] GetUniqCardCountsPerColorsFromSortedByColors(NumericColoredCard[] cards, int uniqColorCount)
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

        public static int GetUniqColorCountFromSortedByCardColor(NumericColoredCard[] cards)
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