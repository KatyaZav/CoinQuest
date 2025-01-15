using DG.Tweening;
using System;
using UnityEngine;

namespace Assets.UI.Tweening.Factory
{
    public class RotationFadeScalerFactory : ITweenFactory
    {
        private float _finishFade, _startFade, _duraction;
        private Vector2 _finishScale, _startScale;
        private Vector3 _finishRotation, _startRotation;
        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;

        private Sequence _animation;

        public RotationFadeScalerFactory
            (RectTransform rectTransform, CanvasGroup canvasGroup,
            Vector2 start, Vector2 finish,
            Vector3 startR, Vector3 finishR,
            float duraction = 0.5f)
        {
            _finishFade = 1;
            _startFade = 0;

            _startScale = start;
            _finishScale = finish;

            _finishRotation = startR;
            _startRotation = finishR;

            _duraction = duraction;
            _rectTransform = rectTransform;
            _canvasGroup = canvasGroup;
        }

        public Sequence GetSequence(Action callback = null)
        {
            _animation = DOTween.Sequence();

            _animation
                .Append(
                    _canvasGroup
                        .DOFade(_finishFade, _duraction)
                        .From(_startFade)
                        .SetEase(Ease.OutQuad))
                .Join(
                    _rectTransform
                        .transform.DOScale(_finishScale, _duraction)
                        .From(_startScale)
                        .SetEase(Ease.OutQuad))
                .Join(
                    _rectTransform
                    .DORotate(_finishRotation, _duraction, RotateMode.FastBeyond360)
                    .From(_startRotation)
                    .SetEase(Ease.OutQuad))
                .OnComplete(() => callback?.Invoke());

            return _animation;
        }
    }
}
