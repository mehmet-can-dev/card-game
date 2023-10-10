using System;
using System.Collections.Generic;
using CardGame.Core;
using CardGame.View.DataModels;
using CardGame.View.Utilities;
using UnityEngine;

namespace CardGame.View.Hand
{
    public class SplitCardAssigner : MonoBehaviour, ICardAssigner
    {
        private HandViewGridData handViewGridData;
        private Dictionary<Core.Card, Tile> cardTileOwnershipContainer;
        private List<Tile> tiles;

        public void Init(HandViewGridData handViewGridData, Dictionary<Core.Card, Tile> cardTileOwnershipContainer,
            List<Tile> tiles)
        {
            this.cardTileOwnershipContainer = cardTileOwnershipContainer;
            this.handViewGridData = handViewGridData;
            this.tiles = tiles;
        }

        public void ReAssignCards(NumericColoredCard[][] cardContainer, NumericColoredCard[] notMatchedCards,
            Action onComplete)
        {
            Vector2Int tempIndexTwoDimension = Vector2Int.zero;

            if (cardContainer == null && notMatchedCards == null)
            {
                onComplete?.Invoke();
            }
            else if (cardContainer == null)
            {
                AssignOrderedCards(notMatchedCards, tempIndexTwoDimension, onComplete);
            }
            else if (notMatchedCards == null)
            {
                AssignOrderedCardContainer(cardContainer, onComplete);
            }
            else
            {
                tempIndexTwoDimension = AssignOrderedCardContainer(cardContainer);
                AssignOrderedCards(notMatchedCards, tempIndexTwoDimension, onComplete);
            }
        }

        private Vector2Int AssignOrderedCardContainer(NumericColoredCard[][] cardContainer, Action onComplete = null)
        {
            int tileHorizontalIndex = 0;
            int tileVerticalIndex = 0;

            var tempContainer = new Dictionary<Core.Card, Tile>(cardTileOwnershipContainer);
            for (int i = 0; i < cardContainer.Length; i++)
            {
                if (tileHorizontalIndex + cardContainer[i].Length > handViewGridData.sizeX)
                {
                    tileVerticalIndex++;
                    tileHorizontalIndex = 0;
                }

                for (int j = 0; j < cardContainer[i].Length; j++)
                {
                    var card = cardContainer[i][j];
                    if (tempContainer.TryGetValue(card, out var value))
                    {
                        var tilesIndex = tileHorizontalIndex + tileVerticalIndex * handViewGridData.sizeX;

                        if (i == cardContainer.Length - 1 && j == cardContainer[i].Length - 1)
                        {
                            AssignCardToTile(value, tilesIndex, card, onComplete);
                        }
                        else
                        {
                            AssignCardToTile(value, tilesIndex, card);
                        }
                    }


                    tileHorizontalIndex++;
                }

                tileHorizontalIndex++;
            }

            return new Vector2Int(tileHorizontalIndex, tileVerticalIndex);
        }

        private void AssignOrderedCards(NumericColoredCard[] notMatchedCards, Vector2Int startIndexes,
            Action onComplete = null)
        {
            var tempContainerR = new Dictionary<Core.Card, Tile>(cardTileOwnershipContainer);

            var currentIndex = startIndexes.x + startIndexes.y * handViewGridData.sizeX;

            for (int i = 0; i < notMatchedCards.Length; i++)
            {
                var card = notMatchedCards[i];
                if (tempContainerR.TryGetValue(card, out var value))
                {
                    if (i == notMatchedCards.Length - 1)
                    {
                        AssignCardToTile(value, currentIndex, card, onComplete);
                    }
                    else
                    {
                        AssignCardToTile(value, currentIndex, card);
                    }
                }

                currentIndex++;
            }
        }

        private void AssignCardToTile(Tile value, int tilesIndex, NumericColoredCard card, Action onComplete = null)
        {
            var cardView = value.GetConnectedCard;
            var tempIndex = tilesIndex;
            value.ResetConnectCardWithoutNotify();
            cardView.MoveTargetPosition(
                tiles[tempIndex].transform.position + Vector3.forward * LayerConstants.CARDLAYER,
                () =>
                {
                    tiles[tempIndex].ConnectCardWithoutNotify(cardView);
                    onComplete?.Invoke();
                });

            cardTileOwnershipContainer[card] = tiles[tilesIndex];
        }
    }
}