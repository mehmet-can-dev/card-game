using System.Collections.Generic;

namespace CardGame.Core.Sort.Recursive
{
    public class SmartRecursiveSort : RecursiveSortBase
    {
        private static readonly List<PredictionData> SmartPrediction_WithJoker = new()
        {
            new PredictionData
            {
                PredictionConnectionType = ConnectionType.Colored,
                Prediction = (p) => p.conectionType is ConnectionType.Colored or ConnectionType.Unknown
            },
            new PredictionData
            {
                PredictionConnectionType = ConnectionType.Numeric,
                Prediction = (p) => p.conectionType is ConnectionType.Numeric or ConnectionType.Unknown
            }
        };

        private static readonly List<PredictionData> SmartPrediction_WithoutJoker = new()
        {
            new PredictionData
            {
                PredictionConnectionType = ConnectionType.Colored,
                Prediction = (p) => p.conectionType is ConnectionType.Colored
            },
            new PredictionData
            {
                PredictionConnectionType = ConnectionType.Numeric,
                Prediction = (p) => p.conectionType is ConnectionType.Numeric
            }
        };

        protected override List<PredictionData> GetWithJokerPredictionList()
        {
            return SmartPrediction_WithJoker;
        }

        protected override List<PredictionData> GetWithoutJokerPredictionList()
        {
            return SmartPrediction_WithoutJoker;
        }
    }
}