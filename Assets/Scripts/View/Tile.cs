using UnityEngine;

namespace CardGame.View
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private ColorSetterUseByProperty colorSetter;

        public void Init(Color tileColor)
        {
            colorSetter.SetColor(tileColor);
        }
    }
}