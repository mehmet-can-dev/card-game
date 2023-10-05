using System;

namespace CardGame.Core.Test.Testables
{
    public class ColoredNumericCardsNoJoker: ITestableCards
    {
        public NumericColoredCard[] GetNotSortedCards()
        {
            return new NumericColoredCard[]
            {
                new NumericColoredCard(0, 9, ColorLogic.black),
                new NumericColoredCard(1, 10, ColorLogic.black),
                new NumericColoredCard(2, 11, ColorLogic.black),
                new NumericColoredCard(3, 12, ColorLogic.black),
                new NumericColoredCard(4, 4, ColorLogic.black),
                new NumericColoredCard(5, 4, ColorLogic.blue),
                new NumericColoredCard(6, 4, ColorLogic.yellow),
                new NumericColoredCard(7, 6, ColorLogic.black),
                new NumericColoredCard(8, 6, ColorLogic.blue),
                new NumericColoredCard(9, 6, ColorLogic.red),
                new NumericColoredCard(10, 2, ColorLogic.blue),
                new NumericColoredCard(11, 2, ColorLogic.red),
                new NumericColoredCard(12, 4, ColorLogic.blue),
                new NumericColoredCard(12, 7, ColorLogic.red),
            };
        }

        public NumericColoredCard[][] GetNumericSortedCards(out NumericColoredCard[] notSortedCards)
        {
            var splitCards = new NumericColoredCard[][]
            {
                new[]
                {
                    new NumericColoredCard(0, 9, ColorLogic.black),
                    new NumericColoredCard(1, 10, ColorLogic.black),
                    new NumericColoredCard(2, 11, ColorLogic.black),
                    new NumericColoredCard(3, 12, ColorLogic.black),
                },
            };

            notSortedCards = new[]
            {
                new NumericColoredCard(4, 4, ColorLogic.black),
                new NumericColoredCard(5, 4, ColorLogic.blue),
                new NumericColoredCard(6, 4, ColorLogic.yellow),
                new NumericColoredCard(7, 6, ColorLogic.black),
                new NumericColoredCard(8, 6, ColorLogic.blue),
                new NumericColoredCard(9, 6, ColorLogic.red),
                new NumericColoredCard(10, 2, ColorLogic.blue),
                new NumericColoredCard(11, 2, ColorLogic.red),
                new NumericColoredCard(12, 4, ColorLogic.blue),
                new NumericColoredCard(12, 7, ColorLogic.red),
            };

            return splitCards;
        }

        public NumericColoredCard[][] GetColoredSortedCards(out NumericColoredCard[] notSortedCards)
        {
            var splitCards = new NumericColoredCard[][]
            {
                new[]
                {
                    new NumericColoredCard(4, 4, ColorLogic.black),
                    new NumericColoredCard(5, 4, ColorLogic.blue),
                    new NumericColoredCard(6, 4, ColorLogic.yellow),
                },
                new[]
                {
                    new NumericColoredCard(7, 6, ColorLogic.black),
                    new NumericColoredCard(8, 6, ColorLogic.blue),
                    new NumericColoredCard(9, 6, ColorLogic.red),
                }
            };

            notSortedCards = new[]
            {
                new NumericColoredCard(10, 2, ColorLogic.blue),
                new NumericColoredCard(11, 2, ColorLogic.red),
                new NumericColoredCard(12, 4, ColorLogic.blue),
                new NumericColoredCard(12, 7, ColorLogic.red),
                new NumericColoredCard(0, 9, ColorLogic.black),
                new NumericColoredCard(1, 10, ColorLogic.black),
                new NumericColoredCard(2, 11, ColorLogic.black),
                new NumericColoredCard(3, 12, ColorLogic.black),
            };
            return splitCards;
        }

        public NumericColoredCard[][] GetSmartSortedCards(out NumericColoredCard[] notSortedCards)
        {
            var splitCards = new NumericColoredCard[][]
            {
                new[]
                {
                    new NumericColoredCard(0, 9, ColorLogic.black),
                    new NumericColoredCard(1, 10, ColorLogic.black),
                    new NumericColoredCard(2, 11, ColorLogic.black),
                    new NumericColoredCard(3, 12, ColorLogic.black),
                },
                new[]
                {
                    new NumericColoredCard(4, 4, ColorLogic.black),
                    new NumericColoredCard(5, 4, ColorLogic.blue),
                    new NumericColoredCard(6, 4, ColorLogic.yellow),
                },
                new[]
                {
                    new NumericColoredCard(7, 6, ColorLogic.black),
                    new NumericColoredCard(8, 6, ColorLogic.blue),
                    new NumericColoredCard(9, 6, ColorLogic.red),
                }
            };


            notSortedCards = new[]
            {
                new NumericColoredCard(10, 2, ColorLogic.blue),
                new NumericColoredCard(11, 2, ColorLogic.red),
                new NumericColoredCard(12, 4, ColorLogic.blue),
                new NumericColoredCard(12, 7, ColorLogic.red),
            };

            return splitCards;
        }
    }
}