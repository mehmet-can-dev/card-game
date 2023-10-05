using System;

namespace CardGame.Core.Test.Testables
{
    public class SingleColorCardsNoJoker : ITestableCards
    {
        public NumericColoredCard[] GetNotSortedCards()
        {
            return new NumericColoredCard[]
            {
                new NumericColoredCard(0, 1, ColorLogic.red),
                new NumericColoredCard(1, 2, ColorLogic.red),
                new NumericColoredCard(2, 3, ColorLogic.red),
                new NumericColoredCard(3, 4, ColorLogic.red),
                new NumericColoredCard(4, 5, ColorLogic.red),
                new NumericColoredCard(5, 6, ColorLogic.red),
                new NumericColoredCard(6, 7, ColorLogic.red),
                new NumericColoredCard(7, 8, ColorLogic.red),
                new NumericColoredCard(8, 9, ColorLogic.red),
                new NumericColoredCard(9, 10, ColorLogic.red),
                new NumericColoredCard(10, 11, ColorLogic.red),
                new NumericColoredCard(11, 12, ColorLogic.red),
                new NumericColoredCard(12, 13, ColorLogic.red),
            };
        }

        public NumericColoredCard[][] GetNumericSortedCards(out NumericColoredCard[] notSortedCards)
        {
            var splitCards = new NumericColoredCard[1][]
            {
                new[]
                {
                    new NumericColoredCard(0, 1, ColorLogic.red),
                    new NumericColoredCard(1, 2, ColorLogic.red),
                    new NumericColoredCard(2, 3, ColorLogic.red),
                    new NumericColoredCard(3, 4, ColorLogic.red),
                    new NumericColoredCard(4, 5, ColorLogic.red),
                    new NumericColoredCard(5, 6, ColorLogic.red),
                    new NumericColoredCard(6, 7, ColorLogic.red),
                    new NumericColoredCard(7, 8, ColorLogic.red),
                    new NumericColoredCard(8, 9, ColorLogic.red),
                    new NumericColoredCard(9, 10, ColorLogic.red),
                    new NumericColoredCard(10, 11, ColorLogic.red),
                    new NumericColoredCard(11, 12, ColorLogic.red),
                    new NumericColoredCard(12, 13, ColorLogic.red),
                }
            };

            notSortedCards = Array.Empty<NumericColoredCard>();
            
            return splitCards;
        }

        public NumericColoredCard[][] GetColoredSortedCards(out NumericColoredCard[] notSortedCards)
        {
            notSortedCards = new[]
            {
                new NumericColoredCard(0, 1, ColorLogic.red),
                new NumericColoredCard(1, 2, ColorLogic.red),
                new NumericColoredCard(2, 3, ColorLogic.red),
                new NumericColoredCard(3, 4, ColorLogic.red),
                new NumericColoredCard(4, 5, ColorLogic.red),
                new NumericColoredCard(5, 6, ColorLogic.red),
                new NumericColoredCard(6, 7, ColorLogic.red),
                new NumericColoredCard(7, 8, ColorLogic.red),
                new NumericColoredCard(8, 9, ColorLogic.red),
                new NumericColoredCard(9, 10, ColorLogic.red),
                new NumericColoredCard(10, 11, ColorLogic.red),
                new NumericColoredCard(11, 12, ColorLogic.red),
                new NumericColoredCard(12, 13, ColorLogic.red),
            };
            return Array.Empty<NumericColoredCard[]>();
        }

        public NumericColoredCard[][] GetSmartSortedCards(out NumericColoredCard[] notSortedCards)
        {
            var splitCards = new NumericColoredCard[1][]
            {
                new[]
                {
                    new NumericColoredCard(0, 1, ColorLogic.red),
                    new NumericColoredCard(1, 2, ColorLogic.red),
                    new NumericColoredCard(2, 3, ColorLogic.red),
                    new NumericColoredCard(3, 4, ColorLogic.red),
                    new NumericColoredCard(4, 5, ColorLogic.red),
                    new NumericColoredCard(5, 6, ColorLogic.red),
                    new NumericColoredCard(6, 7, ColorLogic.red),
                    new NumericColoredCard(7, 8, ColorLogic.red),
                    new NumericColoredCard(8, 9, ColorLogic.red),
                    new NumericColoredCard(9, 10, ColorLogic.red),
                    new NumericColoredCard(10, 11, ColorLogic.red),
                    new NumericColoredCard(11, 12, ColorLogic.red),
                    new NumericColoredCard(12, 13, ColorLogic.red),
                }
            };

            notSortedCards = Array.Empty<NumericColoredCard>();
            
            return splitCards;
        }
    }
}