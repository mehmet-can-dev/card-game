using System;
using System.Collections;
using System.Collections.Generic;
using CardGame.Core;
using CardGame.Core.Sort;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Color = CardGame.Core.Color;

public class DeckTest
{
    public int deckCount = 2;
    public int cardPerDeck = 52;
    public int jokerCount = 4;
    public int deckDrawCount = 25;

    public Color[] colors = ColorLogic.UsedColors;


    [Test]
    public void DeckBuildTest()
    {
        var builder = new DeckBuilder(deckCount, cardPerDeck, colors);

        List<int> checkIdUniqList = new List<int>();
        Dictionary<int, int> checkNoCountContainer = new Dictionary<int, int>();
        Dictionary<Color, int> checkColorCountContainer = new Dictionary<Color, int>();

        var decks = builder.Build();
        var mergedDeck = DeckBuilder.MergeDeck(decks);

        for (int i = 0; i < mergedDeck.Cards.Length; i++)
        {
            var id = mergedDeck.Cards[i].Id;
            if (checkIdUniqList.Contains(id))
            {
                Assert.Fail("Cards ids not uniq");
                return;
            }

            var no = mergedDeck.Cards[i].No;

            if (checkNoCountContainer.ContainsKey(no))
            {
                checkNoCountContainer[no]++;
                if (checkNoCountContainer[no] > deckCount * colors.Length)
                {
                    Assert.Fail("Cards no's not equal deck count");
                    return;
                }
            }
            else
            {
                checkNoCountContainer.Add(no, 1);
            }
            
            var color = mergedDeck.Cards[i].Color;

            if (checkColorCountContainer.ContainsKey(color))
            {
                checkColorCountContainer[color]++;
                if (checkColorCountContainer[color] > deckCount * cardPerDeck/colors.Length)
                {
                    Assert.Fail("Cards colors not equal deck count");
                    return;
                }
            }
            else
            {
                checkColorCountContainer.Add(color, 1);
            }
            
        }

        Assert.Pass();
    }

    [Test]
    public void DeckMergeTest()
    {
        var builder = new DeckBuilder(deckCount, cardPerDeck, colors);
        var decks = builder.Build();
        var mergedDeck = DeckBuilder.MergeDeck(decks);
        Assert.AreEqual(mergedDeck.MaxCount, deckCount * cardPerDeck);
    }

    [Test]
    public void DeckShuffleTest()
    {
        var builder = new DeckBuilder(deckCount, cardPerDeck, colors);
        var decks = builder.Build();
        var mergedDeck1 = DeckBuilder.MergeDeck(decks);
        var mergedDeck2 = DeckBuilder.MergeDeck(decks);

        mergedDeck1.Shuffle();
        Assert.False(ArraysEqual(mergedDeck1.Cards, mergedDeck2.Cards));
    }

    [Test]
    public void DeckJokerConvertTest()
    {
        var builder = new DeckBuilder(deckCount, cardPerDeck, colors);
        var decks = builder.Build();
        var mergedDeck = DeckBuilder.MergeDeck(decks);

        mergedDeck.TurnCardToJokerCards(jokerCount);

        int deckJokerCount = 0;
        for (int i = 0; i < mergedDeck.Cards.Length; i++)
        {
            if (mergedDeck.Cards[i] is JokerCard)
            {
                deckJokerCount++;
            }
        }

        Assert.AreEqual(jokerCount, deckJokerCount);
    }

    [Test]
    public void DeckDrawTest()
    {
        var builder = new DeckBuilder(deckCount, cardPerDeck, colors);
        var decks = builder.Build();
        var mergedDeck = DeckBuilder.MergeDeck(decks);

        mergedDeck.TurnCardToJokerCards(jokerCount);

        int drawCardCount = 0;

        for (int i = 0; i < deckDrawCount; i++)
        {
            mergedDeck.DrawCard();
            drawCardCount++;
        }

        Assert.AreEqual(drawCardCount, mergedDeck.MaxCount - mergedDeck.CurrentCardCount);
    }


    private static bool ArraysEqual<T>(T[] a1, T[] a2)
    {
        if (ReferenceEquals(a1, a2))
            return true;

        if (a1 == null || a2 == null)
            return false;

        if (a1.Length != a2.Length)
            return false;

        var comparer = EqualityComparer<T>.Default;
        for (int i = 0; i < a1.Length; i++)
        {
            if (!comparer.Equals(a1[i], a2[i])) return false;
        }

        return true;
    }
}