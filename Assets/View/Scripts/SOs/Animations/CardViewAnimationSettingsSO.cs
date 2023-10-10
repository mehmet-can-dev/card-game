using UnityEngine;

namespace CardGame.View.SO.Animations
{
    [CreateAssetMenu(fileName = "CardViewAnimationSettings", menuName = "CardGame/Animation/CardAnimationSettings", order = 0)]
    public class CardViewAnimationSettingsSO : ScriptableObject
    {
        public float duration;
        public AnimationCurve curve;
    }
}