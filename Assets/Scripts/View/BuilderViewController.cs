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
    public class BuilderViewController : MonoBehaviour
    {
        [Header("Scene References")] [SerializeField]
        private DeckViewBase deckViewBase;

        [SerializeField] private HandViewBase handViewBase;

        [Header("Project References")] [SerializeField]
        private BuilderSettingsSO builderSettingsSo;

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

        public void SortHandBySmartSort()
        {
            handViewBase.SortHandBySmart();
        }

        public void DealHand()
        {
            StartCoroutine(DealHandAnimation());
        }

        public void ClearHand()
        {
            StartCoroutine(ClearHandAnimation());
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

        private IEnumerator ClearHandAnimation()
        {
            var handCount = handViewBase.Hand.CurrentCardCount;
            for (int i = 0; i < handCount; i++)
            {
                var spawnedCard = handViewBase.RemoveCardFromTile();
                var numericCard = spawnedCard.Card;
                deckViewBase.AddCard(numericCard);
                spawnedCard.MoveTargetPosition(deckViewBase.transform.position + Vector3.forward,
                    () => Destroy(spawnedCard.gameObject));

                yield return null;
            }

            deckViewBase.Shuffle();
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