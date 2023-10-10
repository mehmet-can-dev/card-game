using UnityEngine;

namespace CardGame.View.Utilities
{
    public class ColorSetterUseByProperty : MonoBehaviour
    {
        [Header("Child Reference")]
        [SerializeField] private Renderer targetRenderer;
        private MaterialPropertyBlock propertyBlock;

        public void SetColor(Color color)
        {
            if (propertyBlock == null)
                propertyBlock = new MaterialPropertyBlock();
            propertyBlock.SetColor("_Color", color);
            targetRenderer.SetPropertyBlock(propertyBlock);
        }
    }
}