using UnityEngine;

namespace GamePlay.QuestFolder
{
    [CreateAssetMenu(fileName = "QuestData", menuName = "StaticData/QuestData")]
    public class QuestStaticData : ScriptableObject
    {
        public int MaxTargetProductsNum;
    }
}