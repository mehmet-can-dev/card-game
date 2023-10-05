using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace CardGame.View
{
    public class CardViewAnimationModule : MonoBehaviour
    {
        [Header("Project References")] [SerializeField]
        private CardViewAnimationSettingsSO animationSettingsSo;

        public void Init()
        {
        }

        public IEnumerator MovePosition(Vector3 pos, Action onComplete)
        {
            yield return transform.DOMove(pos, animationSettingsSo.duration).SetEase(animationSettingsSo.curve)
                .WaitForCompletion();
            onComplete?.Invoke();
        }
    }
}