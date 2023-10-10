using CardGame.View.Hand;

namespace CardGame.View.Card
{
    public interface ICardViewFinderModule
    {
        public Tile FindClosestTile();
    }
}