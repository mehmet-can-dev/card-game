using CardGame.Core;
using TMPro;
using UnityEngine;
using Color = UnityEngine.Color;

namespace CardGame.View
{
    public class DeckViewBase : MonoBehaviour
    {
        [SerializeField] private ColorSetterUseByProperty colorSetter;
        [SerializeField] private TextMeshProUGUI cardCountText;

        public void Init(Deck deck, Color deckColor)
        {
            cardCountText.SetText(deck.CurrentCardCount.ToString());
            colorSetter.SetColor(deckColor);
        }
    }
}