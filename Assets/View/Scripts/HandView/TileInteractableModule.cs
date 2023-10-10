using CardGame.View.InputSystem;
using CardGame.View.Utilities;
using UnityEngine;

namespace CardGame.View.Hand
{
    public class TileInteractableModule : MonoBehaviour, IInteractable
    {
        private Tile tile;
        private Vector3 offset;

        public void Init(Tile tile)
        {
            this.tile = tile;
        }

        public void OnInteractStarted(Vector3 pos)
        {
            if (!tile.IsCardConnected())
                return;

            tile.GetConnectedCard.SelectCard();

            var cardPos = tile.GetConnectedCard.transform.position;
            offset = cardPos - pos;
            offset.z = 0;

            cardPos.x = pos.x;
            cardPos.y = pos.y;
            cardPos.z = LayerConstants.SELECTEDCARDLAYER;
            tile.GetConnectedCard.transform.position = cardPos + offset;
        }

        public void OnDrag(Vector3 pos)
        {
            if (!tile.IsCardConnected())
                return;

            var cardPos = tile.GetConnectedCard.transform.position;
            offset.z = 0;

            cardPos.x = pos.x;
            cardPos.y = pos.y;
            tile.GetConnectedCard.transform.position = cardPos + offset;
        }

        public void OnInteractEnded(Vector3 pos)
        {
            if (!tile.IsCardConnected())
                return;

            var connectTile = tile.GetConnectedCard.FindClosestTile();

            if (ReferenceEquals(connectTile, null))
            {
                tile.ResetConnectCardPosition();
                tile.GetConnectedCard.DeSelectCard();
                return;
            }

            if (connectTile == tile)
            {
                tile.ResetConnectCardPosition();
                tile.GetConnectedCard.DeSelectCard();
                return;
            }

            if (connectTile.IsCardConnected())
            {
                tile.ResetConnectCardPosition();
                tile.GetConnectedCard.DeSelectCard();
                return;
            }


            var cardView = tile.GetConnectedCard;
            cardView.DeSelectCard();
            tile.ResetConnectCard();
            connectTile.ConnectCard(cardView);
            connectTile.ResetConnectCardPosition();
        }
    }
}