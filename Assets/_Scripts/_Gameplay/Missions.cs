using Menu.UI;
using UnityEngine;

namespace Assets.Gameplay
{
    public class Missions : MonoBehaviour
    {
        [SerializeField] ItemSlider _slider;

        public void Open()
        {
            gameObject.SetActive(true);

            _slider.Init();
        }

        public void Close()
        {
            gameObject.SetActive(false); 
        }
    }
}
