using UnityEngine;
using UnityEngine.Serialization;

namespace GamePlay.FoodFolder
{
    [CreateAssetMenu(fileName = "FoodData", menuName = "StaticData/FoodData")]
    public class FoodStaticData : ScriptableObject
    {
        [Header("Spawner")]
        public int NumberOfEachKind;
        public float MinTimeToNextSpawn;
        public float MaxTimeToNextSpawn;
        public float Speed;

        [Header("Basket")]
        public int MaxProductsInBasket;

        [Header("Selection")]
        public float SelectThreshold;
    }
}