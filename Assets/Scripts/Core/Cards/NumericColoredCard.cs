﻿using System.Text;

namespace CardGame.Core
{
    public class NumericColoredCard : Card
    {
        public int No { get; private set; }
        public Color Color { get; private set; }

        public NumericColoredCard(int id, int no, Color color) : base(id)
        {
            No = no;
            Color = color;
        }

        public virtual StringBuilder ToStringBuilder()
        {
            var sb = new StringBuilder();
            sb.Append(nameof(Id));
            sb.Append(Id);
            sb.Append(nameof(No));
            sb.Append(No);
            sb.Append(nameof(Color));
            sb.Append(Color);
            return sb;
        }
    }
}