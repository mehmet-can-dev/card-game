using System;
using UnityEngine;

namespace CardGame.View.DebugUi
{
    public class DebugCanvas : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private Builder builder;

        private void Start()
        {
            UiDebugFunctions.CreateDebugUIButton(canvas, builder.DealHand, 0, "Deal Hands");
            UiDebugFunctions.CreateDebugUIButton(canvas, builder.ClearHand, 1, "Clear Hands");
            UiDebugFunctions.CreateDebugUIButton(canvas, builder.SortHandByNumeric, 2, "Numeric Sort");
            UiDebugFunctions.CreateDebugUIButton(canvas, builder.SortHandByColor, 3, "Colored Sort");
        }
    }
}