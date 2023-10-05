using System;

namespace CardGame.Core
{
    public class Color : IComparable<Color>
    {
        public byte R { get; }

        public byte G { get; }

        public byte B { get; }
        
        public string RGB =>R.ToString() + G.ToString() + B.ToString();
        
        public Color(byte r, byte g, byte b)
        {
            this.R = r;
            this.G = g;
            this.B = b;
        }

        public override string ToString()
        {
            return "r:" + R + "g:" + G + "b:" + B;
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
                
            
            return c1.B == c2.B && c1.G == c2.G && c1.R == c2.R;
        }

        public override bool Equals(object obj)
        {
            var otherColor = (Color)obj;
            if (ReferenceEquals(otherColor, this))
                return true;

            if (ReferenceEquals(this, null) || ReferenceEquals(otherColor, null))
                return false;
            
            return otherColor.B == this.B && otherColor.G == this.G && otherColor.R == this.R;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
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