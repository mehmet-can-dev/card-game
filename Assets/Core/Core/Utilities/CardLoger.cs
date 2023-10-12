using System.Collections.Generic;
using System.Text;

namespace CardGame.Core
{
    public class CardLoger
    {
        public static StringBuilder LogNumericArray(NumericColoredCard[] notSortableCard)
        {
            var sb = new StringBuilder();
            if (notSortableCard != null)
            {
                for (int i = 0; i < notSortableCard.Length; i++)
                {
                    sb.Append(notSortableCard[i].ToStringBuilder());
                    sb.Append("type:");
                    sb.Append(notSortableCard[i].GetType());
                    sb.Append("\n");
                }
            }

            return sb;
        }

        public static StringBuilder Log2DimensionNumericArray(NumericColoredCard[][] cards)
        {
            var sb = new StringBuilder();
            if (cards != null)
            {
                for (int i = 0; i < cards.Length; i++)
                {
                    sb.Append("list:");
                    sb.Append(i.ToString());
                    sb.Append("\n");
                    for (int j = 0; j < cards[i].Length; j++)
                    {
                        sb.Append(cards[i][j].ToStringBuilder());
                        sb.Append("type:");
                        sb.Append(cards[i][j].GetType());
                        sb.Append("\n");
                    }
                }

                sb.Append("");
            }

            return sb;
        }

        public static StringBuilder LogNumericList(List<NumericColoredCard> notSortableCard)
        {
            var sb = new StringBuilder();
            if (notSortableCard != null)
            {
                for (int i = 0; i < notSortableCard.Count; i++)
                {
                    sb.Append(notSortableCard[i].ToStringBuilder());
                    sb.Append("type:");
                    sb.Append(notSortableCard[i].GetType());
                    sb.Append("\n");
                }

                sb.AppendLine("");
            }

            return sb;
        }

        public static StringBuilder Log2DimensionNumericList(List<List<NumericColoredCard>> cards)
        {
            var sb = new StringBuilder();
            if (cards != null)
            {
                for (int i = 0; i < cards.Count; i++)
                {
                    sb.Append("list:");
                    sb.Append(i.ToString());
                    sb.Append("\n");
                    for (int j = 0; j < cards[i].Count; j++)
                    {
                        sb.Append(cards[i][j].ToStringBuilder());
                        sb.Append("type:");
                        sb.Append(cards[i][j].GetType());
                        sb.Append("\n");
                    }

                    sb.AppendLine("");
                }

                sb.AppendLine("");
            }

            return sb;
        }
    }
}