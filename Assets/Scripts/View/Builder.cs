using System.Collections;
using System.Collections.Generic;
using CardGame.Core;
using CardGame.Core.Sort;
using CardGame.View.DataModels;
using UnityEngine;
using UnityEngine.Serialization;
using Color = UnityEngine.Color;

namespace CardGame.View
{
    public class Builder : MonoBehaviour
    {
        [SerializeField] private DeckViewBase deckViewBase;
        [SerializeField] private HandViewBase handViewBase;

        [SerializeField] private BuilderSettingsSO builderSettingsSo;


        void Start()
        {
            RandomLogic.AssignRandomSeed();

            var mergedDeck = BuildDeck(builderSettingsSo.BuilderCountData);

            deckViewBase.Init(mergedDeck, builderSettingsSo.BuilderViewData.deckColor);

            var hand = new Hand(builderSettingsSo.BuilderCountData.handCount);

            handViewBase.Init(hand);
        }

        public void SortHandByNumeric()
        {
            handViewBase.SortHandByNumeric();
        }

        public void SortHandByColor()
        {
            handViewBase.SortHandByColored();
        }

        public void DealHand()
        {
            StartCoroutine(DealHandAnimation());
        }

        private IEnumerator DealHandAnimation()
        {
            for (int i = 0; i < handViewBase.Hand.MaxCount; i++)
            {
                var spawnedCard = deckViewBase.DrawCard();
                var connectTile = handViewBase.AddCardToTile(spawnedCard);
                StartCoroutine(deckViewBase.DeckToPlayerHandAnimation(handViewBase, spawnedCard, connectTile));
                yield return null;
            }
        }


        private Deck BuildDeck(BuilderCountData builderCountData)
        {
            var builder = new DeckBuilder(builderCountData.deckCount, builderCountData.cardPerDeck,
                ColorLogic.UsedColors);

            var decks = builder.Build();

            var mergedDeck = DeckBuilder.MergeDeck(decks);

            mergedDeck.Shuffle();
            mergedDeck.TurnCardToJokerCards(builderCountData.jokerCount);
            mergedDeck.Shuffle();
            return mergedDeck;
        }
    }
}