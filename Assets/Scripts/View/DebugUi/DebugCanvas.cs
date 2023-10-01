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
            UiDebugFunctions.CreateDebugUIButton(canvas, builder.SortHandByColor, 1, "Numeric Sort");
        }
    }
}