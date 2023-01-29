using GamePlay.FoodFolder;
using GamePlay.FoodFolder.FoodSpawnerFolder.Pool;
using UnityEngine;

namespace Utilities.MathServiceFolder
{
    public class MathService : IMathService
    {
        private readonly IFoodPool _foodPool;
        private readonly float _threshold;

        public MathService(IFoodPool foodPool, FoodStaticData foodStaticData)
        {
            _foodPool = foodPool;

            _threshold = foodStaticData.SelectThreshold;
        }

        public void CheckForClosest(Ray ray, out Food result)
        {
            Food closestFood = null;
            float closestPercentage = 0;
            
            foreach (var food in _foodPool.ActiveFood)
            {
                var rayDir = ray.direction;
                var vectorToFood = food.transform.position - ray.origin;

                var lookPercentage = Vector3.Dot(rayDir.normalized, vectorToFood.normalized);

                if (lookPercentage > _threshold && lookPercentage > closestPercentage)
                {
                    closestFood = food;
                    closestPercentage = lookPercentage;
                }
            }

            result = closestFood;
        }
    }
}