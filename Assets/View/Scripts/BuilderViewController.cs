using CardGame.Core;
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

        void Start()
        {
            deck = BuildDeck(builderSettingsSo.BuilderCountData);

            deckView.Init(deck, builderSettingsSo.BuilderViewData.deckColor);

            hand = new Core.Hand(builderSettingsSo.BuilderCountData.handCount);

            handView.Init(hand);

            debugBehaviour.Init(builderSettingsSo, hand, deck, handView, deckView);
        }

        private Core.Deck BuildDeck(BuilderCountData builderCountData)
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