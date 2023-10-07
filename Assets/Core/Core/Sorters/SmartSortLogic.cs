using System.Collections.Generic;
using System.Linq;

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
            
            notSortedCards = null;
            return null;
        }

        private static void CrateConnections(List<CardNode> nodeList)
        {
            for (int i = 0; i < nodeList.Count; i++)
            {
                var selectedNode = nodeList[i];
                for (int j = 0; j < nodeList.Count; j++)
                {
                    selectedNode.TryAddNode(nodeList[j]);
                }
            }
        }

        private static List<CardNode> CreateCardNodes(List<NumericColoredCard> cards)
        {
            var nodeList = new List<CardNode>(cards.Count);

            for (int i = 0; i < cards.Count; i++)
            {
                nodeList.Add(new CardNode(cards[i], new List<CardNode>()));
            }

            return nodeList;
        }

        public static bool IsPer(NumericColoredCard card1, NumericColoredCard card2, out PerType perType)
        {
            if (Equals(card1, card2))
            {
                perType = PerType.Unkown;
                return false;
            }

            if (card1 is JokerCard || card2 is JokerCard)
            {
                perType = PerType.Unkown;
                return true;
            }

            if (card1.Color == card2.Color && card1.No + 1 == card2.No || card1.No - 1 == card2.No)
            {
                perType = PerType.Numeric;
                return true;
            }

            if (card1.No == card2.No && card1.Color != card2.Color)
            {
                perType = PerType.Colored;
                return true;
            }

            perType = PerType.Unkown;

            return false;
        }

        public class CardNode
        {
            public NumericColoredCard card;
            public List<CardNode> connectableNodes;

            public CardNode(NumericColoredCard card, List<CardNode> connectableNodes)
            {
                this.card = card;
                this.connectableNodes = connectableNodes;
            }

            public bool TryAddNode(CardNode targetCardNode)
            {
                if (IsPer(card, targetCardNode.card, out var perType))
                {
                    connectableNodes.Add(targetCardNode);
                    return true;
                }

                return false;
            }
        }

        public enum PerType
        {
            Colored,
            Numeric,
            Unkown
        }
    }
}