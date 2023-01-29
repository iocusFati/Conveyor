using System;
using CameraBehaviour;
using GamePlay.LocationFolder;
using GamePlay.QuestFolder;
using Infrastructure.Bootstrapper;
using Infrastructure.Factories;
using Infrastructure.Services.StaticDataService;
using UI;
using UI.HUD;
using UI.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPointTag = "InitialPoint";
        
        private readonly IGameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IPlayerFactory _playerFactory;
        private readonly ILocationFactory _locationFactory;
        private readonly CameraMovement _cameraMovement;
        private readonly IUIFactory _uiFactory;
        private readonly IQuestFactory _questFactory;
        private readonly IStaticDataService _staticData;
        
        private Curtain _curtain;
        private Location _location;
        private HUD_Display _HUD;

        public LoadLevelState(IGameStateMachine gameStateMachine,
            SceneLoader sceneLoader,
            IPlayerFactory playerFactory,
            ILocationFactory locationFactory,
            IUIFactory uiFactory,
            IQuestFactory questFactory,
            IStaticDataService staticData,
            CameraMovement cameraMovement)
        {
            _uiFactory = uiFactory;
            _questFactory = questFactory;
            _staticData = staticData;
            _playerFactory = playerFactory;

            _cameraMovement = cameraMovement;
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _locationFactory = locationFactory;
        }
        public void Enter(string sceneName)
        {
            Action onAppeared;
            _curtain ??= _uiFactory.CreateCurtain();
            
            if (sceneName == SceneManager.GetActiveScene().name) 
                onAppeared = Reload;
            else
                onAppeared = () =>
                    _sceneLoader.Load(sceneName, OnLoaded);
            
            _curtain.Show(onAppeared);
        }

        public void Exit() => 
            _curtain.Hide();

        private void OnLoaded()
        {
            _location = _locationFactory.CreateLocation();
            _location.Basket.Construct(_staticData.FoodData);
            
            _cameraMovement.Initialize(_playerFactory, _staticData.CameraData);
            Vector3 initialPoint = GameObject.FindGameObjectWithTag(InitialPointTag).transform.position;

            _playerFactory.CreatePlayer(initialPoint);
            _HUD = _uiFactory.CreateHUD();
            _HUD.Construct(_gameStateMachine, _staticData.UIData);
            CreateQuest();

            _gameStateMachine.Enter<GameLoopState>();
        }

        private void Reload()
        {
            _cameraMovement.GetToInitialPosition();
            _cameraMovement.GetInitialRotation();
            foreach (var activate in _location.DeactivateObjects) 
                activate.SetActive(true);
            
            _location.Basket.GoToDefault();
            CreateQuest();
            
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void CreateQuest()
        {
            var quest = _questFactory.Create();
            _HUD.Initialize(quest);
            _HUD.ShowTheQuest();
        }
    }
}