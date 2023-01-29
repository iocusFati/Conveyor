using GamePlay.FoodFolder.FoodSpawnerFolder;
using GamePlay.FoodFolder.FoodSpawnerFolder.Pool;
using Infrastructure.Factories;

namespace Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly IFoodPool _foodPool;
        private IFoodSpawner _foodSpawner;

        public GameLoopState(ILocationFactory locationFactory, IFoodPool foodPool)
        {
            _foodPool = foodPool;
            locationFactory.OnLocationCreated += location => _foodSpawner = location.FoodSpawner;
        }

        public void Enter()
        {
            _foodPool.Initialize(_foodSpawner);
            _foodSpawner.StartSpawning();
        }

        public void Exit() => 
            _foodSpawner.StopSpawning();
    }
}