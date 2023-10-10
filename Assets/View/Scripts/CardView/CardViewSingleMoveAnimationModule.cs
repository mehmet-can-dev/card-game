using System;
using System.Collections;
using CardGame.View.SO.Animations;
using DG.Tweening;
using UnityEngine;

namespace CardGame.View.Card
{
    public class CardViewSingleMoveAnimationModule : MonoBehaviour, ICardViewAnimationModule
    {
        [Header("Project References")] [SerializeField]
        private CardViewAnimationSettingsSO animationSettingsSo;

        public IEnumerator MovePosition(Vector3 pos, Action onComplete)
        {
            yield return transform.DOMove(pos, animationSettingsSo.duration).SetEase(animationSettingsSo.curve)
                .WaitForCompletion();
            onComplete?.Invoke();
        }
    }
}