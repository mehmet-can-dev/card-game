using System;
using CardGame.Core;
using CardGame.Core.Sort;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class SortTest
    {
        [Test]
        public void SortNumericTest()
        {
            NumericColoredCard[] testCards = new NumericColoredCard[]
            {
                new NumericColoredCard(0, 1, ColorLogic.red),
                new NumericColoredCard(1, 2, ColorLogic.red),
                new NumericColoredCard(2, 3, ColorLogic.red),
                //new NumericColoredCard(3, 3, ColorLogic.red),
                new NumericColoredCard(4, 4, ColorLogic.red),
                new NumericColoredCard(5, 5, ColorLogic.red),
                new NumericColoredCard(6, 7, ColorLogic.red),
                new NumericColoredCard(7, 8, ColorLogic.red),
                new NumericColoredCard(8, 9, ColorLogic.red),
                new NumericColoredCard(9, 10, ColorLogic.red),
                new NumericColoredCard(10, 12, ColorLogic.red),
                new NumericColoredCard(11, 13, ColorLogic.red),
                new NumericColoredCard(12, 15, ColorLogic.red),
                new NumericColoredCard(13, 16, ColorLogic.red),
                new NumericColoredCard(14, 17, ColorLogic.red)
            };

            // mergedDeck.Shuffle();
            var hand = new Hand(testCards.Length);

            LogAssert.Expect(LogType.Exception, "Exception");

            for (int i = 0; i < testCards.Length; i++)
            {
                hand.AddCard(testCards[i]);
            }

            var cards = NumericSortLogic.SortByNumeric(hand.Cards, out var notSortableCard, 3);

            for (int i = 0; i < cards.Length; i++)
            {
                for (int j = 0; j < cards[i].Length; j++)
                {
                    Debug.LogException(new Exception(cards[i][j].ToStringBuilder().ToString()));
                }
            }

            Assert.Pass();
            //Assert.AreEqual(mergedDeck.MaxCount, deckCount * cardPerDeck);
        }
    }
}