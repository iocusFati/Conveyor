using DG.Tweening;
using TMPro;
using UI.Data;
using UnityEngine;

namespace UI.HUD
{
    public class HUD_Animation
    {
        private readonly TextMeshProUGUI _score;
        
        private readonly float _scaleScoreTo;
        private readonly float _scaleScoreDuration;

        public HUD_Animation(UIStaticData uiStaticData ,TextMeshProUGUI score)
        {
            _score = score;

            _scaleScoreTo = uiStaticData.ScaleScoreTo;
            _scaleScoreDuration = uiStaticData.ScaleScoreDuration;
        }

        public void PingPongScaleScore() =>
            _score.transform
                .DOScale(_scaleScoreTo, _scaleScoreDuration)
                .OnComplete(() => _score.transform.DOScale(Vector3.one, _scaleScoreDuration));
    }
}