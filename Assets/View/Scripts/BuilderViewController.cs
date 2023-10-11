using System;
using CardGame.Core;
using CardGame.Core.Sort;
using CardGame.View.DataModels;
using CardGame.View.DebugSystem;
using CardGame.View.Deck;
using CardGame.View.Hand;
using CardGame.View.SO;
using UnityEngine;
using UnityEngine.Serialization;

namespace CardGame.View
{
    //used for scene view initialize
    public class BuilderViewController : MonoBehaviour
    {
        [Header("Scene References")] [SerializeField]
        private DeckView deckView;

        [SerializeField] private HandView handView;
        [SerializeField] private DebugBehaviour debugBehaviour;

        [Header("Project References")] [SerializeField]
        private BuilderSettingsSO builderSettingsSo;

        private Core.Hand hand;
        private Core.Deck deck;

        private ISort currentSorter;

        void Start()
        {
            deck = BuildDeck(builderSettingsSo.BuilderCountData);

            deckView.Init(deck, builderSettingsSo.BuilderViewData.deckColor);

            hand = new Core.Hand(builderSettingsSo.BuilderCountData.handCount);

            handView.Init(hand);

            debugBehaviour.Init(this, hand, handView, deckView);
        }

        public void SortHand(Action onComplete)
        {
            if (currentSorter == null)
                throw new Exception("Sort Type Unknown");
            
            handView.SortHand(currentSorter, builderSettingsSo.SortViewData, onComplete);
        }

        public void SetNewSorter(ISort sorter)
        {
            currentSorter = sorter;
        }

        private Core.Deck BuildDeck(BuilderCountData builderCountData)
        {
            var builder = new DeckBuilder(builderCountData.deckCount, builderCountData.cardPerDeck,
                ColorConstants.UsedColors);

            var decks = builder.Build();
            var mergedDeck = DeckBuilder.MergeDeck(decks);
            mergedDeck.Shuffle();
            mergedDeck.TurnCardToJokerCards(builderCountData.jokerCount);
            mergedDeck.Shuffle();
            return mergedDeck;
        }
    }
}