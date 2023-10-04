using System;
using UnityEngine;
using Color = UnityEngine.Color;

namespace CardGame.View
{
    public class Tile : MonoBehaviour
    {
        [Header("Module References")] [SerializeField]
        private TileInteractableModule tileInteractableModule;
        
        [Header("Child References")] [SerializeField]
        private ColorSetterUseByProperty colorSetter;
        
        private CardViewBase connectedCard;
        public CardViewBase GetConnectedCard => connectedCard;

        private Action<Tile,CardViewBase> onCardConnect;
        private Action<Tile,CardViewBase> onCardReset;
        
        public void Init(Color tileColor,Action<Tile,CardViewBase> onCardConnect,Action<Tile,CardViewBase> onCardReset)
        {
            this.onCardConnect = onCardConnect;
            this.onCardReset = onCardReset;
            colorSetter.SetColor(tileColor);
            tileInteractableModule.Init(this);
        }

        public void ConnectCard(CardViewBase connectCard)
        {
            connectCard.transform.SetParent(transform);
            connectedCard = connectCard;
            onCardConnect?.Invoke(this,connectCard);
        }
        public void ConnectCardWithoutNotify(CardViewBase connectCard)
        {
            connectCard.transform.SetParent(transform);
            connectedCard = connectCard;
        }

        public void ResetConnectCardPosition()
        {
            connectedCard.transform.localPosition = Vector3.zero;
        }

        public void ResetConnectCardWithoutNotify()
        {
            connectedCard = null;
           
        }
        
        public void ResetConnectCard()
        {
            onCardReset?.Invoke(this,connectedCard);
            connectedCard = null;
            
        }

        public bool IsCardConnected()
        {
            return connectedCard != null;
        }
        
    }
}