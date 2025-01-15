using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Gameplay
{
    public class PlayersChoice : MonoBehaviour
    {
        public Action ItemDropedEvent, MimikGettedEvent, ItemCollectedEvent;

        [SerializeField] ItemGenerator _generator;
        [SerializeField] Button _yes, _no;


        public void GetCoin()
        {
            PlayerSaves.MakeGetted(_generator.Item);

            //_itemView.ActivateCollectAnimation();
        }

        public void DropCoin()
        {
            ItemDropedEvent?.Invoke();
        }

        public void CollectCoin()
        {
            if (_generator.IsMimik)
            {
                MimikGettedEvent?.Invoke();
            }
            else
            {
                ItemCollectedEvent?.Invoke();
            }
        }

        public void DisableButtons()
        {
            Activate(false);
        }

        public void ActivateButtons()
        {
            Activate(true);
        }

        void Activate(bool isTrue)
        {
            _yes.gameObject.SetActive(isTrue);
            _no.gameObject.SetActive(isTrue);
        }
    }
}
