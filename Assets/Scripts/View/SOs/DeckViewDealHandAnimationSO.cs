using UnityEngine;

namespace CardGame.View
{
    [CreateAssetMenu(fileName = "DeckViewDealHandAnimation", menuName = "CardGame/Animation/DeckDealAnimationSettings", order = 0)]
    public class DeckViewDealHandAnimationSO : ScriptableObject
    {
        public float flipDuration = 0.5f;
        public float jumpPower = 1;
        public float jumpDuration = 1;
        public AnimationCurve jumpCurve ;
    }
}