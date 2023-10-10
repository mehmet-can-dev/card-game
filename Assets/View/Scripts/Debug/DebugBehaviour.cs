using System;
using System.Collections;
using CardGame.Core;
using UnityEngine;

namespace CardGame.View.DebugUi
{
    public class DebugBehaviour : MonoBehaviour
    {
        private Hand hand;
        private Deck deck;
        private HandViewBase handViewBase;
        private DeckViewBase deckViewBase;

        private BuilderSettingsSO builderSettingsSo;

        private bool isAnimationPlaying = false;

        public void Init(BuilderSettingsSO builderSettingsSo, Hand hand, Deck deck, HandViewBase handViewBase,
            DeckViewBase deckViewBase)
        {
            this.hand = hand;
            this.deck = deck;
            this.handViewBase = handViewBase;
            this.deckViewBase = deckViewBase;
            this.builderSettingsSo = builderSettingsSo;
        }

        public void SortHandByNumeric()
        {
            if (isAnimationPlaying)
                return;
            if (hand.IsEmpty())
                return;
            isAnimationPlaying = true;

            handViewBase.SortHandByNumeric(builderSettingsSo.SortViewData, ResetAnimationPLaying);
        }

        public void SortHandByColor()
        {
            if (isAnimationPlaying)
                return;
            if (hand.IsEmpty())
                return;
            isAnimationPlaying = true;

            handViewBase.SortHandByColored(builderSettingsSo.SortViewData, ResetAnimationPLaying);
        }

        public void SortHandBySmartSort()
        {
            if (isAnimationPlaying)
                return;
            if (hand.IsEmpty())
                return;
            isAnimationPlaying = true;

            handViewBase.SortHandBySmart(builderSettingsSo.SortViewData, ResetAnimationPLaying);
        }

        public void DealHand()
        {
            if (isAnimationPlaying)
                return;

            if (hand.IsFull())
                return;

            isAnimationPlaying = true;

            StartCoroutine(DealHandAnimation(ResetAnimationPLaying));
        }

        public void ClearHand()
        {
            if (isAnimationPlaying)
                return;

            if (hand.IsEmpty())
                return;

            isAnimationPlaying = true;
            StartCoroutine(ClearHandAnimation(ResetAnimationPLaying));
        }

        private void ResetAnimationPLaying()
        {
            isAnimationPlaying = false;
        }

        // Can move another script for animations
        private IEnumerator DealHandAnimation(Action onComplete)
        {
            int count = handViewBase.Hand.MaxCount;
            for (int i = 0; i < count; i++)
            {
                var spawnedCard = deckViewBase.DrawCard();
                var connectTile = handViewBase.AddCardToTile(spawnedCard);
                if (i != count - 1)
                    StartCoroutine(deckViewBase.DeckToPlayerHandAnimation(handViewBase, spawnedCard, connectTile));
                else
                    yield return StartCoroutine(
                        deckViewBase.DeckToPlayerHandAnimation(handViewBase, spawnedCard, connectTile));

                yield return null;
            }

            onComplete?.Invoke();
        }

        // Can move another script for animations
        private IEnumerator ClearHandAnimation(Action onComplete)
        {
            var handCount = handViewBase.Hand.CurrentCardCount;
            for (int i = 0; i < handCount; i++)
            {
                var spawnedCard = handViewBase.RemoveCardFromTile();
                var numericCard = spawnedCard.Card;
                deckViewBase.AddCard(numericCard);
                if (i != handCount - 1)
                    spawnedCard.MoveTargetPosition(deckViewBase.transform.position + Vector3.forward,
                        () => Destroy(spawnedCard.gameObject));
                else
                    spawnedCard.MoveTargetPosition(deckViewBase.transform.position + Vector3.forward,
                        () =>
                        {
                            Destroy(spawnedCard.gameObject);
                            onComplete?.Invoke();
                        });


                yield return null;
            }

            deckViewBase.Shuffle();
        }
    }
}