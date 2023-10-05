using UnityEngine;

namespace CardGame.Core
{
    public class CardArrayUtilities
    {
        public static T[][] Merge2DimensionArray<T>(T[][] arr1, T[][] arr2)
        {
            var mergedArray = new T[arr1.Length + arr2.Length][];

            int tempIndex = 0;
            for (int i = 0; i < arr1.Length; i++)
            {
                mergedArray[i] = new T[arr1[i].Length];
                for (int j = 0; j < arr1[i].Length; j++)
                {
                    mergedArray[i][j] = arr1[i][j];
                }

                tempIndex++;
            }

            for (int i = 0; i < arr2.Length; i++)
            {
                mergedArray[tempIndex + i] = new T[arr2[i].Length];
                for (int j = 0; j < arr2[i].Length; j++)
                {
                    mergedArray[tempIndex + i][j] = arr2[i][j];
                }
            }

            return mergedArray;
        }

        public static bool CheckEquality2DCardArray(NumericColoredCard[][] arr1, NumericColoredCard[][] arr2)
        {
            return arr1.Equals(arr2);
        }

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