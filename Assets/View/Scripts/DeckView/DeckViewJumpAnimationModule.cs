using System.Collections;
using CardGame.View.Card;
using CardGame.View.Hand;
using CardGame.View.SO.Animations;
using CardGame.View.Utilities;
using DG.Tweening;
using UnityEngine;

namespace CardGame.View.Deck
{
    public class DeckViewJumpAnimationModule : MonoBehaviour, IDeckViewAnimationModule
    {
        [Header("Project References")] [SerializeField]
        private DeckViewDealHandAnimationSO deckViewDealHandAnimation;

        private IEnumerator CardSpawnAnimation(CardView cardView, Vector3 targetPosition)
        {
            cardView.transform.DORotateQuaternion(Quaternion.identity, deckViewDealHandAnimation.flipDuration);
            yield return cardView.transform
                .DOJump(targetPosition, deckViewDealHandAnimation.jumpPower, 1, deckViewDealHandAnimation.jumpDuration)
                .SetEase(deckViewDealHandAnimation.jumpCurve).WaitForCompletion();
        }

        public IEnumerator DeckToPlayerHandAnimation(CardView spawnedCard, Tile connectTile)
        {
            var targetPos = connectTile.transform.position + Vector3.forward * LayerConstants.CARDLAYER;
            yield return CardSpawnAnimation(spawnedCard, targetPos);
        }
    }
}