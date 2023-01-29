using System;
using GamePlay.PlayerFolder;
using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.Bootstrapper
{
    public interface IPlayerFactory : IService
    {
        void CreatePlayer(Vector3 at);
        event Action<Player> OnPlayerCreated;
        event Action<IPlayerAnimation> OnPlayerAnimationCreated;
    }
}