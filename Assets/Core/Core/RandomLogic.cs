using System;

namespace CardGame.Core
{
    public static class RandomLogic
    {
        public static void ShuffleWithRandomSeed<T>(this T[] array, int startIndex)
        {
            var rng = new global::System.Random(DateTime.Now.Millisecond);
            var n = array.Length;
            while (n > startIndex + 1)
            {
                n--;
                var k = rng.Next(startIndex, n + 1);
                (array[k], array[n]) = (array[n], array[k]);
            }
        }
    }
}