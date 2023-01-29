using Utilities.MathServiceFolder;

namespace Infrastructure.Services.Input
{
    public class StandaloneInput : InputService
    {
        public StandaloneInput(IMathService mathService) : base(mathService) { }

        protected override bool Click() => 
            UnityEngine.Input.GetMouseButtonUp(0);
    }
}