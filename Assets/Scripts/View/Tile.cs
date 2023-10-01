using CardGame.Core;
using UnityEngine;
using Color = UnityEngine.Color;

namespace CardGame.View
{
    public class Tile : MonoBehaviour
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
    }
}