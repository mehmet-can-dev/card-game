using UnityEngine;

namespace CardGame.View.DebugSystem
{
    public class DebugCanvas : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private DebugBehaviour debugBehaviour;

        private void Start()
        {
            UiDebugFunctions.CreateDebugUIButton(canvas, debugBehaviour.DealHand, 0, "Deal Hands");
            UiDebugFunctions.CreateDebugUIButton(canvas, debugBehaviour.ClearHand, 1, "Clear Hands");
            UiDebugFunctions.CreateDebugUIButton(canvas, () =>
            {
                debugBehaviour.SetSortLogicNumeric();
                debugBehaviour.Sort();
            }, 2, "Numeric Sort");
            UiDebugFunctions.CreateDebugUIButton(canvas, () =>
            {
                debugBehaviour.SetSortLogicColored();
                debugBehaviour.Sort();
            }, 3, "Colored Sort");
            UiDebugFunctions.CreateDebugUIButton(canvas, () =>
            {
                debugBehaviour.SetSortLogicSmart();
                debugBehaviour.Sort();
            }, 4, "Smart Sort");
        }
    }
}