using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace CardGame.View
{
    public class CardViewAnimation : MonoBehaviour
    {
        public void Init()
        {
        }

        public IEnumerator MovePosition(Vector3 pos, Action onComplete)
        {
            yield return transform.DOMove(pos, 1).SetEase(Ease.OutQuad).WaitForCompletion();
            onComplete?.Invoke();
        }
    }
}