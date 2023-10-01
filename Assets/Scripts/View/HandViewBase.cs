﻿using CardGame.Core;
using UnityEngine;
using Color = UnityEngine.Color;

namespace CardGame.View
{
    public class HandViewBase : MonoBehaviour
    {
        [SerializeField] private HandViewGridModule handViewGridModule;
        [SerializeField] private Color tileColor;

        private Hand hand;

        public Hand Hand => hand;

        public void Init(Hand hand)
        {
            this.hand = hand;
            handViewGridModule.Init(tileColor);
        }

        public Tile AddCardToTile(CardViewBase cardViewBase)
        {
            var tile = handViewGridModule.GetEmptyTile();
            hand.AddCard(cardViewBase.Card);
            handViewGridModule.ConnectCardToTile(tile, cardViewBase);
            return tile;
        }
    }
}