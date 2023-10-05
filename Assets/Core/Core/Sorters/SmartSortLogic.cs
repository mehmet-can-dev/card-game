namespace CardGame.Core.Sort
{
    public class SmartSortLogic
    {
        public static NumericColoredCard[][] SortBySmart(NumericColoredCard[] cards,
            out NumericColoredCard[] notSortedCards, int min, int max)
        {
            // var jokerCards = SortUtilities.SplitJokerCard(cards, out var withoutSJokerCards);

            var coloredSortedCards =
                ColoredSortLogic.SortByColored(cards, min, max, out var notSortedCardsAfterColor);
            var numericSortedCardsAfterSortedColor =
                NumericSortLogic.SortByNumeric(notSortedCardsAfterColor,
                    out var notSortableCardsAfterSortedColor, min);

            var numericSortedCards =
                NumericSortLogic.SortByNumeric(cards, out var notSortedCardsAfterNumeric, min);

            var coloredSortedCardsAfterSortedNumeric = ColoredSortLogic.SortByColored(
                notSortedCardsAfterNumeric, min, max, out var notSortableCardsAfterSortedNumeric);


            NumericColoredCard[][] selectedCards;
            if (notSortableCardsAfterSortedColor.Length >= notSortableCardsAfterSortedNumeric.Length)
            {
                notSortedCards = notSortableCardsAfterSortedNumeric;

                if (ReferenceEquals(numericSortedCards, null))
                    selectedCards = coloredSortedCardsAfterSortedNumeric;
                else if (ReferenceEquals(coloredSortedCardsAfterSortedNumeric, null))
                    selectedCards = numericSortedCards;
                else
                    selectedCards =
                        CardArrayUtilities.Merge2DimensionArray(numericSortedCards, coloredSortedCardsAfterSortedNumeric);
            }
            else
            {
                notSortedCards = notSortableCardsAfterSortedColor;

                if (ReferenceEquals(coloredSortedCards, null))
                    selectedCards = numericSortedCardsAfterSortedColor;
                else if (ReferenceEquals(numericSortedCardsAfterSortedColor, null))
                    selectedCards = coloredSortedCards;
                else
                    selectedCards =
                        CardArrayUtilities.Merge2DimensionArray(coloredSortedCards, numericSortedCardsAfterSortedColor);
                // SortUtilities.AddJokerCardsToNumericCardsArray(notSortableCardsAfterSortedColor, jokerCards);
            }

            return selectedCards;
        }
    }
}