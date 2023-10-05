using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace CardGame.View.DebugUi
{
    public class DebugCanvas : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
         [SerializeField] private DebugBehaviour debugBehaviour;

        private void Start()
        {
            UiDebugFunctions.CreateDebugUIButton(canvas, debugBehaviour.DealHand, 0, "Deal Hands");
            UiDebugFunctions.CreateDebugUIButton(canvas, debugBehaviour.ClearHand, 1, "Clear Hands");
            UiDebugFunctions.CreateDebugUIButton(canvas, debugBehaviour.SortHandByNumeric, 2, "Numeric Sort");
            UiDebugFunctions.CreateDebugUIButton(canvas, debugBehaviour.SortHandByColor, 3, "Colored Sort");
            UiDebugFunctions.CreateDebugUIButton(canvas, debugBehaviour.SortHandBySmartSort, 4, "Smart Sort");
        }
    }
}