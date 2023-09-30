using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class Builder : MonoBehaviour
{
    void Start()
    {
        RandomLogic.AssignRandomSeed();

        var builder = new DeckBuilder(2, 52, ColorLogic.UsedColors);

        var decks = builder.Build();

        var mergedDeck = builder.MergeDeck(decks);

        mergedDeck.Cards.Shuffle();

        CreateJokerCards(4, mergedDeck);

        mergedDeck.Cards.Shuffle();

        var hand = new Hand(14);

        for (int i = 0; i < hand.MaxCount; i++)
        {
            var c = mergedDeck.DrawCard();
            hand.AddCard(c);
        }

        Debug.Log(hand.ToStringBuilder());
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