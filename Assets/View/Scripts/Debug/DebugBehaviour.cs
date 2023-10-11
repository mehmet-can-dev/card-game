using System;
using System.Collections;
using CardGame.Core.Sort;
using CardGame.Core.Sort.Forward;
using CardGame.Core.Sort.Recursive;
using CardGame.View.Deck;
using CardGame.View.Hand;
using CardGame.View.SO;
using UnityEngine;

namespace CardGame.View.DebugSystem
{
    public class DebugBehaviour : MonoBehaviour
    {
        private Core.Hand hand;
        private HandView handView;
        private DeckView deckView;

        private BuilderViewController builderViewController;

        private bool isAnimationPlaying = false;

        public void Init(BuilderViewController viewController, Core.Hand hand, HandView handView,
            DeckView deckView)
        {
            this.hand = hand;
            this.handView = handView;
            this.deckView = deckView;
            builderViewController = viewController;
        }

        public void SetSortLogicNumeric()
        {
            builderViewController.SetNewSorter(new NumericForwardSort());
        }

        public void SetSortLogicColored()
        {
            builderViewController.SetNewSorter(new ColoredForwardSort());
        }

        public void SetSortLogicSmart()
        {
            builderViewController.SetNewSorter(new SmartSort());
        }

        public void Sort()
        {
            if (isAnimationPlaying)
                return;
            if (hand.IsEmpty())
                return;
            isAnimationPlaying = true;

            builderViewController.SortHand(ResetAnimationPLaying);
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
            int count = handView.Hand.MaxCount;
            for (int i = 0; i < count; i++)
            {
                var spawnedCard = deckView.DrawCard();
                var connectTile = handView.AddCardToTile(spawnedCard);
                if (i != count - 1)
                    StartCoroutine(
                        deckView.DeckViewAnimationModuleModule.DeckToPlayerHandAnimation(spawnedCard, connectTile));
                else
                    yield return StartCoroutine(
                        deckView.DeckViewAnimationModuleModule.DeckToPlayerHandAnimation(spawnedCard, connectTile));

                yield return null;
            }

            onComplete?.Invoke();
        }

        // Can move another script for animations
        private IEnumerator ClearHandAnimation(Action onComplete)
        {
            var handCount = handView.Hand.CurrentCardCount;
            for (int i = 0; i < handCount; i++)
            {
                var spawnedCard = handView.RemoveCardFromTile();
                var numericCard = spawnedCard.Card;
                deckView.AddCard(numericCard);
                if (i != handCount - 1)
                    spawnedCard.MoveTargetPosition(deckView.transform.position + Vector3.forward,
                        () => { spawnedCard.DestroyCard(); });
                else
                    spawnedCard.MoveTargetPosition(deckView.transform.position + Vector3.forward,
                        () =>
                        {
                            spawnedCard.DestroyCard();
                            onComplete?.Invoke();
                        });


                yield return null;
            }

            deckView.Shuffle();
        }
    }
}