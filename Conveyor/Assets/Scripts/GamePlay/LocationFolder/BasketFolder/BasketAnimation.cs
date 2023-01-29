using UnityEngine;

namespace GamePlay.LocationFolder.BasketFolder
{
    public class BasketAnimation
    {
        private const string ShakeAnimationName = "Shake";
        private readonly Animator _animator;

        private Transform _basket;
        private Quaternion _initialRot;

        public BasketAnimation(Animator animator)
        {
            _animator = animator;
        }
        
        public void Shake()
        {
            _animator.SetTrigger(ShakeAnimationName);
        }
    }
}