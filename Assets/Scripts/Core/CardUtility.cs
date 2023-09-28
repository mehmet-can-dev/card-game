public static class CardUtility
{
    private static Color red = new Color(255, 0, 0);
    private static Color black = new Color(0, 0, 0);
    private static Color yellow = new Color(255, 255, 0);
    private static Color blue = new Color(0, 0, 255);

    public static Color Red => red;
    public static Color Black => black;
    public static Color Yellow => yellow;
    public static Color Blue => blue;

    public static Color[] UsedColors = new Color[] { Blue, Black, Yellow, Red };
}