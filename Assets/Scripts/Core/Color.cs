using System;

namespace Core
{
    public class Color : IComparable<Color>
    {
        public byte r;
        public byte g;
        public byte b;

        public string RGB =>r.ToString() + g.ToString() + b.ToString();
      


        public Color(byte r, byte g, byte b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }


        public override string ToString()
        {
            return "r:" + r + "g:" + g + "b:" + b;
        }

        public int CompareTo(Color obj)
        {
            return String.Compare(RGB, obj.RGB, StringComparison.Ordinal);
        }

        public static bool operator ==(Color c1, Color c2)
        {
            return c1.b == c2.b && c1.g == c2.g && c1.r == c2.r;
        }

        public static bool operator !=(Color c1, Color c2)
        {
            return !(c1 == c2);
        }

        public override bool Equals(Object obj)
        {
            return false;
        }
    }
}