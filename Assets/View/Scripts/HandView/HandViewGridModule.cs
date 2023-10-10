using System;
using System.Collections.Generic;
using CardGame.Core;
using CardGame.View.Card;
using CardGame.View.DataModels;
using CardGame.View.Utilities;
using UnityEngine;

namespace CardGame.View.Hand
{
    public class HandViewGridModule : MonoBehaviour
    {
        [Header("Project References")] [SerializeField]
        private Tile tilePrefab;

        [Header("Variable References")] [SerializeField]
        private HandViewGridData handViewGridData;

        [Header("Child References")] [SerializeField]
        private Transform tileParent;

        private List<Tile> tiles;

        private Dictionary<Core.Card, Tile> cardTileOwnershipContainer;

        public void Init()
        {
            tiles = new List<Tile>();
            cardTileOwnershipContainer = new Dictionary<Core.Card, Tile>();
            CreateTiles();
        }

        public void ConnectCardToTile(Tile tile, CardView cardView)
        {
            cardTileOwnershipContainer.Add(cardView.Card, tile);
            tile.ConnectCardWithoutNotify(cardView);
        }

        public void RemoveConnectionCardFromTile(NumericColoredCard card, Tile tile)
        {
            cardTileOwnershipContainer.Remove(card);
            tile.ResetConnectCardWithoutNotify();
        }

        public Tile GetEmptyTile()
        {
            Func<Tile, bool> prediction = (tile) => !tile.IsCardConnected();
            if (CheckTiles(prediction, out var tile)) return tile;
            return null;
        }

        public Tile GetConnectedTile(NumericColoredCard card)
        {
            if (cardTileOwnershipContainer.TryGetValue(card, out Tile tile))
            {
                return tile;
            }

            return null;
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

        private void OnCardConnectedTile(Tile tile, CardView cardView)
        {
            cardTileOwnershipContainer.Add(cardView.Card, tile);
        }

        private void OnCardDisconnectedTile(Tile tile, CardView cardView)
        {
            cardTileOwnershipContainer.Remove(cardView.Card);
        }


        private void CreateTiles()
        {
            Vector2 tileSize = tilePrefab.transform.localScale;
            Vector2 offset = new Vector2(handViewGridData.sizeX - tileSize.x, handViewGridData.sizeY) * -0.5f;

            for (int y = handViewGridData.sizeY - 1; y >= 0; y--)
            {
                for (int x = 0; x < handViewGridData.sizeX; x++)
                {
                    var tile = Instantiate(tilePrefab, tileParent);
                    var pos = new Vector2(x, y);
                    pos *= tileSize;
                    tile.transform.localScale *= 0.98f;
                    tile.Init(handViewGridData.tileColor, OnCardConnectedTile, OnCardDisconnectedTile);
                    tile.transform.localPosition = pos + offset;
                    tiles.Add(tile);
                }
            }
        }

        private bool CheckTiles(Func<Tile, bool> prediction, out Tile tile)
        {
            for (int i = 0; i < tiles.Count; i++)
            {
                if (prediction(tiles[i]))
                {
                    tile = tiles[i];
                    return true;
                }
            }

            tile = null;
            return false;
        }
    }
}