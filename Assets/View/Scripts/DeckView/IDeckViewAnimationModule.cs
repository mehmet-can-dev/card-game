using System.Collections;
using CardGame.View.Card;
using CardGame.View.Hand;

namespace CardGame.View.Deck
{
    public interface IDeckViewAnimationModule
    {
        public IEnumerator DeckToPlayerHandAnimation(CardView spawnedCard,
            Tile connectTile);
    }
}