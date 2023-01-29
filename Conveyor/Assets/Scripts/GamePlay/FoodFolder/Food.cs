using System;
using GamePlay.FoodFolder.FoodSpawnerFolder.Pool;
using UnityEngine;

namespace GamePlay.FoodFolder
{
    public class Food : MonoBehaviour
    {
        private const string ReleaseTriggerTag = "ReleaseTrigger";

        [SerializeField] private FoodType _type;
        public FoodType Type => _type;
        
        private IFoodPool _foodPool;

        public void Construct(IFoodPool foodPool, FoodType type)
        {
            _foodPool = foodPool;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(ReleaseTriggerTag)) 
                Release();
        }

        public void Release() => 
            _foodPool.Release(this);
    }
}