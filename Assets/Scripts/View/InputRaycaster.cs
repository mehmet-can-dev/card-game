using System;
using UnityEngine;

namespace CardGame.View
{
    public class InputRaycaster : MonoBehaviour
    {
        [SerializeField] private Camera camera;

        private bool isPressed = false;
        private IInteractable iInteractable;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit))
                {
                    if (hit.collider.TryGetComponent(out IInteractable interactable))
                    {
                        iInteractable = interactable;
                        var worldPos = camera.ScreenToWorldPoint(Input.mousePosition);
                        interactable.OnInteractStarted(worldPos);
                        isPressed = true;
                    }
                }
            }

            if (isPressed)
            {
                if (iInteractable != null)
                {
                    var worldPos = camera.ScreenToWorldPoint(Input.mousePosition);
                    iInteractable.OnDrag(worldPos);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (iInteractable != null)
                {
                    var worldPos = camera.ScreenToWorldPoint(Input.mousePosition);
                    iInteractable.OnInteractEnded(worldPos);
                    iInteractable = null;
                }

                isPressed = false;
            }
        }
    }
}