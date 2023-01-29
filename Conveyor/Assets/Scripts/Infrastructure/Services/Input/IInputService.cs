using GamePlay.FoodFolder;

namespace Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        public bool PickFood(out Food food);
    }
}