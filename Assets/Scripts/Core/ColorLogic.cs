
namespace Core
{
    public static class ColorLogic
    {
        private static Color red = new Color(255, 0, 0);
        private static Color black = new Color(0, 0, 0);
        private static Color yellow = new Color(255, 255, 0);
        private static Color blue = new Color(0, 0, 255);

        private static Color Red => red;
        private static Color Black => black;
        private static Color Yellow => yellow;
        private static Color Blue => blue;

        public static Color[] UsedColors = new Color[] { Blue, Black, Yellow, Red };
    }
}