using System.Collections.Generic;

namespace CardGame.Core.Sort.Recursive
{
        public class MatchedConnectionsData<T>
        {
            public List<T> matchedCards = new();
            public ConnectionType conectionType;
        }
}