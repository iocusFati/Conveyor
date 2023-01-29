using System;
using System.Collections;
using Infrastructure.Bootstrapper;
using UnityEngine;

namespace CameraBehaviour
{
    public class CameraMovement
    {
        private const string CameraWinPointTag = "CameraWinPoint";

        private readonly ICoroutineRunner _coroutineRunner;
        
        private Camera _camera;
        private Vector3 _cameraWinPos;
        private Vector3 _cameraInitialPos;

        private float _destinationModifier;
        private float _moveInterpolant;
        private float _rotateInterpolant;

        private Transform _player;
        private float _percentageBeforeStop;
        private Quaternion _cameraInitialRot;

        public CameraMovement(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }
        
        public void Initialize(IPlayerFactory playerFactory, CameraStaticData cameraStaticData)
        {
            _camera = Camera.main;
            var camTransform = _camera.transform;
            _cameraInitialPos = camTransform.position;
            _cameraInitialRot = camTransform.rotation;
            _cameraWinPos = GameObject.FindGameObjectWithTag(CameraWinPointTag).transform.position;

            _percentageBeforeStop = cameraStaticData.PercentageBeforeStop;
            _moveInterpolant = cameraStaticData.MoveInterpolant;
            _rotateInterpolant = cameraStaticData.RotateInterpolant;
            
            playerFactory.OnPlayerCreated += player => _player = player.CameraLookAtPoint;
        }

        public void GetToWinPosition(Action onComplete) => 
            _coroutineRunner.StartCoroutine(MoveTowardsWinPos(onComplete));

        public void GetToInitialPosition() => 
            _camera.transform.position = _cameraInitialPos;

        public void GetInitialRotation() =>
            _camera.transform.localRotation = _cameraInitialRot;

        private IEnumerator MoveTowardsWinPos(Action onComplete)
        {
            Vector3 initialPos = _camera.transform.position;
            Coroutine coroutine = _coroutineRunner.StartCoroutine(RotateTowardsPlayer());

            while (true)
            {
                var camPos = _camera.transform.position;
                _camera.transform.position = Vector3.Lerp(
                    camPos, _cameraWinPos, _moveInterpolant * Time.deltaTime);

                if (PercentageDone(initialPos, camPos, initialPos) > _percentageBeforeStop)
                    break;
                
                LookAtPlayer();

                yield return null;
            }
            
            _coroutineRunner.StopCoroutine(coroutine);
            onComplete.Invoke();
        }

        private float PercentageDone(Vector3 vector1, Vector3 vector2, Vector3 initialPos)
        {
            float distanceToWantedPos = Vector3.Distance(initialPos, _cameraWinPos);
            float distanceMade = Vector3.Distance(vector1, vector2);
            float percentageDone = distanceMade * 100 / distanceToWantedPos;
            
            return percentageDone;
        }

        private IEnumerator RotateTowardsPlayer()
        {
            while (true)
            {
                LookAtPlayer();

                yield return null;
            }
        }

        private void LookAtPlayer() => 
            _camera.transform.localRotation = Quaternion.Slerp(
                _camera.transform.localRotation, LookAtPlayerRotation(), _rotateInterpolant * Time.deltaTime);

        private Quaternion LookAtPlayerRotation()
        {
            Vector3 lookDirection = _player.position - _camera.transform.position;
            return Quaternion.LookRotation(lookDirection);
        }
    }
}