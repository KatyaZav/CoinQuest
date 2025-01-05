using UnityEngine;
using UnityEngine.EventSystems;
using UI.Tween;
using DG.Tweening;

namespace UI
{
    public class OnMouseScaleAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private float _maxScale = 1.2f, _duraction = 0.5f;
        [SerializeField] private RectTransform _rectTransform;

        private ScalerTween _scalerTween;
        //private BaseTween _scalerTween;

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            _scalerTween.Activate();
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            _scalerTween.Disactivate();
        }
        
        private void Start()
        {
            _rectTransform ??= GetComponent<RectTransform>();

            var _animation = DOTween.Sequence();

            _animation
                .Append(_rectTransform
                    .DOScale(Vector2.one * _maxScale, _duraction)
                    .SetEase(Ease.OutQuart));
            _animation.Pause();

            var _animation1 = DOTween.Sequence();

            _animation1
                .Append(_rectTransform
                    .DOScale(Vector2.one, _duraction)
                    .SetEase(Ease.OutQuart));
            _animation1.Pause();

            //_scalerTween = new BaseTween(_animation, _animation1);
            _scalerTween = new ScalerTween(_rectTransform, _maxScale, _duraction);
        }

        private void OnDisable()
        {
            _scalerTween.CompleteAnimation();
            //_scalerTween.CompleteActiveAnimation();
        }
    }
}
