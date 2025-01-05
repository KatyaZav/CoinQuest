using UnityEngine;
using UnityEngine.UI;
using UI.Tween;

namespace Events
{
    public class UIEventPopup : MonoBehaviour
    {
        [SerializeField] private Text _descriptionText;
        [SerializeField] private RectTransform _popupTransform;
        [SerializeField] private CanvasGroup _popupGroup;

        private EventSystemHolder _eventSystemHolder;
        private BasePopupTween _tweenAnimation;

        public void Init(EventSystemHolder eventSystemHolder)
        {
            _eventSystemHolder = eventSystemHolder;
            _tweenAnimation = new BasePopupTween(new PopupInfo(_popupGroup, _popupTransform));

            _eventSystemHolder.ChangedEvent += OnEventChange;
        }

        public void Exit()
        {
            _eventSystemHolder.OnDisable();

            if (_tweenAnimation.IsActiveAnimation)
                _tweenAnimation.CompleteAnimation();
            
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
