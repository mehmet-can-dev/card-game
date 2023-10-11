using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGame.Core.Sort.Recursive
{
    public static class ConnectionTypeUtilities
    {
        public static bool IsConnect(NumericColoredCard card1, NumericColoredCard card2,
            out ConnectionType connectionType)
        {
            if (Equals(card1, card2))
            {
                connectionType = ConnectionType.Unknown;
                return false;
            }

            if (card1 is JokerCard || card2 is JokerCard)
            {
                connectionType = ConnectionType.Unknown;
                return true;
            }

            if (card1.Color == card2.Color && (card1.No - 1 == card2.No))
            {
                connectionType = ConnectionType.Numeric;
                return true;
            }

            if (card1.No == card2.No && card1.Color != card2.Color)
            {
                connectionType = ConnectionType.Colored;
                return true;
            }

            connectionType = ConnectionType.Unknown;

            return false;
        }


        public static void TryConnectJokerToAlreadyMatchedCards(int uniqColorCount, int maxNumber,
            List<CardNodeData> nodeList,
            List<MatchedConnectionsData<NumericColoredCard>> matchedCardsList)
        {
            for (int i = 0; i < nodeList.Count; i++)
            {
                if (nodeList[i].card is JokerCard)
                {
                    for (int j = 0; j < matchedCardsList.Count; j++)
                    {
                        if (matchedCardsList[j].conectionType == ConnectionType.Colored)
                        {
                            if (matchedCardsList[j].matchedCards.Count >= uniqColorCount)
                                continue;

                            //ToDo Check Already Matches
                            if (matchedCardsList[j].matchedCards.Last() is not JokerCard)
                            {
                                nodeList[i].isSelected = true;
                                matchedCardsList[j].matchedCards.Add(nodeList[i].card);
                                break;
                            }

                            if (matchedCardsList[j].matchedCards.First() is not JokerCard)
                            {
                                nodeList[i].isSelected = true;
                                matchedCardsList[j].matchedCards.Insert(0, nodeList[i].card);
                                break;
                            }
                        }
                        else if (matchedCardsList[j].conectionType == ConnectionType.Numeric)
                        {
                            var firstCard = matchedCardsList[j].matchedCards.First();
                            if (firstCard.No != maxNumber && firstCard is not JokerCard)
                            {
                                nodeList[i].isSelected = true;
                                matchedCardsList[j].matchedCards.Insert(0, nodeList[i].card);
                                break;
                            }

                            var lastCard = matchedCardsList[j].matchedCards.Last();

                            if (lastCard.No != 1 && lastCard is not JokerCard)
                            {
                                nodeList[i].isSelected = true;
                                matchedCardsList[j].matchedCards.Add(nodeList[i].card);
                                break;
                            }
                        }
                        else
                        {
                            throw new Exception("Unknown Sorted Type");
                        }
                    }
                }
            }

            nodeList.RemoveAll(p => p.isSelected);
        }

        public static ConnectionData TryFindConnectionData(List<CardNodeData> connections, ConnectionType type,
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
                    conneciton = RecursiveLogic.LookConnectionsSpecific(selectedConnection[i].ToNodeData,
                        p => !colorList.Contains(p.Color) && p.No == selectedCard.No);
                    break;
                case ConnectionType.Numeric:
                    conneciton = RecursiveLogic.LookConnectionsSpecific(selectedConnection[i].ToNodeData,
                        p => p.Color == selectedCard.Color && p.No == selectedCard.No - 2);
                    break;
                case ConnectionType.Unknown:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            return conneciton;
        }
    }
}