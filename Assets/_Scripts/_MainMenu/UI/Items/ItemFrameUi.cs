using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Menu.UI
{
    public class ItemFrameUi : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private readonly Color DarkColor = new Color(11, 48, 0, 255);
        private readonly Color NormalColor = Color.white;

        private readonly Dictionary<Rare, Color32> ImageColors = new Dictionary<Rare, Color32>()
    {
        {Rare.Usual, new Color32(255, 246, 162, 255) },
        {Rare.Rare, new Color32(208, 255, 251, 255) },
        {Rare.Legendary, new Color32(196, 248, 188, 255) }
    };

        public Action<Items, ItemsData> OnMouseEnterEvent;
        public Action OnMouseExitEvent;

        [SerializeField] private Image _image;
        [SerializeField] private Image _rampImage;

        private ItemsData _itemData;
        private Items _item;

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            OnMouseEnterEvent?.Invoke(_item, _itemData);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            OnMouseExitEvent?.Invoke();
        }

        public void SetItem(Items item, ItemsData data)
        {
            _itemData = data;
            _item = item;
            _image.sprite = item.Icon;

            if (item.name != "Closed")
            {
                _rampImage.color = ImageColors[item.GetRare];
            }
        }

        public void SetNormalColor()
        {
            _image.color = NormalColor;
        }

        public void SetDarkColor()
        {
            _image.color = DarkColor;
        }
    }
}
