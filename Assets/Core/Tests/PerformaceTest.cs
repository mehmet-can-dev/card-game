using CardGame.Core.Sort.Forward;
using CardGame.Core.Sort.Recursive;
using NUnit.Framework;

namespace CardGame.Core.Test
{
    public class PerformaceTest
    {
        public int deckCount = 16;
        public int cardPerDeck = 52;
        public int jokerCount = 4;
        public int deckDrawCount = 250;

        public Hand GetHand()
        {
            var builder = new DeckBuilder(deckCount, cardPerDeck, ColorConstants.UsedColors);

            var decks = builder.Build();
            var mergedDeck = DeckBuilder.MergeDeck(decks);

            var hand = new Hand(deckDrawCount);
            for (int i = 0; i < deckDrawCount; i++)
            {
                hand.AddCard(mergedDeck.DrawCard());
            }

            return hand;
        }

        [Test]
        public void ForwardNumericTest()
        {
            var hand = GetHand();
            var sorter = new NumericForwardSort();
            sorter.Sort(hand.Cards, out var notSortable, 3, 4, 4, 1, 13);
        }

        [Test]
        public void ForwardColoredTest()
        {
            var hand = GetHand();
            var sorter = new ColoredForwardSort();
            sorter.Sort(hand.Cards, out var notSortable, 3, 4, 4, 1, 13);
        }

        [Test]
        public void SmartColoredTest()
        {
            var hand = GetHand();
            var sorter = new SmartColoredSort();
            sorter.Sort(hand.Cards, out var notSortable, 3, 4, 4, 1, 13);
        }

        [Test]
        public void SmartNumericTest()
        {
            var hand = GetHand();
            var sorter = new SmartNumericSort();
            sorter.Sort(hand.Cards, out var notSortable, 3, 4, 4, 1, 13);
        }
    }
}