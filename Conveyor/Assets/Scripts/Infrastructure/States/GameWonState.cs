using System.Collections.Generic;
using CameraBehaviour;
using GamePlay.FoodFolder.FoodSpawnerFolder.Pool;
using GamePlay.PlayerFolder;
using Infrastructure.Bootstrapper;
using Infrastructure.Factories;
using UI.HUD;
using UI.Services;
using UnityEngine;

namespace Infrastructure.States
{
    public class GameWonState : IState
    {
        private readonly CameraMovement _cameraMovement;
        private readonly IFoodPool _foodPool;
        
        private List<GameObject> _deactivateObjects;
        private HUD_Display _HUD;
        private IPlayerAnimation _playerAnimation;

        public GameWonState(IFoodPool foodPool, IUIFactory uiFactory, IPlayerFactory playerFactory,
            ILocationFactory locationFactory, CameraMovement cameraMovement)
        {
            _cameraMovement = cameraMovement;
            _foodPool = foodPool;
            
            uiFactory.OnHUDCreated += display => _HUD = display;
            playerFactory.OnPlayerAnimationCreated += animation => _playerAnimation = animation;
            locationFactory.OnLocationCreated += location => _deactivateObjects = location.DeactivateObjects;
        }
        
        public void Enter()
        {
            _cameraMovement.GetToWinPosition(
                () => _HUD.ShowWin());
            
            Activate(false);

            _HUD.HideQuest();
            _foodPool.ReleaseAll();
            _playerAnimation.Win();
        }

        private void Activate(bool activation)
        {
            foreach (var deactivate in _deactivateObjects) 
                deactivate.SetActive(activation);
        }

        public void Exit()
        {
            _HUD.HideWin();
            _playerAnimation.Idle();
        }
    }
}