using DG.Tweening;
using System;
using UnityEngine;

namespace Assets.UI.Tweening.Factory
{
    public class MovingFactory : ITweenFactory
    {
        private float _duraction;
        private RectTransform _rectTransform;
        Vector2 _startPosition, _finishPosition;

        private Sequence _animation;

        public MovingFactory
            (RectTransform rectTransform, Vector2 startPosition, Vector2 finishPosition, float duraction = 0.5f)
        {
            _duraction = duraction;
            _rectTransform = rectTransform;

            _startPosition = startPosition;
            _finishPosition = finishPosition;
        }

        public Sequence GetSequence(Action callback = null)
        {
            _animation = DOTween.Sequence();

            _animation
                .Append(
                _rectTransform
                        .DOAnchorPos(_finishPosition, _duraction)
                        .From(_startPosition )
                        .SetEase(Ease.OutQuad))
                .OnComplete(() => callback?.Invoke());

            return _animation;
        }
    }
}
