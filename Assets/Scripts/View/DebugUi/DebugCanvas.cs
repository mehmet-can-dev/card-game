using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace CardGame.View.DebugUi
{
    public class DebugCanvas : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private BuilderViewController builderViewController;

        private void Start()
        {
            UiDebugFunctions.CreateDebugUIButton(canvas, builderViewController.DealHand, 0, "Deal Hands");
            UiDebugFunctions.CreateDebugUIButton(canvas, builderViewController.ClearHand, 1, "Clear Hands");
            UiDebugFunctions.CreateDebugUIButton(canvas, builderViewController.SortHandByNumeric, 2, "Numeric Sort");
            UiDebugFunctions.CreateDebugUIButton(canvas, builderViewController.SortHandByColor, 3, "Colored Sort");
        }
    }
}