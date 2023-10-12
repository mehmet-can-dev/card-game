namespace CardGame.Core.Test
{
    public interface ITestableCards
    {
        public NumericColoredCard[] GetNotSortedCards();
        public NumericColoredCard[][] GetForwardNumericSortedCards(out NumericColoredCard[] notSortedCards);
        public NumericColoredCard[][] GetForwardColoredSortedCards(out NumericColoredCard[] notSortedCards);
        public NumericColoredCard[][] GetSmartSortedCards(out NumericColoredCard[] notSortedCards);
        
    }
}