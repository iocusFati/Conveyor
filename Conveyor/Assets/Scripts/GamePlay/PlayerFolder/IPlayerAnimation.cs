using System;
using UnityEngine;

namespace GamePlay.PlayerFolder
{
    public interface IPlayerAnimation
    {
        void StartReaching(Transform food, Action onReached, Action onArmDown);
        void Win();
        void Idle();
    }
}