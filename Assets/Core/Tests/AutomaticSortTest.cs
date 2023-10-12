using System.Collections.Generic;
using System.Linq;
using CardGame.Core.Sort.Forward;
using CardGame.Core.Sort.Recursive;
using CardGame.Core.Test.Testables;
using NUnit.Framework;
using UnityEngine;

namespace CardGame.Core.Test
{
    public class AutomaticSortTest
    {
        private List<ITestableCards> testableCards = new List<ITestableCards>()
        {
            new FourJokerTestCards(),
            new ExampleDocCards(),
            new ColoredNumericCardsNoJoker(),
        };

        [Test]
        public void SortForwardNumericTest()
        {
            for (int i = 0; i < testableCards.Count; i++)
            {
                Debug.Log("Numeric Test Start " + testableCards.GetType());
                NumericTest(testableCards[i]);
                Debug.Log("Numeric Test End " + testableCards.GetType());
            }
        }

        [Test]
        public void SortSmartTest()
        {
            for (int i = 0; i < testableCards.Count; i++)
            {
                Debug.Log("Smart Test Start " + testableCards.GetType());
                SmartTest(testableCards[i]);
                Debug.Log("Smart Test End " + testableCards.GetType());
            }
        }

        [Test]
        public void SortForwardColoredTest()
        {
            for (int i = 0; i < testableCards.Count; i++)
            {
                Debug.Log("Colored Test Start " + testableCards.GetType());
                ColoredTest(testableCards[i]);
                Debug.Log("Colored Test End " + testableCards.GetType());
            }
        }


        public static void ColoredTest(ITestableCards testableCards)
        {
            var testCards = testableCards.GetNotSortedCards();

            var sorter = new ColoredForwardSort();

            var cards = sorter.Sort(testCards, out var notSortableCard, 3, 4, ColorConstants.UsedColors.Length,
                1, 13);

            var targetCards = testableCards.GetForwardColoredSortedCards(out var targetNotSortable);

            AssertAndLogCards(cards, notSortableCard, targetCards, targetNotSortable);
        }

        public static void SmartTest(ITestableCards testableCards)
        {
            var testCards = testableCards.GetNotSortedCards();

            var sorter = new SmartRecursiveSort();

            var cards = sorter.Sort(testCards, out var notSortableCard, 3, 4, ColorConstants.UsedColors.Length,
                1, 13);

            var targetCards = testableCards.GetSmartSortedCards(out var targetNotSortable);

            AssertAndLogCards(cards, notSortableCard, targetCards, targetNotSortable);
        }

        public static void NumericTest(ITestableCards testableCards)
        {
            var testCards = testableCards.GetNotSortedCards();

            var sorter = new NumericForwardSort();


            var cards = sorter.Sort(testCards, out var notSortableCard, 3, 4, ColorConstants.UsedColors.Length, 1,
                13);

            var targetCards = testableCards.GetForwardNumericSortedCards(out var targetNotSortable);

            AssertAndLogCards(cards, notSortableCard, targetCards, targetNotSortable);


            Assert.Pass();
        }

        public static void AssertAndLogCards(NumericColoredCard[][] cards, NumericColoredCard[] notSortableCard,
            NumericColoredCard[][] targetCards, NumericColoredCard[] targetNotSortable)
        {
            var cardList = ArrayToList(cards);
            var notSortableList = notSortableCard.ToList();

            var targetCardList = ArrayToList(targetCards);
            var targetNotSortableList = targetNotSortable.ToList();

            Debug.Log("Test Sorted List");
            Debug.Log(CardLoger.Log2DimensionNumericList(cardList));

            Debug.Log("Target Sorted List");
            Debug.Log(CardLoger.Log2DimensionNumericList(targetCardList));

            Debug.Log("Test Not Sortable");
            Debug.Log(CardLoger.LogNumericList(notSortableList));
            Debug.Log("Target Not Sortable");
            Debug.Log(CardLoger.LogNumericList(targetNotSortableList));

            Assert.IsTrue(notSortableList.Count == targetNotSortableList.Count);
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