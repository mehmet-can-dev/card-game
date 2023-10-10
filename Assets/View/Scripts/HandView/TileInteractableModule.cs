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

            var cardView=tile.GetConnectedCard;
            
            cardView.SelectCard();

            var cardPos = cardView.transform.position;
            offset = cardPos - pos;
            offset.z = 0;

            cardPos.x = pos.x;
            cardPos.y = pos.y;
            cardPos.z = LayerConstants.SELECTEDCARDLAYER;
            cardView.transform.position = cardPos + offset;
        }

        public void OnDrag(Vector3 pos)
        {
            if (!tile.IsCardConnected())
                return;
            
            var cardView=tile.GetConnectedCard;

            var cardPos = cardView.transform.position;
            offset.z = 0;

            cardPos.x = pos.x;
            cardPos.y = pos.y;
            cardView.transform.position = cardPos + offset;
        }

        public void OnInteractEnded()
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