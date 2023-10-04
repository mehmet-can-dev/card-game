using System.Collections.Generic;

namespace Utilities
{
    public static class IntUtilities
    {
        public static int[] GetIntArray(int num)
        {
            List<int> listOfInts = new List<int>();
            while (num > 0)
            {
                listOfInts.Add(num % 10);
                num = num / 10;
            }

            listOfInts.Reverse();
            return listOfInts.ToArray();
        }
    }
}