using System.Collections.Generic;
using System.Linq;
using CardGame.Core.Sort;
using CardGame.Core.Sort.Forward;
using CardGame.Core.Sort.Recursive;
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
            new ExampleDocCards()
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

            var testCards = testableCards.GetNotSortedCards();

            var sorter = new ColoredForwardSort();

            var cards = sorter.Sort(testCards, out var notSortableCard, 3, 4, ColorConstants.UsedColors.Length, 1,
                13);

            Assert.Pass();
        }

        private static void SmartTest(ITestableCards testableCards)
        {
            var testCards = testableCards.GetNotSortedCards();

            var sorter = new SmartRecursiveSort();

            var cards = sorter.Sort(testCards, out var notSortableCard, 3, 4, ColorConstants.UsedColors.Length,
                1, 13);

            var targetCards = testableCards.GetSmartSortedCards(out var targetNotSortable);

            var cardList = ArrayToList(cards);
            var notSortableList = notSortableCard.ToList();

            var targetCardList = ArrayToList(targetCards);
            var targetNotSortableList = targetNotSortable.ToList();
            
            Assert.IsTrue(IsCardListEqual(notSortableList, targetNotSortableList, false));
        }

        private static void NumericTest(ITestableCards testableCards)
        {
            LogAssert.Expect(LogType.Log, "Log");
            var testCards = testableCards.GetNotSortedCards();

            var sorter = new NumericForwardSort();

            var cards = sorter.Sort(testCards, out var notSortableCard, 3, 4, ColorConstants.UsedColors.Length, 1,
                13);

            Assert.Pass();
        }

        private static List<List<T>> ArrayToList<T>(T[][] array2D)
        {
            var list = new List<List<T>>();

            for (int i = 0; i < array2D.Length; i++)
            {
                list.Add(new List<T>());
                for (int j = 0; j < array2D[i].Length; j++)
                {
                    list[i].Add(array2D[i][j]);
                }
            }

            return list;
        }

        private static bool IsCardListEqual(List<NumericColoredCard> list1, List<NumericColoredCard> list2,
            bool isCheckOrder)
        {
            if (list1.Count != list2.Count)
                return false;

            if (!isCheckOrder)
            {
                list1 = list1.OrderBy(p => p.Id).ToList();
                list2 = list2.OrderBy(p => p.Id).ToList();
            }

            for (int i = 0; i < list1.Count; i++)
            {
                if (!list1[i].Equals(list2[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}