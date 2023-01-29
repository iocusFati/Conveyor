using DG.Tweening;
using Infrastructure.Bootstrapper;
using TMPro;
using UnityEngine;

namespace GamePlay.PlayerFolder
{
    public class FoodPickText
    {
        private readonly ICoroutineRunner _coroutineRunner;
        
        private TextMeshPro _plusOneText;
        
        private readonly float _raiseBy;
        private readonly float _duration;
        private readonly float _defaultLocalPosY;
        private readonly Color _defaultColor;
        private Vector3 _initialPos;

        private Coroutine _fadeCoroutine;

        public FoodPickText(ICoroutineRunner coroutineRunner, IPlayerFactory playerFactory, 
            PlayerStaticData playerData)
        {
            _coroutineRunner = coroutineRunner;

            _raiseBy = playerData.RaiseBy;
            _duration = playerData.Duration;
            _defaultColor = playerData.DefaultColor;

            playerFactory.OnPlayerCreated += OnPlayerCreated;
        }

        private void OnPlayerCreated(Player player)
        {
            _plusOneText = player.FoodPickText;
            _initialPos = _plusOneText.transform.localPosition;
        }

        public void OnFoodPicked()
        {
            _plusOneText.transform.localPosition = _initialPos;
            _plusOneText.color = _defaultColor;
            _plusOneText.gameObject.SetActive(true);
            LookAtCamera();

            _plusOneText.transform
                .DOMoveY(_plusOneText.transform.position.y + _raiseBy, _duration)
                .OnComplete(() => _plusOneText.gameObject.SetActive(false));

            _plusOneText.DOColor(Color.clear, _duration);
        }

        private void LookAtCamera()
        {
            _plusOneText.transform.LookAt(Camera.main.transform);
            _plusOneText.transform.Rotate(new Vector3(0, 180, 0));
        }
    }
}