using Assets.System;
using Assets.UI;
using Menu.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Menu
{
    public class Bootstrap : MonoBehaviour
    {
        private const int GameSceneIndex = 2;

        [Header("Buttons")]
        [SerializeField] private Button _playButton;

        [Header("Texts")]
        [SerializeField] private Text _pawsCountText;

        [SerializeField] private ItemSlider _slider;

        private ReactiveUI<int> _pawsCount;

        private void Start()
        {
            _pawsCount = new ReactiveUI<int>(PlayerSaves.CoinsInBank, _pawsCountText);

            _pawsCount.Init();
            _slider.Init();

            _playButton.onClick.AddListener(OnPlayButtonClicked);
        }

        private void OnDestroy()
        {
            _pawsCount.Dispose();

            _playButton.onClick.RemoveListener(OnPlayButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            new SceneLoader().Load(GameSceneIndex);
        }
    }
}
