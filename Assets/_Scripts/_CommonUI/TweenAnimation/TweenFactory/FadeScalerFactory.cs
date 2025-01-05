using DG.Tweening;
using System;
using UnityEngine;

namespace UI.Tweening.Factory
{
    public class FadeScalerFactory : ITweenFactory
    {
        private float _fade, _duraction;
        private Vector2 _scale;
        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;

        private Sequence _animation;

        public FadeScalerFactory
            (RectTransform rectTransform, CanvasGroup canvasGroup, Vector2 scale, float fade = 1, float duraction = 0.5f)
        {
            _fade = fade;
            _duraction = duraction;
            _scale = scale;
            _rectTransform = rectTransform;
            _canvasGroup = canvasGroup;
        }

        public Sequence GetSequence(Action callback = null)
        {
            _animation = DOTween.Sequence();

            _animation
                .Append(
                    _canvasGroup
                        .DOFade(_fade, _duraction)
                        .SetEase(Ease.OutQuad))
                .Join(
                    _rectTransform
                        .transform.DOScale(_scale, _duraction)
                        .SetEase(Ease.OutQuad))
                .OnComplete(() => callback?.Invoke());

            return _animation;
        }
    }
}
