using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Data;

namespace Events
{
    public class UIEventPopup : MonoBehaviour
    {
        [SerializeField] private Text _descriptionText;
        [SerializeField] private RectTransform _popup;
        [SerializeField] private float _duraction;

        private EventSystemHolder _eventSystemHolder;
        private Tween _tween;

        public void Init(EventSystemHolder eventSystemHolder)
        {
            _eventSystemHolder = eventSystemHolder;

            _eventSystemHolder.ChangedEvent += OnEventChange;
        }

        public void Exit()
        {
            _eventSystemHolder.ChangedEvent -= OnEventChange;
        }

        public void Close()
        {
            _tween.Kill();

            _tween = _popup
                .DOScale(Vector2.zero, _duraction)
                .SetEase(Ease.InOutBounce)
                .OnComplete(() => gameObject.SetActive(false));
        }

        private void OnEventChange(EventData data)
        {
            Debug.Log($"Activate event {data.GetDescription("en")}");
            _descriptionText.text = data.GetDescription(YG.YandexGame.lang);

            gameObject.SetActive(true);

            _tween.Kill();

            _tween = _popup
                .DOScale(Vector2.one, _duraction)
                .SetEase(Ease.InOutBounce);
        }
    }
}
