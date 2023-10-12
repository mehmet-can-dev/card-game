namespace CardGame.Core.Test.Testables
{
    public class ExampleDocCards : ITestableCards
    {
        public NumericColoredCard[] GetNotSortedCards()
        {
            return new[]
            {
                new NumericColoredCard(0, 9, ColorConstants.Red),
                new NumericColoredCard(1, 11, ColorConstants.Red),
                new NumericColoredCard(2, 8, ColorConstants.Blue),
                new NumericColoredCard(3, 3, ColorConstants.Yellow),
                new JokerCard(4, 10, ColorConstants.Blue),
                new NumericColoredCard(5, 7, ColorConstants.Red),
                new NumericColoredCard(6, 5, ColorConstants.Black),
                new NumericColoredCard(7, 2, ColorConstants.Yellow),
                new NumericColoredCard(8, 10, ColorConstants.Red),
                new NumericColoredCard(9, 4, ColorConstants.Yellow),
                new NumericColoredCard(10, 5, ColorConstants.Yellow),
                new NumericColoredCard(11, 5, ColorConstants.Blue),
                new NumericColoredCard(12, 10, ColorConstants.Yellow),
                new NumericColoredCard(12, 13, ColorConstants.Black),
            };
        }

        public NumericColoredCard[][] GetForwardNumericSortedCards(out NumericColoredCard[] notSortedCards)
        {
            var splitCards = new[]
            {
                new[]
                {
                    new NumericColoredCard(0, 9, ColorConstants.Red),
                    new NumericColoredCard(8, 10, ColorConstants.Red),
                    new NumericColoredCard(1, 11, ColorConstants.Red)
                },
                new[]
                {
                    new NumericColoredCard(7, 2, ColorConstants.Yellow),
                    new NumericColoredCard(3, 3, ColorConstants.Yellow),
                    new NumericColoredCard(9, 4, ColorConstants.Yellow),
                    new NumericColoredCard(10, 5, ColorConstants.Yellow)
                },
            };

            notSortedCards = new[]
            {
                new NumericColoredCard(11, 5, ColorConstants.Blue),
                new NumericColoredCard(2, 8, ColorConstants.Blue),
                new JokerCard(4, 10, ColorConstants.Blue),
                new NumericColoredCard(6, 5, ColorConstants.Black),
                new NumericColoredCard(12, 13, ColorConstants.Black),
                new NumericColoredCard(5, 7, ColorConstants.Red),
                new NumericColoredCard(12, 10, ColorConstants.Yellow)
            };

            return splitCards;
        }

        public NumericColoredCard[][] GetForwardColoredSortedCards(out NumericColoredCard[] notSortedCards)
        {
            var splitCards = new[]
            {
                new[]
                {
                    new NumericColoredCard(12, 10, ColorConstants.Yellow),
                    new NumericColoredCard(8, 10, ColorConstants.Red),
                    new JokerCard(4, 10, ColorConstants.Blue),
                },
                new[]
                {
                    new NumericColoredCard(10, 5, ColorConstants.Yellow),
                    new NumericColoredCard(6, 5, ColorConstants.Black),
                    new NumericColoredCard(11, 5, ColorConstants.Blue),
                },
            };

            notSortedCards = new[]
            {
                new NumericColoredCard(2, 8, ColorConstants.Blue),
                new NumericColoredCard(12, 13, ColorConstants.Black),
                new NumericColoredCard(5, 7, ColorConstants.Red),
                new NumericColoredCard(0, 9, ColorConstants.Red),
                new NumericColoredCard(1, 11, ColorConstants.Red),
                new NumericColoredCard(7, 2, ColorConstants.Yellow),
                new NumericColoredCard(3, 3, ColorConstants.Yellow),
                new NumericColoredCard(9, 4, ColorConstants.Yellow)
            };

            return splitCards;
        }

        public NumericColoredCard[][] GetSmartSortedCards(out NumericColoredCard[] notSortedCards)
        {
            var splitCards = new[]
            {
                new[]
                {
                    new NumericColoredCard(7, 2, ColorConstants.Yellow),
                    new NumericColoredCard(3, 3, ColorConstants.Yellow),
                    new NumericColoredCard(9, 4, ColorConstants.Yellow)
                },
                new[]
                {
                    new NumericColoredCard(10, 5, ColorConstants.Yellow),
                    new NumericColoredCard(6, 5, ColorConstants.Black),
                    new NumericColoredCard(11, 5, ColorConstants.Blue),
                },
                new[]
                {
                    new NumericColoredCard(5, 7, ColorConstants.Red),
                    new JokerCard(4, 10, ColorConstants.Blue),
                    new NumericColoredCard(0, 9, ColorConstants.Red),
                    new NumericColoredCard(8, 10, ColorConstants.Red),
                    new NumericColoredCard(1, 11, ColorConstants.Red),
                }
            };

            notSortedCards = new[]
            {
                new NumericColoredCard(2, 8, ColorConstants.Blue),
                new NumericColoredCard(12, 13, ColorConstants.Black),
                new NumericColoredCard(12, 10, ColorConstants.Yellow),
            };

            return splitCards;
        }
    }
}