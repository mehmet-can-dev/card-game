using System;
using CardGame.Core;
using CardGame.View.Hand;
using CardGame.View.SpriteTexts;
using CardGame.View.Utilities;
using UnityEngine;
using UnityEngine.Rendering;
using Color = UnityEngine.Color;

namespace CardGame.View.Card
{
    [RequireComponent(typeof(ICardViewAnimationModule))]
    [RequireComponent(typeof(ICardViewFinderModule))]
    public class CardView : MonoBehaviour
    {
        private ICardViewAnimationModule cardViewAnimationModule;

        private ICardViewFinderModule cardViewFinderModule;

        [Header("Child References")] [SerializeField]
        private ColorSetterUseByProperty frontColorSetter;

        [SerializeField] private ColorSetterUseByProperty backColorSetter;
        [SerializeField] private ColorSetterUseByProperty outLineColorSetter;
        [SerializeField] private SpriteText cardNoSpriteText;
        [SerializeField] private GameObject jokerGameObject;
        [SerializeField] private SortingGroup sortingGroup;

        private NumericColoredCard card;

        public NumericColoredCard Card => card;

        public void Init(NumericColoredCard card, Color backColor)
        {
            this.card = card;
            frontColorSetter.SetColor(Utilities.ColorUtilities.CoreColorToUnityColor(card.Color));
            backColorSetter.SetColor(backColor);
            outLineColorSetter.SetColor(Color.cyan);
            cardNoSpriteText.SetNumber(card.No);

            cardViewAnimationModule = GetComponent<ICardViewAnimationModule>();
            cardViewFinderModule = GetComponent<ICardViewFinderModule>();

            if (card is JokerCard)
                jokerGameObject.SetActive(true);
            else
                jokerGameObject.SetActive(false);

            outLineColorSetter.gameObject.SetActive(false);
        }

        public void DestroyCard()
        {
            cardNoSpriteText.Clear();
            Destroy(gameObject);
        }

        public void MoveTargetPosition(Vector3 targetPos, Action onComplete)
        {
            StartCoroutine(cardViewAnimationModule.MovePosition(targetPos, onComplete));
        }

        public Tile FindClosestTile()
        {
            return cardViewFinderModule.FindClosestTile();
        }

        public void SelectCard()
        {
            outLineColorSetter.gameObject.SetActive(true);
            sortingGroup.sortingOrder = LayerConstants.SELECTEDCARDSORTINGORDER;
        }

        public void DeSelectCard()
        {
            outLineColorSetter.gameObject.SetActive(false);
            sortingGroup.sortingOrder = LayerConstants.DEFAULTSORTINGORDER;
        }
    }
}