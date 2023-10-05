using UnityEngine;

namespace Utilities
{
    public static class ColorUtilities
    {
        public static Color CoreColorToUnityColor(CardGame.Core.Color color)
        {
            return new Color(color.R / 255f, color.G / 255f, color.B / 255f);
        }
    }
}