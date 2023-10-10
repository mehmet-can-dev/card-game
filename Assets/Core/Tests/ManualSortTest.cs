using CardGame.Core.Sort;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CardGame.Core.Test
{
    public class ManualSortTest
    {
        NumericColoredCard[] testCards_noJoker_colored_numeric = new NumericColoredCard[]
        {
            new NumericColoredCard(0, 2, ColorConstants.Red),
            new NumericColoredCard(1, 2, ColorConstants.Blue),
            new NumericColoredCard(2, 2, ColorConstants.Black),
            new NumericColoredCard(3, 2, ColorConstants.Yellow),

            new NumericColoredCard(5, 5, ColorConstants.Yellow),
            new NumericColoredCard(6, 5, ColorConstants.Red),
            new NumericColoredCard(7, 5, ColorConstants.Black),

            new NumericColoredCard(8, 9, ColorConstants.Red),
            new NumericColoredCard(9, 10, ColorConstants.Red),
            new NumericColoredCard(21, 11, ColorConstants.Red),
            new NumericColoredCard(10, 12, ColorConstants.Red),

            new NumericColoredCard(12, 15, ColorConstants.Red),
            new NumericColoredCard(22, 15, ColorConstants.Blue),
            new NumericColoredCard(23, 15, ColorConstants.Yellow),

            new NumericColoredCard(14, 17, ColorConstants.Red),
            new NumericColoredCard(15, 17, ColorConstants.Blue),
            new NumericColoredCard(16, 17, ColorConstants.Yellow),

            new NumericColoredCard(19, 1, ColorConstants.Black),
            new NumericColoredCard(20, 3, ColorConstants.Red),
        };

        NumericColoredCard[] testCards_numeric = new NumericColoredCard[]
        {
            new NumericColoredCard(0, 2, ColorConstants.Red),
            new NumericColoredCard(1, 2, ColorConstants.Red),
            new NumericColoredCard(2, 3, ColorConstants.Red),
            new NumericColoredCard(3, 3, ColorConstants.Red),
            new NumericColoredCard(4, 4, ColorConstants.Red),
            new NumericColoredCard(5, 5, ColorConstants.Red),
            new NumericColoredCard(20, 4, ColorConstants.Red),
            new NumericColoredCard(6, 6, ColorConstants.Red),
            new NumericColoredCard(7, 7, ColorConstants.Red),
            new NumericColoredCard(8, 9, ColorConstants.Red),
            new NumericColoredCard(9, 10, ColorConstants.Red),
            new NumericColoredCard(10, 12, ColorConstants.Red),
            new NumericColoredCard(12, 15, ColorConstants.Red),
            new NumericColoredCard(13, 16, ColorConstants.Red),
            new NumericColoredCard(14, 17, ColorConstants.Red),
            new NumericColoredCard(15, 17, ColorConstants.Blue),
            new NumericColoredCard(16, 18, ColorConstants.Blue),
            new NumericColoredCard(17, 18, ColorConstants.Blue),
            new NumericColoredCard(18, 19, ColorConstants.Blue),
            new JokerCard(2, 11, ColorConstants.Black),
            new NumericColoredCard(19, 1, ColorConstants.Black)
        };

        NumericColoredCard[] testCards_fullnumeric_double = new NumericColoredCard[]
        {
            new NumericColoredCard(0, 9, ColorConstants.Black),
            new NumericColoredCard(1, 10, ColorConstants.Black),
            new JokerCard(2, 11, ColorConstants.Black),
            new NumericColoredCard(3, 12, ColorConstants.Black),
            new NumericColoredCard(4, 4, ColorConstants.Black),
            new NumericColoredCard(5, 4, ColorConstants.Blue),
            new JokerCard(18, 11, ColorConstants.Red),
            new NumericColoredCard(6, 4, ColorConstants.Yellow),
            new NumericColoredCard(7, 6, ColorConstants.Black),
            new NumericColoredCard(8, 6, ColorConstants.Blue),
            new NumericColoredCard(9, 6, ColorConstants.Red),
            new NumericColoredCard(10, 2, ColorConstants.Blue),
            new NumericColoredCard(11, 2, ColorConstants.Red),
            new NumericColoredCard(12, 4, ColorConstants.Blue),
            new NumericColoredCard(12, 7, ColorConstants.Red),
        };
        
        NumericColoredCard[] testCards_fullcolored = new NumericColoredCard[]
        {
            new NumericColoredCard(0, 8, ColorConstants.Yellow),
            new NumericColoredCard(1, 8, ColorConstants.Black),
            new JokerCard(2, 11, ColorConstants.Black),
            new NumericColoredCard(3, 8, ColorConstants.Yellow),
            new NumericColoredCard(4, 4, ColorConstants.Red),
            new NumericColoredCard(5, 4, ColorConstants.Black),
            new JokerCard(18, 11, ColorConstants.Red),
            new NumericColoredCard(6, 3, ColorConstants.Yellow),
            new NumericColoredCard(7, 5, ColorConstants.Blue),
            new NumericColoredCard(8, 7, ColorConstants.Blue),
            new NumericColoredCard(9, 9, ColorConstants.Black),
            new NumericColoredCard(10, 9, ColorConstants.Yellow),
            new NumericColoredCard(11, 12, ColorConstants.Black),
            new NumericColoredCard(12, 13, ColorConstants.Blue),
        };



        [Test]
        public void SortNumericTest1()
        {
            NumericTest(testCards_noJoker_colored_numeric);
        }


        [Test]
        public void SortSmartTest1()
        {
            SmartTest(testCards_noJoker_colored_numeric);
        }

        [Test]
        public void SortSmartTest2()
        {
            SmartTest(testCards_numeric);
        }

        [Test]
        public void SortSmartTest3()
        {
            SmartTest(testCards_fullnumeric_double);
        }
        
        [Test]
        public void SortSmartTest4()
        {
            SmartTest(testCards_fullcolored);
        }

        [Test]
        public void SortColoredTest1()
        {
            ColoredTest(testCards_noJoker_colored_numeric);
        }

        [Test]
        public void SortColoredTest2()
        {
            ColoredTest(testCards_numeric);
        }

        [Test]
        public void SortColoredTest3()
        {
            ColoredTest(testCards_fullnumeric_double);
        }

        private static void ColoredTest(NumericColoredCard[] testCards)
        {
            LogAssert.Expect(LogType.Log, "Log");

            var cards = ColoredSortLogic.SortByColored(testCards, 3, 4, out var notSortableCard);

            CardArrayUtilities.Log2DimensionNumericArray(cards);

            CardArrayUtilities.LogNumericArray(notSortableCard);

            Assert.Pass();
        }

        private static void SmartTest(NumericColoredCard[] testCards)
        {
            LogAssert.Expect(LogType.Log, "Log");

            var cards = SmartSortLogic.SortBySmart(testCards, out var notSortableCard, 3, ColorConstants.UsedColors.Length,
                13);

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

            var cards = NumericSortLogic.SortByNumeric(testCards, out var notSortableCard, 3);

            CardArrayUtilities.Log2DimensionNumericArray(cards);

            Assert.Pass();
        }
    }
}