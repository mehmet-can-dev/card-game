namespace CardGame.Core
{
    public class Card
    {
        public int Id { get; private set; }

        public Card(int id)
        {
            this.Id = id;
        }
    }
}