using System.Collections.Generic;
using UnityEngine;

namespace CardGame.View
{
    public class HandViewGridModule : MonoBehaviour
    {
        [SerializeField] private Tile tilePrefab;

        [SerializeField] private int sizeX;
        [SerializeField] private int sizeY;
        [SerializeField] private Transform tileParent;
        

        private List<Tile> tiles;

        public void Init(Color tileColor)
        {
            tiles = new List<Tile>();

            Vector2 tileSize = tilePrefab.transform.localScale;
            Vector2 offset = new Vector2(sizeX - tileSize.x, sizeY) * -0.5f;

            for (int y = 0; y < sizeY; y++)
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

        public void ConnectCardToTile(Tile tile, CardViewBase cardViewBase)
        {
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