using System;

namespace CardGame.Core.Sort.Recursive
{
    public class PredictionData
    {
        public Func<ConnectionData, bool> Prediction;
        public ConnectionType PredictionConnectionType;
    }
}