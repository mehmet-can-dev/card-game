using System;
using System.Collections.Generic;
using CardGame.Core;
using CardGame.Core.Sort;
using CardGame.View.DataModels;
using UnityEngine;
using Color = UnityEngine.Color;

namespace CardGame.View
{
    public class HandViewBase : MonoBehaviour
    {
        [Header("Module References")] [SerializeField]
        private HandViewGridModule handViewGridModule;

        private Hand hand;
        public Hand Hand => hand;

        public void Init(Hand hand)
        {
            this.hand = hand;
            handViewGridModule.Init();
        }

        public Tile AddCardToTile(CardViewBase cardViewBase)
        {
            var tile = handViewGridModule.GetEmptyTile();
            hand.AddCard(cardViewBase.Card);
            handViewGridModule.ConnectCardToTile(tile, cardViewBase);
            return tile;
        }

        public CardViewBase RemoveCardFromTile()
        {
            var card = hand.DrawCard();
            var tile = handViewGridModule.GetConnectedTile(card);
            var cardView = tile.GetConnectedCard;
            handViewGridModule.RemoveConnectionCardFromTile(card, tile);
            return cardView;
        }

        public void SortHandByNumeric(SortViewData sortViewData)
        {
            var cards = NumericSortLogic.SortByNumeric(hand.Cards, out var notSortableCards, sortViewData.min);
            handViewGridModule.ReAssignCards(cards, notSortableCards);
        }

        public void SortHandByColored(SortViewData sortViewData)
        {
            var cards = ColoredSortLogic.SortByColored(hand.Cards, sortViewData.min, sortViewData.max,
                out var notSortableCards);
            handViewGridModule.ReAssignCards(cards, notSortableCards);
        }

        public void SortHandBySmart(SortViewData sortViewData)
        {
            var cards = SmartSortLogic.SortBySmart(hand.Cards, out var notSortableCards, sortViewData.min,
                ColorLogic.UsedColors.Length, sortViewData.maxNumberPerDeck);
            handViewGridModule.ReAssignCards(cards, notSortableCards);
        }
    }
}