using UnityEngine;

namespace Utilities
{
    public static class Utilities
    {
        public static Color CoreColorToUnityColor(CardGame.Core.Color color)
        {
            return new Color(color.r / 255f, color.g / 255f, color.b / 255f);
        }
    }
}