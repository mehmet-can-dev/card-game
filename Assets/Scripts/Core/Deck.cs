public class Deck
{
    public NumericColoredCard[] Cards { get; private set; }

    public Deck(NumericColoredCard[] cards)
    {
        Cards = cards;
    }
}