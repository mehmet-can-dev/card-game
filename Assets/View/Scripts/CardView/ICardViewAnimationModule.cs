using System;
using System.Collections;
using UnityEngine;

namespace CardGame.View.Card
{
    public interface ICardViewAnimationModule
    {
        public IEnumerator MovePosition(Vector3 pos, Action onComplete);
    }
}