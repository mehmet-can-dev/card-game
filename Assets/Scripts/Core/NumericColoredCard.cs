using System.Text;

namespace Core
{
    public class NumericColoredCard : CardBase
    {
        public int No { get; private set; }
        public Color Color { get; private set; }

        public NumericColoredCard(int id, int no, Color color) : base(id)
        {
            No = no;
            Color = color;
        }

        public StringBuilder ToStringBuilder()
        {
            var sb = new StringBuilder();
            sb.Append(nameof(Id));
            sb.Append(Id);
            sb.Append(nameof(No));
            sb.Append(No);
            sb.Append(nameof(Color));
            sb.Append(Color);
            return sb;
        }
    }
}