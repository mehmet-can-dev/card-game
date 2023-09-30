
namespace CardGame.Core
{
    public class CardBase
    {
        public int Id { get; private set; }

        public CardBase(int id)
        {
            this.Id = id;
        }
    }
}
