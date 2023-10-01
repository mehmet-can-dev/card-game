using System.Collections.Generic;
using CardGame.Core;
using UnityEngine;
using Color = UnityEngine.Color;

namespace CardGame.View
{
    public class HandViewGridModule : MonoBehaviour
    {
        [SerializeField] private Tile tilePrefab;

        [SerializeField] private int sizeX;
        [SerializeField] private int sizeY;
        [SerializeField] private Transform tileParent;

        private List<Tile> tiles;

        private Dictionary<Card, Tile> cardTileOwnershipContainer;

        public void Init(Color tileColor)
        {
            tiles = new List<Tile>();
            cardTileOwnershipContainer = new Dictionary<Card, Tile>();

            Vector2 tileSize = tilePrefab.transform.localScale;
            Vector2 offset = new Vector2(sizeX - tileSize.x, sizeY) * -0.5f;

            for (int y = sizeY - 1; y >= 0; y--)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    var tile = Instantiate(tilePrefab, tileParent);
                    var pos = new Vector2(x, y);
                    pos *= tileSize;
                    tile.transform.localScale *= 0.98f;
                    tile.Init(tileColor);
                    tile.transform.localPosition = pos + offset;
                    tiles.Add(tile);
                }
            }
        }

        public void ReAssignCards(NumericColoredCard[][] cardContainer, NumericColoredCard[] notMatchedCards)
        {
            var tempContainer = new Dictionary<Card, Tile>(cardTileOwnershipContainer);

            int tileHorizontalIndex = 0;
            int tileVerticalIndex = 0;
            for (int i = 0; i < cardContainer.Length; i++)
            {
                if (tileHorizontalIndex + cardContainer[i].Length > sizeX)
                {
                    tileVerticalIndex++;
                    tileHorizontalIndex = 0;
                }

                for (int j = 0; j < cardContainer[i].Length; j++)
                {
                    var card = cardContainer[i][j];
                    if (tempContainer.TryGetValue(card, out var value))
                    {
                        var tilesIndex = tileHorizontalIndex + tileVerticalIndex * sizeX;

                        var cardView = value.GetConnectedCard;
                        var tempIndex = tilesIndex;
                        value.ResetConnectCard();
                        cardView.MoveTargetPosition(tiles[tempIndex].transform.position,
                            () => tiles[tempIndex].ConnectCard(cardView));

                        cardTileOwnershipContainer[card] = tiles[tilesIndex];
                    }


                    tileHorizontalIndex++;
                }

                tileHorizontalIndex++;
            }

            var currentIndex = tileHorizontalIndex + tileVerticalIndex * sizeX;
            currentIndex++;

            
            for (int i = 0; i < notMatchedCards.Length; i++)
            {
                var card = notMatchedCards[i];
                if (tempContainer.TryGetValue(card, out var value))
                {
                    var cardView = value.GetConnectedCard;
                    var tempIndex = currentIndex;
                    value.ResetConnectCard();
                    cardView.MoveTargetPosition(tiles[tempIndex].transform.position,
                        () => tiles[tempIndex].ConnectCard(cardView));

                    cardTileOwnershipContainer[card] = tiles[currentIndex];
                }

                currentIndex++;
            }
        }

        public void ConnectCardToTile(Tile tile, CardViewBase cardViewBase)
        {
            cardTileOwnershipContainer.Add(cardViewBase.Card, tile);
            tile.ConnectCard(cardViewBase);
        }

        public Tile GetEmptyTile()
        {
            for (int i = 0; i < tiles.Count; i++)
            {
                if (!tiles[i].IsCardConnected())
                {
                    return tiles[i];
                }
            }

            return null;
        }
    }
}