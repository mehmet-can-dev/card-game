using System;
using System.Text;
using UnityEngine;

namespace Core
{
    public class Hand
    {
        private NumericColoredCard[] Cards;
        private int maxCount;
        private int currentCardCount;

        private int firstCardIndex = 0;

        public int MaxCount => maxCount;

        public Hand(int maxCount)
        {
            this.maxCount = maxCount;
            currentCardCount = 0;
            firstCardIndex = this.maxCount;
            Cards = new NumericColoredCard[maxCount];
        }

        public void AddCard(NumericColoredCard card)
        {
            if (currentCardCount >= maxCount)
            {
                throw new IndexOutOfRangeException("Hand Full");
            }

            Cards[MaxCount - currentCardCount - 1] = card;
            firstCardIndex--;
            currentCardCount++;
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
    }
}