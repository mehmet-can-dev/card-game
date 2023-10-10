using UnityEngine;

namespace CardGame.View.InputSystem
{
    public interface IInteractable
    {
        public void OnInteractStarted(Vector3 pos);

        public void OnDrag(Vector3 pos);

        public void OnInteractEnded();
    }
}