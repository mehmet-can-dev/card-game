
namespace CardGame.Core
{
    public static class ColorConstants
    {
        public static readonly Color Red = new Color(255, 0, 0);
        public static readonly Color Black = new Color(0, 0, 0);
        public static readonly Color Yellow = new Color(255, 255, 0);
        public static readonly Color Blue = new Color(0, 0, 255);

        public static Color[] UsedColors = new Color[] { Blue, Black, Yellow, Red };
    }
}