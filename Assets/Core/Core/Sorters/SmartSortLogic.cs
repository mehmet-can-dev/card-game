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

            for (int k = 0; k < nodeList.Count; k++)
            {
                var list = new List<List<CardNode>>();
                var l = new List<CardNode>()
                {
                    nodeList[k]
                };
                list.Add(l);
                FindMatchesNodes(nodeList[k], list, l, ConnectionType.Numeric);
                var l2 = new List<CardNode>()
                {
                    nodeList[k]
                };
                list.Add(l);
                FindMatchesNodes(nodeList[k], list, l2, ConnectionType.Colored);

                var longestList = list.OrderByDescending(p => p.Count).First();

                if (longestList.Count >= min)
                    for (int i = 0; i < longestList.Count; i++)
                    {
                        longestList[i].isSelected = true;
                        Debug.Log(longestList[i].card.ToStringBuilder());
                    }

                Debug.Log("  -  ");
            }

            notSortedCards = null;
            return null;
        }

        private static void FindMatchesNodes(CardNode node, List<List<CardNode>> totalList, List<CardNode> connections,
            ConnectionType type)
        {
            var selectedConnection =
                node.connections.Where(p => p.conectionType == type || p.conectionType == ConnectionType.Unkown)
                    .ToList();

            for (int i = 0; i < selectedConnection.Count; i++)
            {
                if (selectedConnection[i].toNode.isSelected)
                    continue;

                if (selectedConnection[i].toNode == node)
                    continue;

                if (connections.Contains(selectedConnection[i].toNode))
                    continue;

                if (connections.Exists(p =>
                        p.card.No == selectedConnection[i].toNode.card.No &&
                        p.card.Color == selectedConnection[i].toNode.card.Color))
                    continue;

                if (i > 1)
                {
                    var l = new List<CardNode> { selectedConnection[i].toNode };
                    totalList.Add(l);
                    FindMatchesNodes(selectedConnection[i].toNode, totalList, l, type);
                }
                else
                {
                    connections.Add(selectedConnection[i].toNode);
                    FindMatchesNodes(selectedConnection[i].toNode, totalList, connections, type);
                }
            }
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

            if (card1.Color == card2.Color && (card1.No + 1 == card2.No || card1.No - 1 == card2.No))
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
            public bool isSelected;

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