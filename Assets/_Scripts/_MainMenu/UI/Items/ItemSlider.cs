using DG.Tweening;
using System.Collections.Generic;
using Assets.UI.Tweening;
using Assets.UI.Tweening.Factory;
using UnityEngine;
using UnityEngine.UI;
using YG;
using System.Linq;

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
        [SerializeField] private Text _placeText;
        [SerializeField] private Transform _content;

        [SerializeField] private List<RoomInfo> _roomInfo = new List<RoomInfo> ();

        private AnimationTween _animation;

        private List<ItemFrameUi> _items = new List<ItemFrameUi>();
        private ItemsLoader _itemLoader;

        public void Init()
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

                _items.Add(element);

                element.OnMouseEnterEvent += ActivateDescription;
                element.OnMouseExitEvent += DisactivateDescription;
            }
        }

        private void OnDisable()
        {
            foreach (var item in _items)
            {
                if (item != null)
                {
                    item.OnMouseEnterEvent -= ActivateDescription;
                    item.OnMouseExitEvent -= DisactivateDescription;

                    if (item.gameObject != null)
                        Destroy(item.gameObject);
                }
            }

            _animation.CompleteActiveAnimation();
        }

        private void ActivateDescription(Items item, ItemsData data)
        {
            if (data.IsGetted == false)
                _descriptionText.text = _itemLoader.GetNullItem().GetDescription(YandexGame.lang);
            else
                _descriptionText.text = item.GetDescription(YandexGame.lang);

            _placeText.text = GetRoomText(item);
            _description.gameObject.SetActive(true);
            _animation.Activate();
        }

        private void DisactivateDescription()
        {
            _animation.Disactivate(() => _description.gameObject?.SetActive(false));
        }

        private string GetRoomText(Items needItem)
        {
            if (needItem.RoomPlace == 0)
                return "";

            var roomInfo = _roomInfo.FirstOrDefault(item => item.Room == needItem.RoomPlace);

            if (roomInfo == null)
                throw new System
                    .ArgumentException($"Not found Room type of {needItem.RoomPlace} in item {needItem}");

            return roomInfo.TextTranslate.GetText(YandexGame.lang);
        }
    }
}

[System.Serializable]
public class RoomInfo
{
    [field: SerializeField] public Room Room { get; private set; }
    [field: SerializeField] public TextTranslate TextTranslate { get; private set; }
    
}
