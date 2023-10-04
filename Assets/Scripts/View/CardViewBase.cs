using System;
using CardGame.Core;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Color = UnityEngine.Color;

namespace CardGame.View
{
    public class CardViewBase : MonoBehaviour
    {
        [Header("Module References")]
        [SerializeField] private CardViewAnimationModule cardViewAnimationModule;

        [Header("Child References")]
        [SerializeField] private ColorSetterUseByProperty frontColorSetter;
        [SerializeField] private ColorSetterUseByProperty backColorSetter;
        [SerializeField] private TextMeshProUGUI cardNoText;
        [SerializeField] private TextMeshProUGUI jokerText;

        private NumericColoredCard card;

        public NumericColoredCard Card => card;

        public void Init(NumericColoredCard card, Color backColor)
        {
            this.card = card;
            frontColorSetter.SetColor(Utilities.ColorUtilities.CoreColorToUnityColor(card.Color));
            backColorSetter.SetColor(backColor);
            cardNoText.SetText(card.No.ToString());

            cardViewAnimationModule.Init();

            if (card is JokerCard)
                jokerText.gameObject.SetActive(true);
            else
                jokerText.gameObject.SetActive(false);
        }

        public void MoveTargetPosition(Vector3 targetPos, Action onComplete)
        {
            StartCoroutine(cardViewAnimationModule.MovePosition(targetPos, onComplete));
        }
    }
}