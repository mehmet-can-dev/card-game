using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGame.Core.Sort.Recursive
{
    public abstract class RecursiveSortBase : ISort
    {
       
        protected virtual List<PredictionData> GetWithJokerPredictionList()
        {
            throw new Exception("Assign Prediction");
            return null;
        }

        protected virtual List<PredictionData> GetWithoutJokerPredictionList()
        {
            throw new Exception("Assign Prediction");
            return null;
        }

        public NumericColoredCard[][] Sort(NumericColoredCard[] cards, out NumericColoredCard[] notSortedCards,
            int minCardCount,
            int maxCardCount, int uniqColorCount, int minNumber, int maxNumber)
        {
            List<MatchedConnectionsData<NumericColoredCard>> matchedCardsList =
                new List<MatchedConnectionsData<NumericColoredCard>>();
            List<NumericColoredCard> notMatched = new List<NumericColoredCard>();

            var cardList = cards.ToList();
            var nodeList = RecursiveLogic.CreateCardNodes(cardList);

            var jokerNodes = nodeList.Where(p => p.card is JokerCard).ToList();

            nodeList.RemoveAll(p => p.card is JokerCard);

            RecursiveLogic.CrateConnections(nodeList);

            nodeList = nodeList.OrderBy(p => p.card.No).ToList();

            FindMatchedWithoutJokers(minCardCount, nodeList, matchedCardsList);

            nodeList.RemoveAll(p => p.isSelected);

            FindMatchedWithJokerFromNotMatched(minCardCount, jokerNodes, nodeList, matchedCardsList);

            ConnectionTypeUtilities.TryConnectJokerToAlreadyMatchedCards(uniqColorCount, maxNumber, nodeList,
                matchedCardsList);

            nodeList.RemoveAll(p => p.isSelected);

            for (int i = 0; i < nodeList.Count; i++)
            {
                notMatched.Add(nodeList[i].card);
            }

            notSortedCards = notMatched.ToArray();
            var matchedCardsArray = new NumericColoredCard[matchedCardsList.Count][];
            for (int i = 0; i < matchedCardsList.Count; i++)
            {
                matchedCardsArray[i] = new NumericColoredCard[matchedCardsList[i].matchedCards.Count];
                for (int j = 0; j < matchedCardsList[i].matchedCards.Count; j++)
                {
                    matchedCardsArray[i][j] = matchedCardsList[i].matchedCards[j];
                }
            }

            return matchedCardsArray;
        }


        private void FindMatchedWithJokerFromNotMatched(int min, List<CardNodeData> jokerNodes,
            List<CardNodeData> nodeList, List<MatchedConnectionsData<NumericColoredCard>> matchedCardsList)
        {
            for (int j = 0; j < jokerNodes.Count; j++)
            {
                nodeList.Add(jokerNodes[j]);
                RecursiveLogic.CrateConnections(nodeList);

                var list = new List<MatchedConnectionsData<CardNodeData>>();
                for (int k = 0; k < nodeList.Count; k++)
                {
                    if (nodeList[k].card is JokerCard)
                        continue;

                    if (nodeList[k].isSelected)
                        continue;

                    RecursiveLogic.TryFindSingleNodeMatches(list, nodeList, k, GetWithJokerPredictionList());
                }

                RecursiveLogic.FindMaxLenghtMatched(min, matchedCardsList, list);

                nodeList.RemoveAll(p => p.isSelected);
            }
        }

        private void FindMatchedWithoutJokers(int min, List<CardNodeData> nodeList,
            List<MatchedConnectionsData<NumericColoredCard>> matchedCardsList)
        {
            for (int k = 0; k < nodeList.Count; k++)
            {
                if (nodeList[k].card is JokerCard)
                    continue;

                if (nodeList[k].isSelected)
                    continue;

                var list = new List<MatchedConnectionsData<CardNodeData>>();


                RecursiveLogic.TryFindSingleNodeMatches(list, nodeList, k,
                    GetWithoutJokerPredictionList());

                RecursiveLogic.FindMaxLenghtMatched(min, matchedCardsList, list);
            }
        }
    }
}