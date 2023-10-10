using System.Collections;
using CardGame.View.Card;
using CardGame.View.SO.Animations;
using DG.Tweening;
using UnityEngine;

namespace CardGame.View.Deck
{
    public class DeckViewAnimationModule : MonoBehaviour
    {
        [Header("Project References")] [SerializeField]
        private DeckViewDealHandAnimationSO deckViewDealHandAnimation;

        public IEnumerator CardSpawnAnimation(CardView cardView, Vector3 targetPosition)
        {
            cardView.transform.DORotateQuaternion(Quaternion.identity, deckViewDealHandAnimation.flipDuration);
            yield return cardView.transform
                .DOJump(targetPosition, deckViewDealHandAnimation.jumpPower, 1, deckViewDealHandAnimation.jumpDuration)
                .SetEase(deckViewDealHandAnimation.jumpCurve).WaitForCompletion();
        }
    }
}