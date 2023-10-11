namespace CardGame.Core.Sort
{
    public interface ISort
    {
        public NumericColoredCard[][] Sort(NumericColoredCard[] cards,
            out NumericColoredCard[] notSortedCards, int minCardCount,int maxCardCount, int uniqColorCount, int minNumber,int maxNumber);
        
    }
}