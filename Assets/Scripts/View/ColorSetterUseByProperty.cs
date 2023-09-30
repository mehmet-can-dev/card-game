using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardGame.View
{
    public class ColorSetterUseByProperty : MonoBehaviour
    {
        [SerializeField] private Renderer renderer;
        private MaterialPropertyBlock propertyBlock;

        public void SetColor(Color color)
        {
            if (propertyBlock == null)
                propertyBlock = new MaterialPropertyBlock();
            propertyBlock.SetColor("_Color", color);
            renderer.SetPropertyBlock(propertyBlock);
        }
    }
}