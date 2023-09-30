using System;
using Random = UnityEngine.Random;

namespace Core
{
    public static class RandomLogic
    {
        public static void AssignRandomSeed()
        {
            Random.InitState(DateTime.Now.Millisecond);
        }

        public static void ShuffleWithRandomSeed<T>(this T[] array)
        {
            var rng = new global::System.Random(Random.Range(0, 1000));
            var n = array.Length;
            while (n > 1)
            {
                n--;
                var k = rng.Next(n + 1);
                (array[k], array[n]) = (array[n], array[k]);
            }
        }

        public static void Shuffle<T>(this T[] array, int startIndex = 0)
        {
            int n = array.Length;
            while (n > startIndex + 1)
            {
                int k = Random.Range(startIndex, n--);
                (array[n], array[k]) = (array[k], array[n]);
            }
        }


        public static int RandomInteger(int min, int max)
        {
            return Random.Range(min, max);
        }
    }
}