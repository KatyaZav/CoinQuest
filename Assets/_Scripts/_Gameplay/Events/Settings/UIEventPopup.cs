using UnityEngine;
using UnityEngine.UI;
using UI.Tweening;
using UI.Tweening.Factory;

namespace Events
{
    public class UIEventPopup : MonoBehaviour
    {
        private readonly Vector2 _minScale = Vector2.zero;
        private readonly Vector2 _maxScale = Vector2.one;

        private const float _minFade = 0;
        private const float _maxFade = 0.9f;

        [SerializeField] private Text _descriptionText;
        [SerializeField] private RectTransform _popupTransform;
        [SerializeField] private CanvasGroup _popupGroup;

        private EventSystemHolder _eventSystemHolder;
        private AnimationTween _tweenAnimation;

        public void Init(EventSystemHolder eventSystemHolder)
        {
            _eventSystemHolder = eventSystemHolder;

            var start = new PopupAnimationFactory(new PopupInfo(_popupGroup, _popupTransform), _minScale, _maxScale, _minFade, _maxFade);
            var end = new PopupAnimationFactory(new PopupInfo(_popupGroup, _popupTransform), _maxScale, _minScale, _maxFade, _minFade);

            _tweenAnimation = new AnimationTween(start, end);

            _eventSystemHolder.ChangedEvent += OnEventChange;
        }

        public void Exit()
        {
            _eventSystemHolder.OnDisable();

            if (_tweenAnimation.IsActiveAnimation)
                _tweenAnimation.CompleteActiveAnimation();
            
            _eventSystemHolder.ChangedEvent -= OnEventChange;
        }

        public void Close()
        {
            _tweenAnimation.Disactivate(() => _popupGroup.gameObject.SetActive(false));
        }

        private void OnEventChange(EventData data)
        {
            _popupGroup.gameObject.SetActive(true);

            _descriptionText.text = data.GetDescription(YG.YandexGame.lang);
            _tweenAnimation.Activate();
        }
    }
}
