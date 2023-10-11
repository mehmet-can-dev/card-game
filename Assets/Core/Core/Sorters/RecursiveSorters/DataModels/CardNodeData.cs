using System.Collections.Generic;

namespace CardGame.Core.Sort.Recursive
{
        public class CardNodeData
        {
            public NumericColoredCard card;
            public List<ConnectionData> connections;
            public bool isSelected;

            public CardNodeData(NumericColoredCard card, List<ConnectionData> connections)
            {
                this.card = card;
                this.connections = connections;
            }

            public bool TryCreateConnection(CardNodeData targetCardNodeData)
            {
                if (ConnectionTypeUtilities.IsConnect(card, targetCardNodeData.card, out var connectType))
                {
                    var connection = new ConnectionData()
                    {
                        FromNodeData = this,
                        ToNodeData = targetCardNodeData,
                        conectionType = connectType
                    };
                    connections.Add(connection);
                    return true;
                }

                return false;
            }
        
    }
}