using UnityEngine;

namespace CardGame.View
{
    public class HandViewGridModule : MonoBehaviour
    {
        [SerializeField] private Tile tilePrefab;

        [SerializeField] private int sizeX;
        [SerializeField] private int sizeY;

        [SerializeField] private Transform tileParent;

        public void Init(Color tileColor)
        {
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
                }
            }
        }
    }
}