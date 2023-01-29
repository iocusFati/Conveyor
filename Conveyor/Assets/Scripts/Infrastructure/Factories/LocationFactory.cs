using System;
using GamePlay.FoodFolder;
using GamePlay.FoodFolder.FoodSpawnerFolder;
using GamePlay.LocationFolder;
using Infrastructure.Assets;
using Infrastructure.Bootstrapper;
using Infrastructure.States;

namespace Infrastructure.Factories
{
    public class LocationFactory : ILocationFactory
    {
        private readonly IAssets _assetProvider;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly FoodStaticData _foodStaticData;
        private readonly IGameStateMachine _gameStateMachine;

        public event Action<Location> OnLocationCreated;

        public LocationFactory(IAssets assetProvider, ICoroutineRunner coroutineRunner,
            FoodStaticData foodStaticData, IGameStateMachine gameStateMachine)
        {
            _assetProvider = assetProvider;
            _coroutineRunner = coroutineRunner;
            _foodStaticData = foodStaticData;
            _gameStateMachine = gameStateMachine;
        }

        public Location CreateLocation()
        {
            Location location = _assetProvider.Instantiate<Location>(AssetPaths.Location);
            
            var foodSpawner = location.GetComponentInChildren<FoodSpawner>();
            foodSpawner.Construct(_coroutineRunner, _gameStateMachine, _foodStaticData);
            location.FoodSpawner = foodSpawner;
            
            OnLocationCreated.Invoke(location);
            
            return location;
        }
    }
}