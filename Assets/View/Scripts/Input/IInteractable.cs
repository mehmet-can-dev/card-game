﻿using UnityEngine;

namespace CardGame.View
{
    public interface IInteractable
    {
        public void OnInteractStarted(Vector3 pos);

        public void OnDrag(Vector3 pos);

        public void OnInteractEnded(Vector3 pos);
    }
}