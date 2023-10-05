
namespace CardGame.Core
{
    public static class ColorLogic
    {
        public static Color red = new Color(255, 0, 0);
        public static Color black = new Color(0, 0, 0);
        public static Color yellow = new Color(255, 255, 0);
        public static Color blue = new Color(0, 0, 255);

        public static Color[] UsedColors = new Color[] { blue, black, yellow, red };
    }
}