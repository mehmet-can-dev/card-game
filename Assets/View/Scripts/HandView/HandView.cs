using System;
using CardGame.Core;
using CardGame.Core.Sort;
using CardGame.View.Card;
using CardGame.View.DataModels;
using UnityEngine;

namespace CardGame.View.Hand
{
    public class HandView : MonoBehaviour
    {
        [Header("Module References")] [SerializeField]
        private HandViewGridModule handViewGridModule;

        private Core.Hand hand;
        public Core.Hand Hand => hand;

        public void Init(Core.Hand hand)
        {
            this.hand = hand;
            handViewGridModule.Init();
        }

        public Tile AddCardToTile(CardView cardView)
        {
            var tile = handViewGridModule.GetEmptyTile();
            hand.AddCard(cardView.Card);
            handViewGridModule.ConnectCardToTile(tile, cardView);
            return tile;
        }

        public CardView RemoveCardFromTile()
        {
            var card = hand.DrawCard();
            var tile = handViewGridModule.GetConnectedTile(card);
            var cardView = tile.GetConnectedCard;
            handViewGridModule.RemoveConnectionCardFromTile(card, tile);
            return cardView;
        }

        public void SortHandByNumeric(SortViewData sortViewData, Action onComplete)
        {
            var cards = NumericSortLogic.SortByNumeric(hand.Cards, out var notSortableCards, sortViewData.min);
            handViewGridModule.ReAssignCards(cards, notSortableCards, onComplete);
        }

        public void SortHandByColored(SortViewData sortViewData, Action onComplete)
        {
            var cards = ColoredSortLogic.SortByColored(hand.Cards, sortViewData.min, sortViewData.max,
                out var notSortableCards);
            handViewGridModule.ReAssignCards(cards, notSortableCards, onComplete);
        }

        public void SortHandBySmart(SortViewData sortViewData, Action onComplete)
        {
            var cards = SmartSortLogic.SortBySmart(hand.Cards, out var notSortableCards, sortViewData.min,
                ColorConstants.UsedColors.Length, sortViewData.maxNumberPerDeck);
            handViewGridModule.ReAssignCards(cards, notSortableCards, onComplete);
        }
    }
}