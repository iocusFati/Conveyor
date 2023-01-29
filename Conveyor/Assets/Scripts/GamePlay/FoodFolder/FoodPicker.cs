using GamePlay.LocationFolder.BasketFolder;
using GamePlay.PlayerFolder;
using GamePlay.QuestFolder;
using Infrastructure.Bootstrapper;
using Infrastructure.Factories;
using Infrastructure.Services.Input;
using Infrastructure.States;
using UI.HUD;
using UnityEngine;

namespace GamePlay.FoodFolder
{
    public class FoodPicker : ITickable
    {
        private readonly IInputService _inputService;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly FoodPickText _pickText;

        private IPlayerAnimation _playerAnimation;
        private HUD_Animation _hudAnimation;
        private Quest _quest;
        private Basket _basket;
        private bool _isReaching;

        public FoodPicker(IInputService inputService, IQuestFactory questFactory, ITicker ticker,
            IGameStateMachine gameStateMachine, IPlayerFactory playerFactory, ILocationFactory locationFactory,
            FoodPickText pickText)
        {
            _inputService = inputService;
            _gameStateMachine = gameStateMachine;
            _pickText = pickText;

            ticker.AddTickable(this);

            questFactory.OnQuestCreated += quest => _quest = quest;  
            playerFactory.OnPlayerAnimationCreated += animation => _playerAnimation = animation;
            locationFactory.OnLocationCreated += location => _basket = location.Basket;
        }
        
        public void Tick()
        {
            if (!_gameStateMachine.IsInGameLoop || _isReaching) return;
            if (!_inputService.PickFood(out Food food)) return;
            
            _isReaching = true;

            _playerAnimation.StartReaching(food.transform, 
                () =>
            {
                Debug.Log("Finished reaching");
                    
                _pickText.OnFoodPicked();
                _quest.TryUpdateProgress(food.Type, out bool reachedTarget);
                _basket.PutInTheBasket(food.Type);
                    
                if (reachedTarget) 
                    _gameStateMachine.Enter<GameWonState>();
                    
                food.Release();
            }, 
                () => _isReaching = false);
        }
    }
}