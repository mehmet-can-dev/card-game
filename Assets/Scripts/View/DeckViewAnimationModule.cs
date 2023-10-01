using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace CardGame.View
{
    public class DeckViewAnimationModule : MonoBehaviour
    {
        public void Init()
        {
        }

        public IEnumerator CardSpawnAnimation(CardViewBase cardViewBase, Vector3 targetPosition)
        {
            cardViewBase.transform.DORotateQuaternion(Quaternion.identity, 0.5f);
            yield return cardViewBase.transform.DOJump(targetPosition, 1, 1, 1).WaitForCompletion();
        }
    }
}