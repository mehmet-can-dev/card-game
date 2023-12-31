﻿using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace CardGame.View
{
    public class DeckViewAnimationModule : MonoBehaviour
    {
        [Header("Project References")] [SerializeField]
        private DeckViewDealHandAnimationSO deckViewDealHandAnimation;

        public void Init()
        {
        }

        public IEnumerator CardSpawnAnimation(CardViewBase cardViewBase, Vector3 targetPosition)
        {
            cardViewBase.transform.DORotateQuaternion(Quaternion.identity, deckViewDealHandAnimation.flipDuration);
            yield return cardViewBase.transform
                .DOJump(targetPosition, deckViewDealHandAnimation.jumpPower, 1, deckViewDealHandAnimation.jumpDuration)
                .SetEase(deckViewDealHandAnimation.jumpCurve).WaitForCompletion();
        }
    }
}