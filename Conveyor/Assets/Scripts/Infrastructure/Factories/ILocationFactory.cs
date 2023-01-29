using System;
using GamePlay.FoodFolder.FoodSpawnerFolder;
using GamePlay.LocationFolder;
using Infrastructure.Services;

namespace Infrastructure.Factories
{
    public interface ILocationFactory : IService
    {
        Location CreateLocation();
        event Action<Location> OnLocationCreated;
    }
}