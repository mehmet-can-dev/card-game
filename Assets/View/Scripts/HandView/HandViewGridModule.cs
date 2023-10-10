using System;
using System.Collections.Generic;
using CardGame.Core;
using CardGame.View.Card;
using CardGame.View.DataModels;
using UnityEngine;

namespace CardGame.View.Hand
{
    [RequireComponent(typeof(ICardAssigner))]
    public class HandViewGridModule : MonoBehaviour
    {
        private ICardAssigner cardAssigner;

        [Header("Project References")] [SerializeField]
        private Tile tilePrefab;

        [Header("Variable References")] [SerializeField]
        private HandViewGridData handViewGridData;

        [Header("Child References")] [SerializeField]
        private Transform tileParent;

        private List<Tile> tiles;

        private Dictionary<Core.Card, Tile> cardTileOwnershipContainer;

        public ICardAssigner CardAssigner => cardAssigner;

        public void Init()
        {
            tiles = new List<Tile>();
            cardTileOwnershipContainer = new Dictionary<Core.Card, Tile>();
            cardAssigner = GetComponent<ICardAssigner>();

            CreateTiles();
            cardAssigner.Init(handViewGridData, cardTileOwnershipContainer, tiles);
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