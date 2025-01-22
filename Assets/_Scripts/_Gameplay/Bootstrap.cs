using UnityEngine;
using Events;
using Assets.UI;
using UnityEngine.UI;
using Assets.Gameplay.UI;
using Assets.Gameplay.Minigame.Audio;

namespace Assets.Gameplay
{
    public class Bootstrap : MonoBehaviour
    {
        [Header("Initable")]
        [SerializeField] private UIHolder _uiHolder;
        [SerializeField] private GameCycle _gameCycle;
        [SerializeField] private Bank _bank;
        [SerializeField] private AudioHolder _audioHolder;

        void Start()
        {
            ItemsLoader loader = new ItemsLoader();
            loader.Load(true);

            _bank.Init();

            EventSystemHolder eventSystemHolder = new EventSystemHolder();

            _uiHolder.Init(eventSystemHolder);
            _gameCycle.Init(eventSystemHolder, loader);
            
            eventSystemHolder.Init(new EventsFabric().GetFullEventsList());

            _audioHolder.Init(eventSystemHolder);
        }

        private void OnDisable()
        {
            _uiHolder.OnDispose();
            _gameCycle.OnDispose();
        }
    }
}
