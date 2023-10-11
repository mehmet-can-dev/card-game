using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGame.Core.Sort.Recursive
{
    public static class RecursiveLogic
    {
        public static void TryFindSingleNodeMatches(List<MatchedConnectionsData<CardNodeData>> matchesList,
            List<CardNodeData> nodeList, int k,
            List<PredictionData> predictions)
        {
            for (int i = 0; i < predictions.Count; i++)
            {
                var l = new MatchedConnectionsData<CardNodeData>();
                l.matchedCards.Add(nodeList[k]);
                l.conectionType = predictions[i].PredictionConnectionType;
                matchesList.Add(l);
                FindMatchesNodes(nodeList[k], matchesList, l.matchedCards,
                    predictions[i].Prediction, l.conectionType);
            }
        }

        public static void FindMatchesNodes(CardNodeData nodeData, List<MatchedConnectionsData<CardNodeData>> totalList,
            List<CardNodeData> connections,
            Func<ConnectionData, bool> prediction, ConnectionType type)
        {
            var selectedConnection =
                nodeData.connections.Where(prediction)
                    .ToList();

            for (int i = 0; i < selectedConnection.Count; i++)
            {
                if (selectedConnection[i].ToNodeData.isSelected)
                    continue;

                if (selectedConnection[i].ToNodeData == nodeData)
                    continue;

                if (connections.Contains(selectedConnection[i].ToNodeData))
                    continue;

                if (selectedConnection[i].ToNodeData.card is JokerCard)
                {
                    CheckJokerConditions(totalList, connections, prediction, type, selectedConnection, i);
                    return;
                }

                if (connections.Exists(p =>
                        p.card.No == selectedConnection[i].ToNodeData.card.No &&
                        p.card.Color == selectedConnection[i].ToNodeData.card.Color))
                    continue;

                if (i > 1)
                {
                    var l = new MatchedConnectionsData<CardNodeData>();
                    l.matchedCards.Add(selectedConnection[i].ToNodeData);
                    l.conectionType = type;
                    totalList.Add(l);
                    FindMatchesNodes(selectedConnection[i].ToNodeData, totalList, l.matchedCards, prediction, type);
                }
                else
                {
                    connections.Add(selectedConnection[i].ToNodeData);
                    FindMatchesNodes(selectedConnection[i].ToNodeData, totalList, connections, prediction, type);
                }
            }
        }

        public static void CheckJokerConditions(List<MatchedConnectionsData<CardNodeData>> totalList,
            List<CardNodeData> connections,
            Func<ConnectionData, bool> prediction, ConnectionType type,
            List<ConnectionData> selectedConnection, int i)
        {
            var selectedCard = selectedConnection[i].FromNodeData.card;

            ConnectionData conneciton = null;
            switch (type)
            {
                case ConnectionType.Colored:
                    List<Color> colorList = new List<Color>();
                    for (int j = 0; j < connections.Count; j++)
                    {
                        if (connections[j].card is JokerCard)
                            continue;

                        colorList.Add(connections[j].card.Color);
                    }

                    conneciton = LookConnectionsSpecific(selectedConnection[i].ToNodeData,
                        p => !colorList.Contains(p.Color) && p.No == selectedCard.No);

                    break;
                case ConnectionType.Numeric:
                    conneciton = LookConnectionsSpecific(selectedConnection[i].ToNodeData,
                        p => p.Color == selectedCard.Color && p.No == selectedCard.No - 2);
                    break;
                case ConnectionType.Unknown:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }


            if (conneciton != null)
            {
                connections.Add(selectedConnection[i].ToNodeData);

                if (!connections.Contains(conneciton.ToNodeData))
                    connections.Add(conneciton.ToNodeData);

                FindMatchesNodes(conneciton.ToNodeData, totalList, connections, prediction, type);
            }
        }

        private static ConnectionData LookConnectionsSpecific(CardNodeData nodeData,
            Func<NumericColoredCard, bool> presicion)
        {
            for (int i = 0; i < nodeData.connections.Count; i++)
            {
                if (nodeData.connections[i].ToNodeData.card is JokerCard)
                    continue;

                NumericColoredCard card = nodeData.connections[i].ToNodeData.card;
                if (presicion(card))
                {
                    return nodeData.connections[i];
                }
            }

            return null;
        }

        public static void FindMaxLenghtMatched(int min,
            List<MatchedConnectionsData<NumericColoredCard>> matchedCardsList,
            List<MatchedConnectionsData<CardNodeData>> list)
        {
            var longestList = list.OrderByDescending(p => p.matchedCards.Count).First();

            if (longestList.matchedCards.Count >= min)
            {
                var matchList = new MatchedConnectionsData<NumericColoredCard>();
                for (int i = 0; i < longestList.matchedCards.Count; i++)
                {
                    longestList.matchedCards[i].isSelected = true;
                    matchList.matchedCards.Add(longestList.matchedCards[i].card);
                    matchList.conectionType = longestList.conectionType;
                }

                matchedCardsList.Add(matchList);
            }
        }

        public static void CrateConnections(List<CardNodeData> nodeList)
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

        public static List<CardNodeData> CreateCardNodes(List<NumericColoredCard> cards)
        {
            var nodeList = new List<CardNodeData>(cards.Count);

            for (int i = 0; i < cards.Count; i++)
            {
                nodeList.Add(new CardNodeData(cards[i], new List<ConnectionData>()));
            }

            return nodeList;
        }
    }
}