using System;
using System.Text;

namespace Core
{
    public class Deck
    {
        public NumericColoredCard[] Cards { get; private set; }
        public int MaxCount = 0;


        public int FirstCardIndex = 0;

        public Deck(NumericColoredCard[] cards)
        {
            Cards = cards;
            MaxCount = cards.Length;
        }

        public NumericColoredCard DrawCard()
        {
            if (FirstCardIndex >= MaxCount)
            {
                throw new IndexOutOfRangeException("Deck Empty");
            }

            var c = Cards[FirstCardIndex];
            Cards[FirstCardIndex] = null;
            FirstCardIndex++;
            return c;
        }

        public NumericColoredCard AddCard(NumericColoredCard card)
        {
            if (FirstCardIndex - 1 < 0)
            {
                throw new IndexOutOfRangeException("Deck Full");
            }

            var c = Cards[FirstCardIndex - 1];
            FirstCardIndex--;
            return c;
        }

        public NumericColoredCard PeekCard(int index)
        {
            if (index < FirstCardIndex && index >= MaxCount)
            {
                throw new IndexOutOfRangeException("Index Out Of Deck Range");
            }

            var c = Cards[index];
            return c;
        }

        public void ReplaceCardWithJokerCard(int index, JokerCard card)
        {
            if (index < FirstCardIndex && index >= MaxCount)
            {
                throw new IndexOutOfRangeException("Index Out Of Deck Range");
            }

            Cards[index] = card;
        }

        public StringBuilder ToStringBuilder()
        {
            var sb = new StringBuilder();

            for (int i = 0; i < Cards.Length; i++)
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