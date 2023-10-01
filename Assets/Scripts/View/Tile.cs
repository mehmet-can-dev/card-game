using CardGame.Core;
using UnityEngine;
using Color = UnityEngine.Color;

namespace CardGame.View
{
    public class Tile : MonoBehaviour, IInteractable
    {
        [SerializeField] private ColorSetterUseByProperty colorSetter;

        private CardViewBase connectedCard;

        public void Init(Color tileColor)
        {
            colorSetter.SetColor(tileColor);
        }

        public void ConnectCard(CardViewBase connectCard)
        {
            connectCard.transform.SetParent(transform);
            connectedCard = connectCard;
        }

        public void ResetConnectCardPosition()
        {
            connectedCard.transform.localPosition = Vector3.zero;
        }

        public bool IsCardConnected()
        {
            return connectedCard != null;
        }

        public void OnInteractStarted(Vector3 pos)
        {
            if (!IsCardConnected())
                return;

            var cardPos = connectedCard.transform.position;
            cardPos.x = pos.x;
            cardPos.y = pos.y;
            cardPos.z -= 1;
            connectedCard.transform.position = cardPos;
        }

        public void OnDrag(Vector3 pos)
        {
            if (!IsCardConnected())
                return;


            var cardPos = connectedCard.transform.position;
            cardPos.x = pos.x;
            cardPos.y = pos.y;
            connectedCard.transform.position = cardPos;
        }

        public void OnInteractEnded(Vector3 pos)
        {
            if (!IsCardConnected())
                return;

            //  if(Physics.BoxCastNonAlloc())
        }
    }
}