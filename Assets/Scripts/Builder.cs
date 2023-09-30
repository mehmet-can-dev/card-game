using CardGame.Core;
using CardGame.Core.Sort;
using UnityEngine;

public class Builder : MonoBehaviour
{
    public int handCount = 20;
    public int deckCount = 2;
    public int cardPerDeck = 52;
    public int jokerCount = 4;

    void Start()
    {
        RandomLogic.AssignRandomSeed();

        var builder = new DeckBuilder(deckCount, cardPerDeck, ColorLogic.UsedColors);

        var decks = builder.Build();

        var mergedDeck = builder.MergeDeck(decks);

        mergedDeck.Cards.Shuffle();

        CreateJokerCards(jokerCount, mergedDeck);

        mergedDeck.Shuffle();

        var hand = new Hand(handCount);

        for (int i = 0; i < hand.MaxCount; i++)
        {
            var c = mergedDeck.DrawCard();
            hand.AddCard(c);
        }

        var sortedCards = ColoredSortLogic.SortByColored(hand.Cards, 3, 4, out var notMatches);

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

        sortedCards = NumericSortLogic.SortByNumeric(hand.Cards);


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

    private static void CreateJokerCards(int jokerCount, Deck mergedDeck)
    {
        for (int i = 0; i < jokerCount; i++)
        {
            var c = mergedDeck.PeekCard(i);
            var j = new JokerCard(c.Id, c.No, c.Color);
            mergedDeck.ReplaceCardWithJokerCard(i, j);
        }
    }
}