using GamePlay.FoodFolder.FoodSpawnerFolder.Pool;
using GamePlay.FoodFolder.Moving;
using Infrastructure.Services;

namespace GamePlay.FoodFolder.FoodSpawnerFolder
{
    public interface IFoodSpawner : IService
    {
        void Initialize(IFoodPool foodPool, IFoodMovement movement);
        void StartSpawning();
        void StopSpawning();
    }
}