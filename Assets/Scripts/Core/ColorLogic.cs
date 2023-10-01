
namespace CardGame.Core
{
    public static class ColorLogic
    {
        private static Color red = new Color(255, 0, 0);
        private static Color black = new Color(0, 0, 0);
        private static Color yellow = new Color(255, 255, 0);
        private static Color blue = new Color(0, 0, 255);

        public static Color[] UsedColors = new Color[] { blue, black, yellow, red };
    }
}