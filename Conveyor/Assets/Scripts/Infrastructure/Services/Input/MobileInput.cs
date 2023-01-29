using UnityEngine;
using Utilities.MathServiceFolder;

namespace Infrastructure.Services.Input
{
    public class MobileInput : InputService
    {
        public MobileInput(IMathService mathService) : base(mathService) { }

        protected override bool Click() => 
            UnityEngine.Input.GetTouch(0).phase == TouchPhase.Ended;
    }
}