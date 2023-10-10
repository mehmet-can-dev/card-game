using System.Collections;
using CardGame.Core;
using CardGame.View.Card;
using CardGame.View.Hand;
using CardGame.View.SpriteTexts;
using CardGame.View.Utilities;
using UnityEngine;
using Color = UnityEngine.Color;

namespace CardGame.View.Deck
{
    public class DeckViewBase : MonoBehaviour
    {
        [Header("Module References")] [SerializeField]
        private DeckViewCardSpawnerModule deckViewCardSpawnerModule;

        [SerializeField] private DeckViewAnimationModule deckViewAnimationModule;

        [Header("Child References")] [SerializeField]
        private ColorSetterUseByProperty colorSetter;

        [SerializeField] private SpriteText cardCountSpriteText;

        private Core.Deck deck;
        private Color deckColor;

        public void Init(Core.Deck deck, Color deckColor)
        {
            this.deck = deck;
            this.deckColor = deckColor;
            deckViewCardSpawnerModule.Init();
            deckViewAnimationModule.Init();
            colorSetter.SetColor(deckColor);
            UpdateText(deck);
        }

        private void UpdateText(Core.Deck deck)
        {
            cardCountSpriteText.SetNumber(deck.CurrentCardCount);
        }

        public CardViewBase DrawCard()
        {
            var c = deck.DrawCard();
            UpdateText(deck);
            var spawnCard = deckViewCardSpawnerModule.SpawnCard(c, deckColor);
            return spawnCard;
        }

        public void AddCard(NumericColoredCard card)
        {
            deck.AddCard(card);
            UpdateText(deck);
        }

        public IEnumerator DeckToPlayerHandAnimation(HandViewBase targetHand, CardViewBase spawnedCard,
            Tile connectTile)
        {
            var targetPos = connectTile.transform.position + Vector3.forward * LayerConstants.CARDLAYER;
            yield return deckViewAnimationModule.CardSpawnAnimation(spawnedCard, targetPos);
        }

        public void Shuffle()
        {
            deck.Shuffle();
        }
    }
}