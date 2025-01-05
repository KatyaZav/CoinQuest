using DG.Tweening;
using System;
using UnityEngine;

namespace UI.Tween
{
    public class ScalerTween
    {
        private float _maxScale, _duraction;
        private RectTransform _rectTransform;

        private Sequence _animation;

        public ScalerTween(RectTransform rectTransform, float maxScale = 1.2f, float duraction = 0.5f)
        {
            _maxScale = maxScale;
            _duraction = duraction;
            _rectTransform = rectTransform;
        }

        public bool IsActiveAnimation => _animation != null && _animation.active;

        public void Activate(Action callback = null)
        {
            KillActiveAnimation();

            _animation = DOTween.Sequence();

            _animation
                .Append(_rectTransform
                    .DOScale(Vector2.one * _maxScale, _duraction)
                    .SetEase(Ease.OutQuart));
        }

        public void Disactivate(Action callback = null)
        {
            KillActiveAnimation();

            _animation = DOTween.Sequence();

            _animation
                .Append(_rectTransform
                    .DOScale(Vector2.one, _duraction)
                    .SetEase(Ease.OutQuart));

        }

        public void CompleteAnimation()
        {
            if (IsActiveAnimation)
                _animation.Complete(true);
        }

        public void KillActiveAnimation()
        {
            if (IsActiveAnimation)
            {
                _animation.Kill();
            }
        }
    }
}
