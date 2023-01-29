using System.Collections.Generic;
using System.Linq;
using GamePlay.FoodFolder.Moving;
using Infrastructure.Assets;
using Random = UnityEngine.Random;

namespace GamePlay.FoodFolder.FoodSpawnerFolder.Pool
{
    public class FoodPool : IFoodPool
    {
        private readonly List<Food> _foodPool = new();
        private readonly IAssets _assetProvider;
        private readonly IFoodMovement _movement;

        private readonly int _numberOfFoodPerKind;

        public List<Food> ActiveFood { get; } = new();

        public FoodPool(IAssets assetProvider, FoodStaticData foodStaticData, IFoodMovement movement)
        {
            _assetProvider = assetProvider;
            _movement = movement;
            _numberOfFoodPerKind = foodStaticData.NumberOfEachKind;
        }

        public void Initialize(IFoodSpawner foodSpawner)
        {
            foodSpawner.Initialize(this, _movement);
            foreach (var path in AssetPaths.FoodPaths)
            {
                for (int i = 0; i < _numberOfFoodPerKind; i++)
                {
                    var food = _assetProvider.Instantiate<Food>(path.Key);
                    food.Construct(this, path.Value);
                    _movement.StartMoving(food);
                    _foodPool.Add(food);
                }
            }

            foreach (var food in _foodPool) 
                Release(food);
        }

        public void GetFood(out Food food)
        {
            var inactiveFood = GetInactive();
            var activeFoodLength = inactiveFood.Length;

            if (inactiveFood.Any())
            {
                food = inactiveFood.ElementAt(Random.Range(0, activeFoodLength));
                food.gameObject.SetActive(true);
            }
            else 
                food = SpawnRandomFood();
            
            ActiveFood.Add(food);
        }

        public void ReleaseAll()
        {
            var onlyActive = _foodPool.Where(food => food.gameObject.activeSelf);
            foreach (var product in onlyActive) 
                Release(product);
        }

        public void Release(Food food)
        {
            food.gameObject.SetActive(false);
            ActiveFood.Remove(food);
        }

        private Food SpawnRandomFood()
        {
            var food = _assetProvider.Instantiate<Food>(GetRandomFoodPath());
            _movement.StartMoving(food);
            return food;
        }

        private static string GetRandomFoodPath() => 
            AssetPaths.FoodPaths.ElementAt(Random.Range(0, AssetPaths.FoodPaths.Count)).Key;

        private Food[] GetInactive() => 
            _foodPool.Where(food => !food.gameObject.activeSelf).ToArray();
    }
}