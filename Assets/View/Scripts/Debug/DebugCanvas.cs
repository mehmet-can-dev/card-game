using UnityEngine;

namespace CardGame.View.DebugSystem
{
    public class DebugCanvas : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private DebugBehaviour debugBehaviour;

        private void Start()
        {
            UiDebugFunctions.CreateDebugUIButtonLeft(canvas, debugBehaviour.DealHand, 0, "Deal Hands");
            UiDebugFunctions.CreateDebugUIButtonLeft(canvas, debugBehaviour.ClearHand, 1, "Clear Hands");
            
            UiDebugFunctions.CreateDebugUIButtonLeft(canvas, () =>
            {
                debugBehaviour.SetSortLogicForwardNumeric();
                debugBehaviour.Sort();
            }, 3, "Forward Numeric Sort");
            UiDebugFunctions.CreateDebugUIButtonLeft(canvas, () =>
            {
                debugBehaviour.SetSortLogicForwardColored();
                debugBehaviour.Sort();
            }, 4, "Forward Colored Sort");
            
            
            UiDebugFunctions.CreateDebugUIButtonRight(canvas, () =>
            {
                debugBehaviour.SetSortLogicRecursiveNumeric();
                debugBehaviour.Sort();
            }, 0, "Smart Numeric Sort");

            UiDebugFunctions.CreateDebugUIButtonRight(canvas, () =>
            {
                debugBehaviour.SetSortLogicRecursiveColored();
                debugBehaviour.Sort();
            }, 1, "Smart Colored Sort");
            
            UiDebugFunctions.CreateDebugUIButtonRight(canvas, () =>
            {
                debugBehaviour.SetSortLogicRecursiveSmart();
                debugBehaviour.Sort();
            }, 2, "Smart Sort");
            
         
          
        }
    }
}