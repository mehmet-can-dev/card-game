using System;
using UnityEngine.Serialization;

namespace CardGame.View.DataModels
{
    [Serializable]
    public class SortViewData
    {
        public int minCardCount;
        public int maxCardCount;
        public int minNumberPerDeck;
        public int maxNumberPerDeck;
    }
}