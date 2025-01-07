using DG.Tweening;
using System;
using UnityEngine;

namespace Assets.UI.Tweening.Factory
{
    public class ScalerFactory : ITweenFactory
    {
        private float _scale, _duraction;
        private RectTransform _rectTransform;

        private Sequence _animation;

        public ScalerFactory(RectTransform rectTransform, float maxScale = 1.2f, float duraction = 0.5f)
        {
            _scale = maxScale;
            _duraction = duraction;
            _rectTransform = rectTransform;
        }

        public Sequence GetSequence(Action callback = null)
        {
            _animation = DOTween.Sequence();

            _animation
                .Append(_rectTransform
                    .DOScale(_scale, _duraction)
                    .SetEase(Ease.OutQuart))
                    .OnComplete(() => callback?.Invoke());

            return _animation;
        }
    }
}
