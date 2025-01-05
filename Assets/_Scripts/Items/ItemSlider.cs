using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Menu.UI
{
    public class ItemSlider : MonoBehaviour
    {
        [SerializeField] private ItemFrameUi _itemPrefab;
        [SerializeField] private GameObject _description;
        [SerializeField] private Text _descriptionText;
        [SerializeField] private Transform _content;

        [SerializeField] private ScalerFadeTween _animation;

        private List<ItemFrameUi> _items = new List<ItemFrameUi>();
        private ItemsLoader _itemLoader;

        private Sequence _anim;

        private void Start()
        {
            _itemLoader = new ItemsLoader();
            _itemLoader.Load();

            MakeItems();
            DisactivateDescription();
        }

        private void MakeItems()
        {
            foreach (var item in _itemLoader.GetItemsList())
            {
                ItemsData data;
                var element = Instantiate(_itemPrefab, _content);

                if (PlayerSaves.TryGetItemContain(item, out data) == false)
                {
                    Debug.Log("Add new item!");
                    data = new ItemsData(_itemLoader.GetNullItem().ID);
                }

                if (data.IsSaw == false)
                {
                    element.SetItem(_itemLoader.GetNullItem(), data);
                }
                else
                {
                    element.SetItem(item, data);

                    if (data.IsGetted == false)
                        element.SetDarkColor();
                    else
                        element.SetNormalColor();
                }


                element.OnMouseEnterEvent += ActivateDescription;
                element.OnMouseExitEvent += DisactivateDescription;
            }
        }

        private void OnDisable()
        {
            foreach (var item in _items)
            {
                item.OnMouseEnterEvent -= ActivateDescription;
                item.OnMouseExitEvent -= DisactivateDescription;
            }
        }

        private void ActivateDescription(Items item, ItemsData data)
        {
            if (data.IsGetted == false)
                _descriptionText.text = _itemLoader.GetNullItem().GetDescription(YandexGame.lang);
            else
                _descriptionText.text = item.GetDescription(YandexGame.lang);

            _description.SetActive(true);
            _animation.Open();
        }

        private void DisactivateDescription()
        {
            _animation.Hide(() => _description?.SetActive(false));
        }
    }
}
