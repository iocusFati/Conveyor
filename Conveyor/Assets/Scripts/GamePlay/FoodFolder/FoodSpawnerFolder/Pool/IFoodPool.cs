using System.Collections.Generic;
using Infrastructure.Services;

namespace GamePlay.FoodFolder.FoodSpawnerFolder.Pool
{
    public interface IFoodPool : IService
    {
        void GetFood(out Food food);
        void Release(Food food);
        void ReleaseAll();
        void Initialize(IFoodSpawner foodSpawner);
        List<Food> ActiveFood { get; }
    }
}