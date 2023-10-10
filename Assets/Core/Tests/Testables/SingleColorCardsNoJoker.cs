using System;

namespace CardGame.Core.Test.Testables
{
    public class SingleColorCardsNoJoker : ITestableCards
    {
        public NumericColoredCard[] GetNotSortedCards()
        {
            return new NumericColoredCard[]
            {
                new NumericColoredCard(0, 1, ColorConstants.Red),
                new NumericColoredCard(1, 2, ColorConstants.Red),
                new NumericColoredCard(2, 3, ColorConstants.Red),
                new NumericColoredCard(3, 4, ColorConstants.Red),
                new NumericColoredCard(4, 5, ColorConstants.Red),
                new NumericColoredCard(5, 6, ColorConstants.Red),
                new NumericColoredCard(6, 7, ColorConstants.Red),
                new NumericColoredCard(7, 8, ColorConstants.Red),
                new NumericColoredCard(8, 9, ColorConstants.Red),
                new NumericColoredCard(9, 10, ColorConstants.Red),
                new NumericColoredCard(10, 11, ColorConstants.Red),
                new NumericColoredCard(11, 12, ColorConstants.Red),
                new NumericColoredCard(12, 13, ColorConstants.Red),
            };
        }

        public NumericColoredCard[][] GetNumericSortedCards(out NumericColoredCard[] notSortedCards)
        {
            var splitCards = new NumericColoredCard[1][]
            {
                new[]
                {
                    new NumericColoredCard(0, 1, ColorConstants.Red),
                    new NumericColoredCard(1, 2, ColorConstants.Red),
                    new NumericColoredCard(2, 3, ColorConstants.Red),
                    new NumericColoredCard(3, 4, ColorConstants.Red),
                    new NumericColoredCard(4, 5, ColorConstants.Red),
                    new NumericColoredCard(5, 6, ColorConstants.Red),
                    new NumericColoredCard(6, 7, ColorConstants.Red),
                    new NumericColoredCard(7, 8, ColorConstants.Red),
                    new NumericColoredCard(8, 9, ColorConstants.Red),
                    new NumericColoredCard(9, 10, ColorConstants.Red),
                    new NumericColoredCard(10, 11, ColorConstants.Red),
                    new NumericColoredCard(11, 12, ColorConstants.Red),
                    new NumericColoredCard(12, 13, ColorConstants.Red),
                }
            };

            notSortedCards = Array.Empty<NumericColoredCard>();
            
            return splitCards;
        }

        public NumericColoredCard[][] GetColoredSortedCards(out NumericColoredCard[] notSortedCards)
        {
            notSortedCards = new[]
            {
                new NumericColoredCard(0, 1, ColorConstants.Red),
                new NumericColoredCard(1, 2, ColorConstants.Red),
                new NumericColoredCard(2, 3, ColorConstants.Red),
                new NumericColoredCard(3, 4, ColorConstants.Red),
                new NumericColoredCard(4, 5, ColorConstants.Red),
                new NumericColoredCard(5, 6, ColorConstants.Red),
                new NumericColoredCard(6, 7, ColorConstants.Red),
                new NumericColoredCard(7, 8, ColorConstants.Red),
                new NumericColoredCard(8, 9, ColorConstants.Red),
                new NumericColoredCard(9, 10, ColorConstants.Red),
                new NumericColoredCard(10, 11, ColorConstants.Red),
                new NumericColoredCard(11, 12, ColorConstants.Red),
                new NumericColoredCard(12, 13, ColorConstants.Red),
            };
            return Array.Empty<NumericColoredCard[]>();
        }

        public NumericColoredCard[][] GetSmartSortedCards(out NumericColoredCard[] notSortedCards)
        {
            var splitCards = new NumericColoredCard[1][]
            {
                new[]
                {
                    new NumericColoredCard(0, 1, ColorConstants.Red),
                    new NumericColoredCard(1, 2, ColorConstants.Red),
                    new NumericColoredCard(2, 3, ColorConstants.Red),
                    new NumericColoredCard(3, 4, ColorConstants.Red),
                    new NumericColoredCard(4, 5, ColorConstants.Red),
                    new NumericColoredCard(5, 6, ColorConstants.Red),
                    new NumericColoredCard(6, 7, ColorConstants.Red),
                    new NumericColoredCard(7, 8, ColorConstants.Red),
                    new NumericColoredCard(8, 9, ColorConstants.Red),
                    new NumericColoredCard(9, 10, ColorConstants.Red),
                    new NumericColoredCard(10, 11, ColorConstants.Red),
                    new NumericColoredCard(11, 12, ColorConstants.Red),
                    new NumericColoredCard(12, 13, ColorConstants.Red),
                }
            };

            notSortedCards = Array.Empty<NumericColoredCard>();
            
            return splitCards;
        }
    }
}