﻿using UnityEngine;

namespace CardGame.View.InputSystem
{
    public class InputRaycaster : MonoBehaviour
    {
        [Header("Scene References")] [SerializeField]
        private Camera mainCamera;

        private bool isPressed = false;
        private IInteractable iInteractable;

        private Vector3 inputDelta;
        private Vector3 previousFrameInputPosition;

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
                        previousFrameInputPosition = Input.mousePosition;
                        isPressed = true;
                    }
                }
            }
            else if (isPressed)
            {
                if (iInteractable != null)
                {
                    if (inputDelta.sqrMagnitude != 0)
                    {
                        var worldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                        iInteractable.OnDrag(worldPos);
                    }

                    inputDelta = Input.mousePosition - previousFrameInputPosition;
                    previousFrameInputPosition = Input.mousePosition;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (iInteractable != null)
                {
                    iInteractable.OnInteractEnded();
                    iInteractable = null;
                }

                isPressed = false;
            }
        }
    }
}