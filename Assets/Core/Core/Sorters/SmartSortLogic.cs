using System;
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

            var jokerNodes = nodeList.Where(p => p.card is JokerCard).ToList();
            nodeList.RemoveAll(p => p.card is JokerCard);

            CrateConnections(nodeList);

            nodeList = nodeList.OrderBy(p => p.card.No).ToList();

            Debug.Log(" Matches");

            for (int k = 0; k < nodeList.Count; k++)
            {
                if (nodeList[k].card is JokerCard)
                    continue;

                var list = new List<List<CardNode>>();
                var l = new List<CardNode>()
                {
                    nodeList[k]
                };
                list.Add(l);
                FindMatchesNodes(nodeList[k], list, l,
                    p => p.conectionType == ConnectionType.Colored, ConnectionType.Colored);
                var l2 = new List<CardNode>()
                {
                    nodeList[k]
                };
                list.Add(l2);
                FindMatchesNodes(nodeList[k], list, l2,
                    p => p.conectionType == ConnectionType.Numeric, ConnectionType.Numeric);

                var longestList = list.OrderByDescending(p => p.Count).First();

                if (longestList.Count >= min)
                {
                    for (int i = 0; i < longestList.Count; i++)
                    {
                        longestList[i].isSelected = true;
                        Debug.Log(longestList[i].card.ToStringBuilder());
                    }

                    Debug.Log("  -  ");
                }
            }

            nodeList.RemoveAll(p => p.isSelected);

            Debug.Log("Not Matches");

            for (int i = 0; i < nodeList.Count; i++)
            {
                Debug.Log(nodeList[i].card.ToStringBuilder());
            }


            Debug.Log("Matches by Joker");
            nodeList.AddRange(jokerNodes);
            CrateConnections(nodeList);
            for (int k = 0; k < nodeList.Count; k++)
            {
                if (nodeList[k].card is JokerCard)
                    continue;

                var list2 = new List<List<CardNode>>();
                var l3 = new List<CardNode>()
                {
                    nodeList[k]
                };
                list2.Add(l3);
                FindMatchesNodes(nodeList[k], list2, l3,
                    p => p.conectionType is ConnectionType.Colored or ConnectionType.Unkown, ConnectionType.Colored);
                var l4 = new List<CardNode>()
                {
                    nodeList[k]
                };
                list2.Add(l4);
                FindMatchesNodes(nodeList[k], list2, l4,
                    p => p.conectionType is ConnectionType.Numeric or ConnectionType.Unkown, ConnectionType.Numeric);

                var longestList = list2.OrderByDescending(p => p.Count).First();
                if (longestList.Count >= min)
                {
                    for (int i = 0; i < longestList.Count; i++)
                    {
                        Debug.Log(longestList[i].card.ToStringBuilder());
                        longestList[i].isSelected = true;
                    }

                    Debug.Log("-p-");
                }
            }


            notSortedCards = null;
            return null;
        }

        private static void FindMatchesNodes(CardNode node, List<List<CardNode>> totalList, List<CardNode> connections,
            Func<Connection, bool> prediction, ConnectionType type)
        {
            var selectedConnection =
                node.connections.Where(prediction)
                    .ToList();

            for (int i = 0; i < selectedConnection.Count; i++)
            {
                if (selectedConnection[i].toNode.isSelected)
                    continue;

                if (selectedConnection[i].toNode == node)
                    continue;

                if (connections.Contains(selectedConnection[i].toNode))
                    continue;

                if (selectedConnection[i].toNode.card is JokerCard)
                {
                    if (CheckJokerConditions(totalList, connections, prediction, type, selectedConnection, i)) return;
                }
                else
                {
                    if (connections.Exists(p =>
                            p.card.No == selectedConnection[i].toNode.card.No &&
                            p.card.Color == selectedConnection[i].toNode.card.Color))
                        continue;
                }

                if (i > 1)
                {
                    var l = new List<CardNode> { selectedConnection[i].toNode };
                    totalList.Add(l);
                    FindMatchesNodes(selectedConnection[i].toNode, totalList, l, prediction, type);
                }
                else
                {
                    connections.Add(selectedConnection[i].toNode);
                    FindMatchesNodes(selectedConnection[i].toNode, totalList, connections, prediction, type);
                }
            }
        }

        private static bool CheckJokerConditions(List<List<CardNode>> totalList, List<CardNode> connections,
            Func<Connection, bool> prediction, ConnectionType type,
            List<Connection> selectedConnection, int i)
        {
            connections.Add(selectedConnection[i].toNode);
            var selectedCard = selectedConnection[i].fromNode.card;

            Connection conneciton = null;
            switch (type)
            {
                case ConnectionType.Colored:
                    List<Color> colorList = new List<Color>();
                    for (int j = 0; j < connections.Count; j++)
                    {
                        if (connections[i].card is JokerCard)
                            continue;
                        colorList.Add(connections[i].card.Color);
                    }

                    conneciton = LookConnectionsSpecific(selectedConnection[i].toNode,
                        p => !colorList.Contains(p.Color) && p.No == selectedCard.No);

                    break;
                case ConnectionType.Numeric:
                    conneciton = LookConnectionsSpecific(selectedConnection[i].toNode,
                        p => p.Color == selectedCard.Color && p.No == selectedCard.No + 2);
                    break;
                case ConnectionType.Unkown:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            if (conneciton != null)
            {
                if (!connections.Contains(conneciton.toNode))
                    connections.Add(conneciton.toNode);
                FindMatchesNodes(conneciton.toNode, totalList, connections, prediction, type);
                return true;
            }

            return false;
        }

        private static Connection LookConnectionsSpecific(CardNode node, Func<NumericColoredCard, bool> presicion)
        {
            for (int i = 0; i < node.connections.Count; i++)
            {
                if (node.connections[i].toNode.card is JokerCard)
                    continue;

                NumericColoredCard card = node.connections[i].toNode.card;
                if (presicion(card))
                {
                    return node.connections[i];
                }
            }

            return null;
        }

        private static void CrateConnections(List<CardNode> nodeList)
        {
            for (int i = 0; i < nodeList.Count; i++)
            {
                var selectedNode = nodeList[i];

                selectedNode.connections.Clear();
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

            if (card1.Color == card2.Color && (card1.No + 1 == card2.No))
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