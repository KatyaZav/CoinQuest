using Assets.System.Audio;
using Events;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Assets.Gameplay.Minigame.Audio
{
    public class AudioHolder : MonoBehaviour
    {
        [SerializeField] private GameObject _audioSourceObject;
        [SerializeField] private AudioMixerGroup _audioMixerGroup;

        [Header("Audio")]
        [SerializeField] private AudioData _buttonsSound;
        [SerializeField] private AudioData _catSound, _eventPopupSound, _newItemSound;

        private Button[] _buttons;

        private List<AudioSource> _sourceList = new List<AudioSource>();
        private EventSystemHolder _eventSystemHolder;

        private AudioPlayer _mimikSoundPlayer, _eventPopupSoundPlayer, _itemSoundPlayer, _buttonSoundPlayer; 

        public void Init(EventSystemHolder eventSystemHolder)
        {
            _itemSoundPlayer = new AudioPlayer(GetAudioSource(), _newItemSound);
            _mimikSoundPlayer = new AudioPlayer(GetAudioSource(), _catSound);
            _eventPopupSoundPlayer = new AudioPlayer(GetAudioSource(), _eventPopupSound);
            _buttonSoundPlayer = new AudioPlayer(GetAudioSource(), _buttonsSound);

            _eventSystemHolder = eventSystemHolder;            
            _buttons = FindObjectsOfType<Button>();

            foreach (Button button in _buttons)
            {
                button.onClick.AddListener(OnButtonClick);
            }                       

            _eventSystemHolder.ChangedEvent += OnChangeEvent;
            SubscriptionKeeper.GettedNewEvent += OnGettedNew;
            SubscriptionKeeper.MimikActivated += OnMimikActivate;

        }

        private void OnDisable()
        {
            SubscriptionKeeper.MimikActivated -= OnMimikActivate;
            SubscriptionKeeper.GettedNewEvent -= OnGettedNew;
            _eventSystemHolder.ChangedEvent -= OnChangeEvent;

            foreach (Button button in _buttons)
            {
                if (button != null)
                    button.onClick.RemoveListener(OnButtonClick);
            }
        }

        private AudioSource GetAudioSource()
        {           
            var item = _audioSourceObject.AddComponent<AudioSource>();

            item.outputAudioMixerGroup = _audioMixerGroup;
            item.playOnAwake = false;

            _sourceList.Add(item);
            return item;
        }

        private void OnGettedNew(Items items)
        {
            _itemSoundPlayer.PlaySound();
        }

        private void OnChangeEvent(EventData data)
        {
            _eventPopupSoundPlayer.PlaySound();
        }

        private void OnButtonClick()
        {
            _buttonSoundPlayer.PlaySound();
        }

        private void OnMimikActivate(Action action)
        {
            _mimikSoundPlayer.PlaySound();
        }
    }
}

