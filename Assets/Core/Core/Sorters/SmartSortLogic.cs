using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CardGame.Core.Sort
{
    public class SmartSortLogic
    {
        public static NumericColoredCard[][] SortBySmart(NumericColoredCard[] cards,
            out NumericColoredCard[] notSortedCards, int min, int max)
        {
            var cardList = cards.ToList();
            var nodeList = CreateCardNodes(cardList);
            CrateConnections(nodeList);

            for (int i = 0; i < nodeList.Count; i++)
            {
                var connections = new List<Connection>();
                FindConnection(nodeList[i], connections);

                Debug.Log(connections.Count);
                for (int j = 0; j < connections.Count; j++)
                {
                    Debug.Log(connections[j].fromNode.card.ToStringBuilder());
                }

                Debug.Log(" - ");
            }


            notSortedCards = null;
            return null;
        }

        private static void FindConnection(CardNode sourceNode, List<Connection> connections)
        {
            int handBrake = 0;
            while (true)
            {
                var connection = FindConnectableNode(sourceNode);
                
                bool isExits = connections.Exists(p => p == connection);

                if (connection != null && !isExits)
                {
                    connections.Add(connection);
                    sourceNode = connection.toNode;
                    handBrake++;
                    continue;
                }

                break;
            }
        }

        private static Connection FindConnectableNode(CardNode sourceNode)
        {
            var connections = sourceNode.connections;
            for (int i = 0; i < connections.Count; i++)
            {
                if (connections[i].conectionType == ConnectionType.Unkown)
                    continue;

                var toNodeConnections = connections[i].toNode.connections;
                for (int j = 0; j < toNodeConnections.Count; j++)
                {
                    if (toNodeConnections[j].conectionType == ConnectionType.Unkown)
                        continue;

                    if (toNodeConnections[j].toNode == sourceNode)
                        continue;
                    if (toNodeConnections[j] == connections[i])
                        continue;

                    if (toNodeConnections[j].conectionType == connections[i].conectionType)
                    {
                        return toNodeConnections[j];
                    }
                }
            }

            return null;
        }

        private static void CrateConnections(List<CardNode> nodeList)
        {
            for (int i = 0; i < nodeList.Count; i++)
            {
                var selectedNode = nodeList[i];
                for (int j = 0; j < nodeList.Count; j++)
                {
                    selectedNode.TryCreateConnection(nodeList[j]);
                }
            }
        }

        private static List<CardNode> CreateCardNodes(List<NumericColoredCard> cards)
        {
            var nodeList = new List<CardNode>(cards.Count);

            for (int i = 0; i < cards.Count; i++)
            {
                nodeList.Add(new CardNode(cards[i], new List<Connection>()));
            }

            return nodeList;
        }

        public static bool IsConnect(NumericColoredCard card1, NumericColoredCard card2,
            out ConnectionType connectionType)
        {
            if (Equals(card1, card2))
            {
                connectionType = ConnectionType.Unkown;
                return false;
            }

            if (card1 is JokerCard || card2 is JokerCard)
            {
                connectionType = ConnectionType.Unkown;
                return true;
            }

            if (card1.Color == card2.Color && card1.No + 1 == card2.No || card1.No - 1 == card2.No)
            {
                connectionType = ConnectionType.Numeric;
                return true;
            }

            if (card1.No == card2.No && card1.Color != card2.Color)
            {
                connectionType = ConnectionType.Colored;
                return true;
            }

            connectionType = ConnectionType.Unkown;

            return false;
        }

        public class CardNode
        {
            public NumericColoredCard card;
            public List<Connection> connections;

            public CardNode(NumericColoredCard card, List<Connection> connections)
            {
                this.card = card;
                this.connections = connections;
            }

            public bool TryCreateConnection(CardNode targetCardNode)
            {
                if (IsConnect(card, targetCardNode.card, out var connectType))
                {
                    var connection = new Connection()
                    {
                        fromNode = this,
                        toNode = targetCardNode,
                        conectionType = connectType
                    };
                    connections.Add(connection);
                    return true;
                }

                return false;
            }
        }

        public class Connection
        {
            public CardNode fromNode;
            public CardNode toNode;

            public ConnectionType conectionType;
        }

        public enum ConnectionType
        {
            Colored,
            Numeric,
            Unkown
        }
    }
}