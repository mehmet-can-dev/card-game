using System;
using CardGame.View.Card;
using CardGame.View.Utilities;
using UnityEngine;
using Color = UnityEngine.Color;

namespace CardGame.View.Hand
{
    public class Tile : MonoBehaviour
    {
        [Header("Module References")] [SerializeField]
        private TileInteractableModule tileInteractableModule;

        [Header("Child References")] [SerializeField]
        private ColorSetterUseByProperty colorSetter;

        private CardView connectedCard;
        public CardView GetConnectedCard => connectedCard;

        private Action<Tile, CardView> onCardConnect;
        private Action<Tile, CardView> onCardReset;

        public void Init(Color tileColor, Action<Tile, CardView> onCardConnect,
            Action<Tile, CardView> onCardReset)
        {
            this.onCardConnect = onCardConnect;
            this.onCardReset = onCardReset;
            colorSetter.SetColor(tileColor);
            tileInteractableModule.Init(this);
        }

        public void ConnectCard(CardView connectCard)
        {
            connectCard.transform.SetParent(transform);
            connectedCard = connectCard;
            onCardConnect?.Invoke(this, connectCard);
        }

        public void ConnectCardWithoutNotify(CardView connectCard)
        {
            connectCard.transform.SetParent(transform);
            connectedCard = connectCard;
        }

        public void ResetConnectCardPosition()
        {
            connectedCard.transform.localPosition = Vector3.forward * LayerConstants.CARDLAYER;
        }

        public void ResetConnectCardWithoutNotify()
        {
            connectedCard = null;
        }

        public void ResetConnectCard()
        {
            onCardReset?.Invoke(this, connectedCard);
            connectedCard = null;
        }

        public bool IsCardConnected()
        {
            return connectedCard != null;
        }
    }
}