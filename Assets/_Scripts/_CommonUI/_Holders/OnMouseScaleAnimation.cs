using UnityEngine;
using UnityEngine.EventSystems;
using Assets.UI.Tweening;
using Assets.UI.Tweening.Factory;

namespace Assets.UI
{
    public class OnMouseScaleAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private float _maxScale = 1.2f, _standartScale = 1, _duraction = 0.5f;
        [SerializeField] private RectTransform _rectTransform;

        private AnimationTween _scalerTween;

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

            var startAnimation = new ScalerFactory(_rectTransform, _maxScale, _duraction);
            var stopAnimation = new ScalerFactory(_rectTransform, _standartScale, _duraction);

            _scalerTween = new AnimationTween(startAnimation, stopAnimation);
        }

        private void OnDisable()
        {
            _scalerTween.CompleteActiveAnimation();
        }
    }
}
