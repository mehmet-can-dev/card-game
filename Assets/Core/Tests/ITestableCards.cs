namespace CardGame.Core.Test
{
    public interface ITestableCards
    {
        public NumericColoredCard[] GetNotSortedCards();
        public NumericColoredCard[][] GetNumericSortedCards(out NumericColoredCard[] notSortedCards);
        public NumericColoredCard[][] GetColoredSortedCards(out NumericColoredCard[] notSortedCards);
        public NumericColoredCard[][] GetSmartSortedCards(out NumericColoredCard[] notSortedCards);
        
        
        
    }
}