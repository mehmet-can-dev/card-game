using CardGame.View.DataModels;
using UnityEngine;

namespace CardGame.View.SO
{
    [CreateAssetMenu(fileName = "BuilderSettings", menuName = "CardGame/BuilderSettings", order = 0)]
    public class BuilderSettingsSO : ScriptableObject
    {
        public BuilderCountData BuilderCountData;
        public BuilderViewData BuilderViewData;
        public SortViewData SortViewData;
    }
}