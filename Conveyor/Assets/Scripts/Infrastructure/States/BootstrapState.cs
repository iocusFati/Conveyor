using GamePlay.FoodFolder;
using GamePlay.FoodFolder.FoodSpawnerFolder.Pool;
using GamePlay.FoodFolder.Moving;
using GamePlay.PlayerFolder;
using GamePlay.QuestFolder;
using Infrastructure.Assets;
using Infrastructure.Bootstrapper;
using Infrastructure.Factories;
using Infrastructure.Services;
using Infrastructure.Services.Input;
using Infrastructure.Services.StaticDataService;
using UI.Services;
using Unity.Mathematics;
using UnityEngine;
using Utilities.MathServiceFolder;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string InitialSceneName = "Initial";
        private const string MainSceneName = "Main";
        
        private readonly IGameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly AllServices _services;
        private readonly ITicker _ticker;

        public BootstrapState(IGameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices services,
            ICoroutineRunner coroutineRunner, ITicker ticker)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _coroutineRunner = coroutineRunner;
            _ticker = ticker;
            _services = services;

            RegisterServices(services);
        }

        public void Enter()
        {
            _sceneLoader.Load(InitialSceneName, OnLoaded);
        }

        public void Exit()
        {
            
        }

        private void OnLoaded()
        {
            var playerFactory = _services.Single<IPlayerFactory>();
            
            var foodPickText = new FoodPickText(_coroutineRunner, playerFactory,
                _services.Single<IStaticDataService>().PlayerData);
            var foodPicker = new FoodPicker(_services.Single<IInputService>(), _services.Single<IQuestFactory>(),
                _ticker, _gameStateMachine, playerFactory, _services.Single<ILocationFactory>(), foodPickText);
            
            _gameStateMachine.Enter<LoadLevelState, string>(MainSceneName);
        }

        private void RegisterServices(AllServices services)
        {
            var staticData = RegisterStaticData(services);
            var assets = services.RegisterService<IAssets>(
                new AssetProvider());


            var foodPool = RegisterFoodPool(services, assets, staticData);
            RegisterFactories(services, assets, staticData);

            var math = services.RegisterService<IMathService>(
                new MathService(foodPool, staticData.FoodData));
            
            services.RegisterService<IInputService>(InputService(math));
        }

        private IFoodPool RegisterFoodPool(AllServices services, IAssets assets, StaticDataService staticData)
        {
            var foodMovement = new FoodMovement(_gameStateMachine, _ticker, staticData.FoodData);
            return services.RegisterService<IFoodPool>(
                new FoodPool(assets, staticData.FoodData, foodMovement));
        }

        private void RegisterFactories(AllServices services, IAssets assets, StaticDataService staticData)
        {
            services.RegisterService<IPlayerFactory>(
                new PlayerFactory(assets, staticData.PlayerData));
            services.RegisterService<ILocationFactory>(
                new LocationFactory(assets, _coroutineRunner, staticData.FoodData, _gameStateMachine));
            services.RegisterService<IUIFactory>(
                new UIFactory(assets, staticData.UIData));
            services.RegisterService<IQuestFactory>(
                new QuestFactory(staticData.QuestData));
        }

        private StaticDataService RegisterStaticData(AllServices services)
        {
            var staticData = new StaticDataService();
            staticData.Initialize();
            services.RegisterService<IStaticDataService>(staticData);

            return staticData;
        }
        
        private static IInputService InputService(IMathService math) =>
            Application.isEditor
                ? new StandaloneInput(math)
                : new MobileInput(math);
    }
}