using System;

namespace CardGame.Core
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
            if (ReferenceEquals(c1, c2))
                return true;

            if (ReferenceEquals(c1, null) || ReferenceEquals(c2, null))
                return false;
                
            
            return c1.b == c2.b && c1.g == c2.g && c1.r == c2.r;
        }

        public static bool operator !=(Color c1, Color c2)
        {
            if (ReferenceEquals(c1, c2))
                return false;
            
            if (ReferenceEquals(c1, null) || ReferenceEquals(c2, null))
                return true;
            
            return !(c1 == c2);
        }
        
    }
}