using System.Collections.Generic;
using CardGame.Core.Sort;
using CardGame.Core.Test.Testables;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CardGame.Core.Test
{
    public class AutomaticSortTest
    {
        private List<ITestableCards> testableCards = new List<ITestableCards>()
        {
            new SingleColorCardsNoJoker(),
            new ColoredNumericCardsNoJoker()
        };

        [Test]
        public void SortNumericTest()
        {
            for (int i = 0; i < testableCards.Count; i++)
            {
                NumericTest(testableCards[i]);
            }
        }

        [Test]
        public void SortSmartTest()
        {
            for (int i = 0; i < testableCards.Count; i++)
            {
                SmartTest(testableCards[i]);
            }
        }

        [Test]
        public void SortColoredTest()
        {
            for (int i = 0; i < testableCards.Count; i++)
            {
                ColoredTest(testableCards[i]);
            }
        }


        private static void ColoredTest(ITestableCards testableCards)
        {
            LogAssert.Expect(LogType.Log, "Log");

            var testCards = testableCards.GetNotSortedCards();

            var cards = ColoredSortLogic.SortByColored(testCards, 3, 4, out var notSortableCard);

            CardArrayUtilities.Log2DimensionNumericArray(cards);

            CardArrayUtilities.LogNumericArray(notSortableCard);

            Assert.Pass();
        }

        private static void SmartTest(ITestableCards testableCards)
        {
            LogAssert.Expect(LogType.Log, "Log");

            var testCards = testableCards.GetNotSortedCards();

            var cards = SmartSortLogic.SortBySmart(testCards, out var notSortableCard, 3);

            CardArrayUtilities.Log2DimensionNumericArray(cards);

            CardArrayUtilities.LogNumericArray(notSortableCard);

            Assert.Pass();
        }

        private static void NumericTest(ITestableCards testableCards)
        {
            LogAssert.Expect(LogType.Log, "Log");
            var testCards = testableCards.GetNotSortedCards();

            var cards = NumericSortLogic.SortByNumeric(testCards, out var notSortableCard, 3);

            CardArrayUtilities.Log2DimensionNumericArray(cards);

            Assert.Pass();
        }
    }
}