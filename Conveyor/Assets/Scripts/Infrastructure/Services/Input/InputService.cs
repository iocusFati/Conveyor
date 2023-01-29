using GamePlay.FoodFolder;
using UnityEngine;
using Utilities.MathServiceFolder;

namespace Infrastructure.Services.Input
{
    public abstract class InputService : IInputService
    {
        private readonly IMathService _mathService;
        
        private Vector2 _mousePosition1;
        private Vector2 _mousePosition2;
        private float _magnitudeBetweenPositions;
        private bool _mouseButtonIsDown;

        protected abstract bool Click();

        protected InputService(IMathService mathService)
        {
            _mathService = mathService;
        }

        public bool PickFood(out Food foodResult)
        {
            if (!Click())
            {
                foodResult = default;
                return false;
            }
            
            Ray ray = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);

            _mathService.CheckForClosest(ray, out var food);
            if (food is not null)
            {
                foodResult = food;
                return true;
            }
            
            foodResult = default;
            return false;
        }
    }
}