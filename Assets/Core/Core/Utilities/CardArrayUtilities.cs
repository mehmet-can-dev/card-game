using UnityEngine;

namespace CardGame.Core
{
    public class CardArrayUtilities
    {
        public static void LogNumericArray(NumericColoredCard[] notSortableCard)
        {
            if (notSortableCard != null)
                for (int i = 0; i < notSortableCard.Length; i++)
                {
                    var sb = notSortableCard[i].ToStringBuilder();
                    sb.Append("type:");
                    sb.Append(notSortableCard[i].GetType());
                    Debug.Log(sb);
                }
        }

        public static void Log2DimensionNumericArray(NumericColoredCard[][] cards)
        {
            if (cards != null)
                for (int i = 0; i < cards.Length; i++)
                {
                    for (int j = 0; j < cards[i].Length; j++)
                    {
                        var sb = cards[i][j].ToStringBuilder();
                        sb.Append("type:");
                        sb.Append(cards[i][j].GetType());
                    }

                    Debug.Log("-");
                }
        }
    }
}