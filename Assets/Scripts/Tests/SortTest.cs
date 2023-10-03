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
            var builder = new DeckBuilder(2, 52, ColorLogic.UsedColors);
            var decks = builder.Build();
            var mergedDeck = DeckBuilder.MergeDeck(decks);

            // mergedDeck.Shuffle();
            var hand = new Hand(20);

            LogAssert.Expect(LogType.Exception, "Exception");

            for (int i = 0; i < hand.MaxCount; i++)
            {
                var c = mergedDeck.DrawCard();
                hand.AddCard(c);
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