using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Random = UnityEngine.Random;

namespace GamePlay.PlayerFolder
{
    public class PlayerAnimation : MonoBehaviour, IPlayerAnimation
    {
        private const string _winAnimationName = "WinID";
        private const string _idleAnimationName = "Idle";

        [SerializeField] private Animator _animator;
        [SerializeField] private TwoBoneIKConstraint _foodReachConstraint;
        [SerializeField] private Transform _target;

        private float _weightInterpolant;
        private int _winAnimationNumber;
        private bool _pickUpAnimRunning;

        private int RandomWinID() => 
            Random.Range(1, _winAnimationNumber);

        public void Construct(PlayerStaticData playerStaticData)
        {
            _weightInterpolant = playerStaticData.WeightInterpolant;
            _winAnimationNumber = playerStaticData.WinAnimationNumber;
        }

        public void Win() => 
            _animator.SetInteger(_winAnimationName, RandomWinID());

        public void Idle()
        {
            _animator.SetTrigger(_idleAnimationName);
            _animator.SetInteger(_winAnimationName, 0);
        }

        public void StartReaching(Transform food, Action onReached, Action onArmDown) => 
            StartCoroutine(Reach(food, onReached, onArmDown));
        
        private IEnumerator Reach(Transform food, Action onReached, Action onArmDown)
        {
            while (_foodReachConstraint.weight < 1)
            {
                _foodReachConstraint.weight += _weightInterpolant * Time.deltaTime;
                _target.position = food.position;

                yield return null;
            }

            StartCoroutine(GoToInitial(onArmDown));
            onReached.Invoke();
        }

        private IEnumerator GoToInitial(Action onArmDown)
        {
            while (_foodReachConstraint.weight > 0)
            {
                _foodReachConstraint.weight -= _weightInterpolant * Time.deltaTime;

                yield return null;
            }
            
            onArmDown.Invoke();
        }
    }
}