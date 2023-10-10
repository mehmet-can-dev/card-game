using System;
using System.Collections.Generic;
using CardGame.Core;
using CardGame.View.DataModels;
using UnityEngine;
using Color = UnityEngine.Color;

namespace CardGame.View
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

        private Dictionary<Card, Tile> cardTileOwnershipContainer;

        public void Init()
        {
            tiles = new List<Tile>();
            cardTileOwnershipContainer = new Dictionary<Card, Tile>();
            CreateTiles();
        }

        public void ConnectCardToTile(Tile tile, CardViewBase cardViewBase)
        {
            cardTileOwnershipContainer.Add(cardViewBase.Card, tile);
            tile.ConnectCardWithoutNotify(cardViewBase);
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

        public void ReAssignCards(NumericColoredCard[][] cardContainer, NumericColoredCard[] notMatchedCards)
        {
            Vector2Int tempIndexTwoDimension = Vector2Int.zero;
            if (cardContainer != null)
            {
                tempIndexTwoDimension = AssignOrderedCardContainer(cardContainer);
            }

            if (notMatchedCards != null)
            {
                AssignOrderedCards(notMatchedCards, tempIndexTwoDimension);
            }
        }

        private Vector2Int AssignOrderedCardContainer(NumericColoredCard[][] cardContainer)
        {
            int tileHorizontalIndex = 0;
            int tileVerticalIndex = 0;

            var tempContainer = new Dictionary<Card, Tile>(cardTileOwnershipContainer);
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

                        AssignCardToTile(value, tilesIndex, card);
                    }

                    tileHorizontalIndex++;
                }

                tileHorizontalIndex++;
            }

            return new Vector2Int(tileHorizontalIndex, tileVerticalIndex);
        }

        private void AssignOrderedCards(NumericColoredCard[] notMatchedCards, Vector2Int startIndexes)
        {
            var tempContainerR = new Dictionary<Card, Tile>(cardTileOwnershipContainer);

            var currentIndex = startIndexes.x + startIndexes.y * handViewGridData.sizeX;

            for (int i = 0; i < notMatchedCards.Length; i++)
            {
                var card = notMatchedCards[i];
                if (tempContainerR.TryGetValue(card, out var value))
                {
                    AssignCardToTile(value, currentIndex, card);
                }

                currentIndex++;
            }
        }

        private void AssignCardToTile(Tile value, int tilesIndex, NumericColoredCard card)
        {
            var cardView = value.GetConnectedCard;
            var tempIndex = tilesIndex;
            value.ResetConnectCardWithoutNotify();
            cardView.MoveTargetPosition(tiles[tempIndex].transform.position + Vector3.forward * LayerConstants.CARDLAYER,
                () => tiles[tempIndex].ConnectCardWithoutNotify(cardView));

            cardTileOwnershipContainer[card] = tiles[tilesIndex];
        }

        private void OnCardConnectedTile(Tile tile, CardViewBase cardViewBase)
        {
            cardTileOwnershipContainer.Add(cardViewBase.Card, tile);
        }

        private void OnCardDisconnectedTile(Tile tile, CardViewBase cardViewBase)
        {
            cardTileOwnershipContainer.Remove(cardViewBase.Card);
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