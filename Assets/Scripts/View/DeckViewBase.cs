using System;
using System.Collections;
using CardGame.Core;
using TMPro;
using UnityEngine;
using Color = UnityEngine.Color;

namespace CardGame.View
{
    public class DeckViewBase : MonoBehaviour
    {
        [SerializeField] private ColorSetterUseByProperty colorSetter;
        [SerializeField] private TextMeshProUGUI cardCountText;

        [SerializeField] private DeckViewCardSpawnerModule deckViewCardSpawnerModule;
        [SerializeField] private DeckViewAnimationModule deckViewAnimationModule;
        private Deck deck;
        private Color deckColor;

        public void Init(Deck deck, Color deckColor)
        {
            this.deck = deck;
            this.deckColor = deckColor;
            deckViewCardSpawnerModule.Init();
            deckViewAnimationModule.Init();
            colorSetter.SetColor(deckColor);
            UpdateText(deck);
        }

        private void UpdateText(Deck deck)
        {
            cardCountText.SetText(deck.CurrentCardCount.ToString());
        }

        public CardViewBase DrawCard()
        {
            var c = deck.DrawCard();
            UpdateText(deck);
            var spawnCard = deckViewCardSpawnerModule.SpawnCard(c, deckColor);
            return spawnCard;
        }

        public IEnumerator DeckToPlayerHandAnimation(HandViewBase targetHand, CardViewBase spawnedCard,
            Tile connectTile)
        {
            var targetPos = connectTile.transform.position;
            yield return deckViewAnimationModule.CardSpawnAnimation(spawnedCard, targetPos);
        }
    }
}