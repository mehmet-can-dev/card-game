using CardGame.Core.Sort;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CardGame.Core.Test
{
    public class SortTest
    {
        NumericColoredCard[] testCards_noJoker_colored_numeric = new NumericColoredCard[]
        {
            new NumericColoredCard(0, 2, ColorLogic.red),
            new NumericColoredCard(1, 2, ColorLogic.blue),
            new NumericColoredCard(2, 2, ColorLogic.black),
            new NumericColoredCard(3, 2, ColorLogic.yellow),

            new NumericColoredCard(5, 5, ColorLogic.yellow),
            new NumericColoredCard(6, 5, ColorLogic.red),
            new NumericColoredCard(7, 5, ColorLogic.black),

            new NumericColoredCard(8, 9, ColorLogic.red),
            new NumericColoredCard(9, 10, ColorLogic.red),
            new NumericColoredCard(21, 11, ColorLogic.red),
            new NumericColoredCard(10, 12, ColorLogic.red),

            new NumericColoredCard(12, 15, ColorLogic.red),
            new NumericColoredCard(22, 15, ColorLogic.blue),
            new NumericColoredCard(23, 15, ColorLogic.yellow),

            new NumericColoredCard(14, 17, ColorLogic.red),
            new NumericColoredCard(15, 17, ColorLogic.blue),
            new NumericColoredCard(16, 17, ColorLogic.yellow),

            new NumericColoredCard(19, 1, ColorLogic.black),
            new NumericColoredCard(20, 3, ColorLogic.red),
        };

        NumericColoredCard[] testCards_noJoker_numeric = new NumericColoredCard[]
        {
            new NumericColoredCard(0, 2, ColorLogic.red),
            new NumericColoredCard(1, 2, ColorLogic.red),
            new NumericColoredCard(2, 3, ColorLogic.red),
            new NumericColoredCard(3, 3, ColorLogic.red),
            new NumericColoredCard(4, 4, ColorLogic.red),
            new NumericColoredCard(5, 5, ColorLogic.red),
            new NumericColoredCard(6, 7, ColorLogic.red),
            new NumericColoredCard(7, 8, ColorLogic.red),
            new NumericColoredCard(8, 9, ColorLogic.red),
            new NumericColoredCard(9, 10, ColorLogic.red),
            new NumericColoredCard(10, 12, ColorLogic.red),
            new NumericColoredCard(12, 15, ColorLogic.red),
            new NumericColoredCard(13, 16, ColorLogic.red),
            new NumericColoredCard(14, 17, ColorLogic.red),
            new NumericColoredCard(15, 17, ColorLogic.blue),
            new NumericColoredCard(16, 18, ColorLogic.blue),
            new NumericColoredCard(17, 18, ColorLogic.blue),
            new NumericColoredCard(18, 19, ColorLogic.blue),

            new NumericColoredCard(19, 1, ColorLogic.black)
        };

        NumericColoredCard[] testCards_noJoker_fullnumeric_double = new NumericColoredCard[]
        {
            new NumericColoredCard(0, 2, ColorLogic.red),
            new NumericColoredCard(2, 3, ColorLogic.red),
            new NumericColoredCard(4, 4, ColorLogic.red),
            new NumericColoredCard(5, 5, ColorLogic.red),
            new NumericColoredCard(6, 7, ColorLogic.red),
            new NumericColoredCard(7, 8, ColorLogic.red),
            new NumericColoredCard(8, 9, ColorLogic.red),
            new NumericColoredCard(9, 10, ColorLogic.red),
            new NumericColoredCard(12, 15, ColorLogic.red),
            new NumericColoredCard(13, 16, ColorLogic.red),
            new NumericColoredCard(14, 17, ColorLogic.red),
            new NumericColoredCard(16, 18, ColorLogic.blue),
            new NumericColoredCard(18, 19, ColorLogic.blue),

            new NumericColoredCard(1, 2, ColorLogic.red),
            new NumericColoredCard(3, 3, ColorLogic.red),
            new NumericColoredCard(5, 4, ColorLogic.red),

            new NumericColoredCard(10, 12, ColorLogic.red),
            new NumericColoredCard(15, 17, ColorLogic.blue),
        };


        [Test]
        public void SortNumericTest1()
        {
            NumericTest(testCards_noJoker_colored_numeric);
        }

        [Test]
        public void SortNumericTest2()
        {
            NumericTest(testCards_noJoker_numeric);
        }

        [Test]
        public void SortNumericTest3()
        {
            NumericTest(testCards_noJoker_fullnumeric_double);
        }


        [Test]
        public void SortSmartTest1()
        {
            SmartTest(testCards_noJoker_colored_numeric);
        }

        [Test]
        public void SortSmartTest2()
        {
            SmartTest(testCards_noJoker_numeric);
        }

        [Test]
        public void SortSmartTest3()
        {
            SmartTest(testCards_noJoker_fullnumeric_double);
        }
        
        [Test]
        public void SortColoredTest1()
        {
            ColoredTest(testCards_noJoker_colored_numeric);
        }

        [Test]
        public void SortColoredTest2()
        {
            ColoredTest(testCards_noJoker_numeric);
        }

        [Test]
        public void SortColoredTest3()
        {
            ColoredTest(testCards_noJoker_fullnumeric_double);
        }
        

        private static void ColoredTest(NumericColoredCard[] testCards)
        {
            LogAssert.Expect(LogType.Log, "Log");

            var hand = new Hand(testCards.Length);

            for (int i = 0; i < testCards.Length; i++)
            {
                hand.AddCard(testCards[i]);
            }

            var cards = ColoredSortLogic.SortByColored(hand.Cards, 3, 4, out var notSortableCard);

            CardArrayUtilities.Log2DimensionNumericArray(cards);

            CardArrayUtilities.LogNumericArray(notSortableCard);

            Assert.Pass();
        }

        private static void SmartTest(NumericColoredCard[] testCards)
        {
            LogAssert.Expect(LogType.Log, "Log");

            var hand = new Hand(testCards.Length);

            for (int i = 0; i < testCards.Length; i++)
            {
                hand.AddCard(testCards[i]);
            }

            var cards = SmartSortLogic.SortBySmart(hand.Cards, out var notSortableCard, 3, 4);

            CardArrayUtilities.Log2DimensionNumericArray(cards);

            CardArrayUtilities.LogNumericArray(notSortableCard);

            Assert.Pass();
        }

        private static void NumericTest(NumericColoredCard[] testCards)
        {
            LogAssert.Expect(LogType.Log, "Log");

            var hand = new Hand(testCards.Length);

            for (int i = 0; i < testCards.Length; i++)
            {
                hand.AddCard(testCards[i]);
            }

            var cards = NumericSortLogic.SortByNumeric(hand.Cards, out var notSortableCard, 3);

            CardArrayUtilities.Log2DimensionNumericArray(cards);

            Assert.Pass();
        }
    }
}