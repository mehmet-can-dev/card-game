using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    void Start()
    {
        var builder = new DeckBuilder(2, 52, CardUtility.UsedColors);

        var decks = builder.Build();

        // for (int i = 0; i < decks.Length; i++)
        // {
        //     for (int j = 0; j < decks[i].Cards.Length; j++)
        //     {
        //         string cardInfo = "i: " + i;
        //         cardInfo += "j: " + j;
        //         cardInfo += "id: " + decks[i].Cards[j].Id;
        //         cardInfo += "no: " + decks[i].Cards[j].No;
        //         cardInfo += "color: " + decks[i].Cards[j].Color;
        //         
        //         Debug.Log(cardInfo);
        //     }
        // }
        
        var mergedDeck = builder.MergeDeck(decks);

        for (int i = 0; i < mergedDeck.Cards.Length; i++)
        {
            string cardInfo = "index: " + i + " id: " + mergedDeck.Cards[i].Id + " no: " + mergedDeck.Cards[i].No +
                              " Color: " + mergedDeck.Cards[i].Color + " type:" +
                              mergedDeck.Cards[i].GetType();
        
               Debug.Log(cardInfo);
        }
    }
}