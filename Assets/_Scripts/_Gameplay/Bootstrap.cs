using UnityEngine;
using Events;
using Assets.UI;
using UnityEngine.UI;
using Assets.Gameplay.UI;

namespace Assets.Gameplay
{
    public class Bootstrap : MonoBehaviour
    {
        [Header("Initable")]
        [SerializeField] private UIHolder _uiHolder;
        [SerializeField] private GameCycle _gameCycle;
        [SerializeField] private Bank _bank;

        void Start()
        {
            ItemsLoader loader = new ItemsLoader();
            loader.Load(true);

            _bank.Init();

            EventSystemHolder eventSystemHolder = new EventSystemHolder();

            _uiHolder.Init(eventSystemHolder);
            _gameCycle.Init(eventSystemHolder, loader);
            
            eventSystemHolder.Init(new EventsFabric().GetFullEventsList());
        }

        private void OnDisable()
        {
            _uiHolder.OnDispose();
            _gameCycle.OnDispose();
        }
    }
}
