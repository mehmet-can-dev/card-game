using System;
using System.Collections.Generic;
using CardGame.Core;
using CardGame.View.DataModels;

namespace CardGame.View.Hand
{
    public interface ICardAssigner
    {
        public void Init(HandViewGridData handViewGridData, Dictionary<Core.Card, Tile> cardTileOwnershipContainer,
            List<Tile> tiles);

        public void ReAssignCards(NumericColoredCard[][] cardContainer, NumericColoredCard[] notMatchedCards,
            Action onComplete);
    }
}