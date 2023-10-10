using UnityEngine;

namespace CardGame.View.InputSystem
{
    public class InputRaycaster : MonoBehaviour
    {
        [Header("Scene References")]
        [SerializeField] private Camera mainCamera;

        private bool isPressed = false;
        private IInteractable iInteractable;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out var hit))
                {
                    if (hit.collider.TryGetComponent(out IInteractable interactable))
                    {
                        iInteractable = interactable;
                        var worldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                        interactable.OnInteractStarted(worldPos);
                        isPressed = true;
                    }
                }
            }

            if (isPressed)
            {
                if (iInteractable != null)
                {
                    var worldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    iInteractable.OnDrag(worldPos);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (iInteractable != null)
                {
                    var worldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    iInteractable.OnInteractEnded(worldPos);
                    iInteractable = null;
                }

                isPressed = false;
            }
        }
    }
}