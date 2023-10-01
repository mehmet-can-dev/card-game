using System;
using CardGame.Core;
using TMPro;
using UnityEngine;
using Color = UnityEngine.Color;

namespace CardGame.View
{
    public class CardViewBase : MonoBehaviour
    {
        [SerializeField] private CardViewAnimation cardViewAnimation;

        [SerializeField] private ColorSetterUseByProperty frontColorSetter;
        [SerializeField] private ColorSetterUseByProperty backColorSetter;
        [SerializeField] private TextMeshProUGUI cardNoText;

        private NumericColoredCard card;

        public NumericColoredCard Card => card;

        public void Init(NumericColoredCard card, Color backColor)
        {
            this.card = card;
            frontColorSetter.SetColor(Utilities.ColorUtilities.CoreColorToUnityColor(card.Color));
            backColorSetter.SetColor(backColor);
            cardNoText.SetText(card.No.ToString());

            cardViewAnimation.Init();
        }

        public void MoveTargetPosition(Vector3 targetPos, Action onComplete)
        {
            StartCoroutine(cardViewAnimation.MovePosition(targetPos, onComplete));
        }
    }
}