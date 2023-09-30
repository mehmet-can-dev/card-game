namespace CardGame.Core.Sort
{
    public static class NumericSortLogic
    {
        public static NumericColoredCard[][] SortByNumeric(NumericColoredCard[] cards)
        {
            SortLogic.SortByCardColor(cards);

            var uniqColorCount = GetUniqColorCountFromSortedByCardColor(cards);

            var cardCountPerColors = GetCardCountPerColorsFromSortedByColors(cards, uniqColorCount);

            var groupByColorCards = GroupColorCardsFromSortedByColors(cards, uniqColorCount, cardCountPerColors);

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

                if (groupByColorCards[currentColorIndex] == null)
                {
                    groupByColorCards[currentColorIndex] =
                        new NumericColoredCard[cardCountPerColors[currentColorIndex]];
                }

                groupByColorCards[currentColorIndex][groupIndex] = cards[i];
                groupIndex++;
            }

            return groupByColorCards;
        }

        public static int[] GetCardCountPerColorsFromSortedByColors(NumericColoredCard[] cards, int uniqColorCount)
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