using System;

namespace CardGame.Core.Test.Testables
{
    public class ColoredNumericCardsNoJoker: ITestableCards
    {
        public NumericColoredCard[] GetNotSortedCards()
        {
            return new NumericColoredCard[]
            {
                new NumericColoredCard(0, 9, ColorConstants.Black),
                new NumericColoredCard(1, 10, ColorConstants.Black),
                new NumericColoredCard(2, 11, ColorConstants.Black),
                new NumericColoredCard(3, 12, ColorConstants.Black),
                new NumericColoredCard(4, 4, ColorConstants.Black),
                new NumericColoredCard(5, 4, ColorConstants.Blue),
                new NumericColoredCard(6, 4, ColorConstants.Yellow),
                new NumericColoredCard(7, 6, ColorConstants.Black),
                new NumericColoredCard(8, 6, ColorConstants.Blue),
                new NumericColoredCard(9, 6, ColorConstants.Red),
                new NumericColoredCard(10, 2, ColorConstants.Blue),
                new NumericColoredCard(11, 2, ColorConstants.Red),
                new NumericColoredCard(12, 4, ColorConstants.Blue),
                new NumericColoredCard(12, 7, ColorConstants.Red),
            };
        }

        public NumericColoredCard[][] GetNumericSortedCards(out NumericColoredCard[] notSortedCards)
        {
            var splitCards = new NumericColoredCard[][]
            {
                new[]
                {
                    new NumericColoredCard(0, 9, ColorConstants.Black),
                    new NumericColoredCard(1, 10, ColorConstants.Black),
                    new NumericColoredCard(2, 11, ColorConstants.Black),
                    new NumericColoredCard(3, 12, ColorConstants.Black),
                },
            };

            notSortedCards = new[]
            {
                new NumericColoredCard(4, 4, ColorConstants.Black),
                new NumericColoredCard(5, 4, ColorConstants.Blue),
                new NumericColoredCard(6, 4, ColorConstants.Yellow),
                new NumericColoredCard(7, 6, ColorConstants.Black),
                new NumericColoredCard(8, 6, ColorConstants.Blue),
                new NumericColoredCard(9, 6, ColorConstants.Red),
                new NumericColoredCard(10, 2, ColorConstants.Blue),
                new NumericColoredCard(11, 2, ColorConstants.Red),
                new NumericColoredCard(12, 4, ColorConstants.Blue),
                new NumericColoredCard(12, 7, ColorConstants.Red),
            };

            return splitCards;
        }

        public NumericColoredCard[][] GetColoredSortedCards(out NumericColoredCard[] notSortedCards)
        {
            var splitCards = new NumericColoredCard[][]
            {
                new[]
                {
                    new NumericColoredCard(4, 4, ColorConstants.Black),
                    new NumericColoredCard(5, 4, ColorConstants.Blue),
                    new NumericColoredCard(6, 4, ColorConstants.Yellow),
                },
                new[]
                {
                    new NumericColoredCard(7, 6, ColorConstants.Black),
                    new NumericColoredCard(8, 6, ColorConstants.Blue),
                    new NumericColoredCard(9, 6, ColorConstants.Red),
                }
            };

            notSortedCards = new[]
            {
                new NumericColoredCard(10, 2, ColorConstants.Blue),
                new NumericColoredCard(11, 2, ColorConstants.Red),
                new NumericColoredCard(12, 4, ColorConstants.Blue),
                new NumericColoredCard(12, 7, ColorConstants.Red),
                new NumericColoredCard(0, 9, ColorConstants.Black),
                new NumericColoredCard(1, 10, ColorConstants.Black),
                new NumericColoredCard(2, 11, ColorConstants.Black),
                new NumericColoredCard(3, 12, ColorConstants.Black),
            };
            return splitCards;
        }

        public NumericColoredCard[][] GetSmartSortedCards(out NumericColoredCard[] notSortedCards)
        {
            var splitCards = new NumericColoredCard[][]
            {
                new[]
                {
                    new NumericColoredCard(0, 9, ColorConstants.Black),
                    new NumericColoredCard(1, 10, ColorConstants.Black),
                    new NumericColoredCard(2, 11, ColorConstants.Black),
                    new NumericColoredCard(3, 12, ColorConstants.Black),
                },
                new[]
                {
                    new NumericColoredCard(4, 4, ColorConstants.Black),
                    new NumericColoredCard(5, 4, ColorConstants.Blue),
                    new NumericColoredCard(6, 4, ColorConstants.Yellow),
                },
                new[]
                {
                    new NumericColoredCard(7, 6, ColorConstants.Black),
                    new NumericColoredCard(8, 6, ColorConstants.Blue),
                    new NumericColoredCard(9, 6, ColorConstants.Red),
                }
            };


            notSortedCards = new[]
            {
                new NumericColoredCard(10, 2, ColorConstants.Blue),
                new NumericColoredCard(11, 2, ColorConstants.Red),
                new NumericColoredCard(12, 4, ColorConstants.Blue),
                new NumericColoredCard(12, 7, ColorConstants.Red),
            };

            return splitCards;
        }
    }
}