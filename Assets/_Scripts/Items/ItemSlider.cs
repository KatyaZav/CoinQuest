using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class ItemSlider : MonoBehaviour
{
    [SerializeField] private UiItemObject _itemPrefab;
    [SerializeField] private GameObject _description;
    [SerializeField] private Text _descriptionText;
    [SerializeField] private Transform _content;

    private List<UiItemObject> _items = new List<UiItemObject>();
    private ItemsLoader _itemLoader;

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
            var element = Instantiate(_itemPrefab, _content);
            element.SetItem(item);

            element.OnMouseEnterEvent += ActivateDescription;
            element.OnMouseExitEvent += DisactivateDescription;
        }
    }

    private void OnDisable()
    {
        foreach (var item in _items)
        {
            item.OnMouseEnterEvent += ActivateDescription;
            item.OnMouseExitEvent += DisactivateDescription;
        }
    }

    private void ActivateDescription(Items item)
    {
        _descriptionText.text = item.GetDescription(YandexGame.lang);
        _description.SetActive(true);
    }

    private void DisactivateDescription()
    {
        _description?.SetActive(false);
    }
}
