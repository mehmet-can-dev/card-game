using System;
using UnityEngine;

public class DeckBuilder
{
    public int DeckCount { get; private set; }
    public int NumericCardCountPerDeck { get; private set; }

    public Color[] Colors { get; private set; }

    public DeckBuilder(int deckCount, int cardPerDeck, Color[] colors)
    {
        if (cardPerDeck % colors.Length != 0)
            throw new ArgumentException("Numeric Card Count must be divided Colors Lenght");

        this.DeckCount = deckCount;
        this.NumericCardCountPerDeck = cardPerDeck;
        this.Colors = colors;
    }

    public Deck[] Build()
    {
        Deck[] decks = new Deck[DeckCount];

        var uniqCreatedCardCount = 0;
        for (int i = 0; i < DeckCount; i++)
        {
            NumericColoredCard[] cards = new NumericColoredCard[NumericCardCountPerDeck];

            var usedColorsLenght = Colors.Length;

            var sameColorCardLenght = NumericCardCountPerDeck / usedColorsLenght;

            var createdCardCount = 0;

            for (int j = 0; j < usedColorsLenght; j++)
            {
                var cardNo = 0;
                for (int k = 0; k < sameColorCardLenght; k++)
                {
                    var numericCard = new NumericColoredCard(uniqCreatedCardCount, cardNo, Colors[j]);
                    cards[createdCardCount] = numericCard;

                    uniqCreatedCardCount++;
                    cardNo++;
                    createdCardCount++;
                }
            }

            var deck = new Deck(cards);
            decks[i] = deck;
        }

        return decks;
    }

    public Deck MergeDeck(Deck[] decks)
    {
        var totalLength = 0;
        for (int i = 0; i < decks.Length; i++)
        {
            totalLength += decks[i].Cards.Length;
        }

        Debug.Log(totalLength);
        var cards = new NumericColoredCard[totalLength];

        for (int index = 0, i = 0; i < decks.Length; i++)
        {
            for (int j = 0; j < decks[i].Cards.Length; j++)
            {
                cards[index] = decks[i].Cards[j];
                index++;
            }
        }

        return new Deck(cards);
    }
}