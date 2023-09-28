public class NumericColoredCard : CardBase
{
    public int No { get; private set; }
    public Color Color { get; private set; }

    public NumericColoredCard(int id, int no, Color color) : base(id)
    {
        No = no;
        Color = color;
    }
}