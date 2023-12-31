﻿using System;
using System.Text;

namespace CardGame.Core
{
    public class Deck
    {
        public NumericColoredCard[] Cards { get; private set; }
        private int maxCount = 0;
        private int firstCardIndex = 0;
        public int MaxCount => maxCount;
        public int CurrentCardCount => maxCount - firstCardIndex;

        public Deck(NumericColoredCard[] cards)
        {
            Cards = cards;
            maxCount = cards.Length;
            firstCardIndex = 0;
        }

        public NumericColoredCard DrawCard()
        {
            if (firstCardIndex >= maxCount)
            {
                throw new IndexOutOfRangeException("Deck Empty");
            }

            var c = Cards[firstCardIndex];
            Cards[firstCardIndex] = null;
            firstCardIndex = firstCardIndex + 1;
            return c;
        }

        public void AddCard(NumericColoredCard card)
        {
            if (firstCardIndex - 1 < 0)
            {
                throw new IndexOutOfRangeException("Deck Full");
            }

            Cards[firstCardIndex - 1] = card;
            firstCardIndex = firstCardIndex - 1;
        }

        public void TurnCardToJokerCards(int jokerCount)
        {
            for (int i = 0; i < jokerCount; i++)
            {
                var c = PeekCard(i);
                var j = new JokerCard(c.Id, c.No, c.Color);
                ReplaceCardWithJokerCard(i, j);
            }
        }

        private NumericColoredCard PeekCard(int index)
        {
            if (index < firstCardIndex && index >= maxCount)
            {
                throw new IndexOutOfRangeException("Index Out Of Deck Range");
            }

            var c = Cards[index];
            return c;
        }

        private void ReplaceCardWithJokerCard(int index, JokerCard card)
        {
            if (index < firstCardIndex && index >= maxCount)
            {
                throw new IndexOutOfRangeException("Index Out Of Deck Range");
            }

            Cards[index] = card;
        }

        public StringBuilder ToStringBuilder()
        {
            var sb = new StringBuilder();

            for (int i = firstCardIndex; i < Cards.Length; i++)
            {
                sb.Append(Cards[i].ToStringBuilder());
                sb.Append("type");
                sb.Append(Cards[i].GetType());
                sb.AppendLine();
            }

            return sb;
        }

        public void Shuffle()
        {
            Cards.Shuffle(firstCardIndex);
        }
    }
}