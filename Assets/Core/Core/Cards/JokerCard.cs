using System.Text;

namespace CardGame.Core
{
    public class JokerCard : NumericColoredCard
    {
        public JokerCard(int id, int no, Color color) : base(id, no, color)
        {
        }

        public override StringBuilder ToStringBuilder()
        {
            var sb = base.ToStringBuilder();
            sb.Append("type");
            sb.Append(GetType());

            return sb;
        }
    }
}