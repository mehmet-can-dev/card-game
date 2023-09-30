using System.Collections;
using System.Collections.Generic;
using CardGame.Core;
using CardGame.Core.Sort;
using UnityEngine;
using UnityEngine.Serialization;
using Color = UnityEngine.Color;

namespace CardGame.View
{
    public class Builder : MonoBehaviour
    {
        [SerializeField] private DeckViewBase deckViewBase;
        [SerializeField] private HandViewBase handViewBase;
        [SerializeField] private Card cardPrefab;

        public Color deckColor;

        public int handCount = 20;
        public int deckCount = 2;
        public int cardPerDeck = 52;
        public int jokerCount = 4;

        void Start()
        {
            RandomLogic.AssignRandomSeed();

            var mergedDeck = BuildDeck();

            deckViewBase.Init(mergedDeck, deckColor);

            var hand = new Hand(handCount);

            handViewBase.Init(hand);

            for (int i = 0; i < hand.MaxCount; i++)
            {
                var c = mergedDeck.DrawCard();
                hand.AddCard(c);
            }

            // var sortedCards = ColoredSortLogic.SortByColored(hand.Cards, 3, 4, out var notMatches);

            // Debug.Log(notMatches.Length);
            // for (int i = 0; i < notMatches.Length; i++)
            // {
            //     Debug.Log(notMatches[i].ToStringBuilder());
            // }
            //
            // Debug.Log("");
            //
            // for (int i = 0; i < sortedCards.GetLength(0); i++)
            // {
            //     for (int j = 0; j < sortedCards[i].Length; j++)
            //     {
            //         Debug.Log(sortedCards[i][j].ToStringBuilder());
            //     }
            // }
            //
            // sortedCards = NumericSortLogic.SortByNumeric(hand.Cards);


            // Debug.Log("");
            //
            // for (int i = 0; i < sortedCards.GetLength(0); i++)
            // {
            //     for (int j = 0; j < sortedCards[i].Length; j++)
            //     {
            //         Debug.Log(sortedCards[i][j].ToStringBuilder());
            //     }
            // }
        }

        private Deck BuildDeck()
        {
            var builder = new DeckBuilder(deckCount, cardPerDeck, ColorLogic.UsedColors);

            var decks = builder.Build();

            var mergedDeck = builder.MergeDeck(decks);

            mergedDeck.Cards.Shuffle();

            mergedDeck.TurnCardToJokerCards(jokerCount);

            mergedDeck.Shuffle();
            return mergedDeck;
        }
    }
}