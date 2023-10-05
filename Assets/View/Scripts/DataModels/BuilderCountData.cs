using System;

namespace CardGame.View.DataModels
{
    [Serializable]
    public class BuilderCountData
    {
        public int handCount = 20;
        public int deckCount = 2;
        public int cardPerDeck = 52;
        public int jokerCount = 4;
    }
}