using DG.Tweening;
using System.Collections.Generic;
using UI.Tweening;
using UI.Tweening.Factory;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Menu.UI
{
    public class ItemSlider : MonoBehaviour
    {
        private readonly Vector2 MinScale = new Vector2(0.5f, 0.5f);
        private readonly Vector2 MaxScale = Vector2.one;

        private const float MaxFade = 0, MinFade = 1;

        [SerializeField] private ItemFrameUi _itemPrefab;

        [SerializeField] private CanvasGroup _description;
        [SerializeField] private RectTransform _rectTransform;

        [SerializeField] private Text _descriptionText;
        [SerializeField] private Transform _content;

        private AnimationTween _animation;

        private List<ItemFrameUi> _items = new List<ItemFrameUi>();
        private ItemsLoader _itemLoader;

        private void Start()
        {
            _itemLoader = new ItemsLoader();
            _itemLoader.Load();

            InitAnimation();

            MakeItems();
            DisactivateDescription();
        }

        private void InitAnimation()
        {
            var startAnim = new FadeScalerFactory(_rectTransform, _description, MaxScale, MinFade);
            var endAnim = new FadeScalerFactory(_rectTransform, _description, MinScale, MaxFade);

            _animation = new AnimationTween(startAnim, endAnim);
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

            _animation.CompleteActiveAnimation();
        }

        private void ActivateDescription(Items item, ItemsData data)
        {
            if (data.IsGetted == false)
                _descriptionText.text = _itemLoader.GetNullItem().GetDescription(YandexGame.lang);
            else
                _descriptionText.text = item.GetDescription(YandexGame.lang);

            _description.gameObject.SetActive(true);
            _animation.Activate();
        }

        private void DisactivateDescription()
        {
            _animation.Disactivate(() => _description.gameObject?.SetActive(false));
        }
    }
}
