﻿using CardGame.Core;
using CardGame.View.Card;
using CardGame.View.SpriteTexts;
using CardGame.View.Utilities;
using UnityEngine;
using Color = UnityEngine.Color;

namespace CardGame.View.Deck
{
    [RequireComponent(typeof(IDeckViewAnimationModule))]
    public class DeckView : MonoBehaviour
    {
        private IDeckViewAnimationModule deckViewAnimationModuleModule;

        [Header("Module References")] [SerializeField]
        private DeckViewCardSpawnerModule deckViewCardSpawnerModule;

        [Header("Child References")] [SerializeField]
        private ColorSetterUseByProperty colorSetter;

        [SerializeField] private SpriteText cardCountSpriteText;

        private Core.Deck deck;
        private Color deckColor;

        public IDeckViewAnimationModule DeckViewAnimationModuleModule => deckViewAnimationModuleModule;

        public void Init(Core.Deck deck, Color deckColor)
        {
            this.deck = deck;
            this.deckColor = deckColor;
            colorSetter.SetColor(deckColor);
            UpdateText(deck);

            deckViewAnimationModuleModule = GetComponent<IDeckViewAnimationModule>();
        }

        private void UpdateText(Core.Deck deck)
        {
            cardCountSpriteText.SetNumber(deck.CurrentCardCount);
        }

        public CardView DrawCard()
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


        public void Shuffle()
        {
            deck.Shuffle();
        }
    }
}