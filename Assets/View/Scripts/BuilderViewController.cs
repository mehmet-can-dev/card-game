using System;
using System.Collections;
using System.Collections.Generic;
using CardGame.Core;
using CardGame.Core.Sort;
using CardGame.View.DataModels;
using CardGame.View.DebugUi;
using UnityEngine;
using UnityEngine.Serialization;
using Color = UnityEngine.Color;

namespace CardGame.View
{
    //used for scene view initialize
    public class BuilderViewController : MonoBehaviour
    {
        [Header("Scene References")] [SerializeField]
        private DeckViewBase deckViewBase;

        [SerializeField] private HandViewBase handViewBase;
        [SerializeField] private DebugBehaviour debugBehaviour;

        [Header("Project References")] [SerializeField]
        private BuilderSettingsSO builderSettingsSo;

        private Hand hand;
        private Deck deck;

        void Start()
        {
            //RandomLogic.AssignRandomSeed();

            deck = BuildDeck(builderSettingsSo.BuilderCountData);

            deckViewBase.Init(deck, builderSettingsSo.BuilderViewData.deckColor);

            hand = new Hand(builderSettingsSo.BuilderCountData.handCount);

            handViewBase.Init(hand);

            debugBehaviour.Init(builderSettingsSo, hand, deck, handViewBase, deckViewBase);
        }

        private Deck BuildDeck(BuilderCountData builderCountData)
        {
            var builder = new DeckBuilder(builderCountData.deckCount, builderCountData.cardPerDeck,
                ColorLogic.UsedColors);

            var decks = builder.Build();
            var mergedDeck = DeckBuilder.MergeDeck(decks);
            mergedDeck.Shuffle();
            mergedDeck.TurnCardToJokerCards(builderCountData.jokerCount);
            mergedDeck.Shuffle();
            return mergedDeck;
        }
    }
}