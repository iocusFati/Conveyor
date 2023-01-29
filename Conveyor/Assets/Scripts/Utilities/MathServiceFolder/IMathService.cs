using GamePlay.FoodFolder;
using Infrastructure.Services;
using UnityEngine;

namespace Utilities.MathServiceFolder
{
    public interface IMathService : IService
    {
        void CheckForClosest(Ray ray, out Food result);
    }
}