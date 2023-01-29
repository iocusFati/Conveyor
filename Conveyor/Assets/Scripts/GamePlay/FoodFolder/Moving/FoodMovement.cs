using System.Collections.Generic;
using Infrastructure.Bootstrapper;
using Infrastructure.States;
using UnityEngine;

namespace GamePlay.FoodFolder.Moving
{
    public class FoodMovement : IFoodMovement, ITickable
    {
        private readonly List<Food> _movingItems = new();
        private readonly IGameStateMachine _gameStateMachine;
        private float _speed;
        public Transform Conveyor { private get; set; }

        public FoodMovement(IGameStateMachine gameStateMachine, ITicker ticker, FoodStaticData foodStaticData)
        {
            _gameStateMachine = gameStateMachine;

            _speed = foodStaticData.Speed;
            
            ticker.AddTickable(this);
        }

        public void StartMoving(Food food) =>
            _movingItems.Add(food);

        public void Tick()
        {
            if (!_gameStateMachine.IsInGameLoop) return;
            
            foreach (var item in _movingItems)
            {
                Vector3 forward = item.transform.InverseTransformDirection(Conveyor.forward).normalized;
                item.transform.Translate(forward * (_speed * Time.deltaTime));
            }        
        }
    }
}