using System;
using CardGame.Core.Comparer;

namespace CardGame.Core.Sort
{
    public static class SortUtilities
    {
        public static void SortByCardColor(NumericColoredCard[] cards)
        {
            Array.Sort(cards, new ColorComparer());
        }

        public static void SortByCardId(NumericColoredCard[] cards)
        {
            Array.Sort(cards, new IdComparer());
        }

        public static void SortByCardNo(NumericColoredCard[] cards)
        {
            Array.Sort(cards, new NumberComparer());
        }

        public static JokerCard[] SplitJokerCard(NumericColoredCard[] cards,
            out NumericColoredCard[] otherCards)
        {
            int jokerCount = 0;
            int numericCardCount = 0;
            for (int i = 0; i < cards.Length; i++)
            {
                if (cards[i] is JokerCard)
                {
                    jokerCount++;
                }
                else
                {
                    numericCardCount++;
                }
            }

            int tempJokerIndex = 0;
            int tempNumericIndex = 0;
            var jokerCards = new JokerCard[jokerCount];
            var numericCards = new NumericColoredCard[numericCardCount];
            for (int i = 0; i < cards.Length; i++)
            {
                if (cards[i] is JokerCard)
                {
                    jokerCards[tempJokerIndex] = (JokerCard)cards[i];
                    tempJokerIndex++;
                }
                else
                {
                    numericCards[tempNumericIndex] = cards[i];
                    tempNumericIndex++;
                }
            }

            otherCards = numericCards;
            return jokerCards;
        }

        public static NumericColoredCard[] AddJokerCardsToNumericCardsArray(NumericColoredCard[] cards,
            JokerCard[] jokerCards)
        {
            int arrayLenght = cards.Length + jokerCards.Length;
            var mergedArray = new NumericColoredCard[arrayLenght];

            for (int i = 0; i < cards.Length; i++)
            {
                mergedArray[i] = cards[i];
            }

            var tempIndex = cards.Length;
            for (int i = 0; i < jokerCards.Length; i++)
            {
                mergedArray[tempIndex + i] = jokerCards[i];
            }

            return mergedArray;
        }
    }
}