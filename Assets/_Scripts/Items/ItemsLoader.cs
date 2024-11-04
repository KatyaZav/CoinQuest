using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemsLoader
{
    private const string PathItems = "Items";
    private const string NoneItem = "Closed";

    private List<ItemsInfo> _items = new List<ItemsInfo>();
    private ItemsInfo _noneItem;

    private Dictionary<Rare, List<ItemsInfo>> _dictionaryItems = new Dictionary<Rare, List<ItemsInfo>>();

    public bool IsLoaded { get; private set;}

    public ItemsInfo GetNullItem() => _noneItem;
    public List<ItemsInfo> GetItemsList() => _items;
    public List<ItemsInfo> GetListByRare(Rare rare) => _dictionaryItems[rare];

    public ItemsInfo GetItemByName(string name)
    {
        foreach (var item in _items)
        {
            if (item.Name == name)
                return item;
        }

        Debug.LogError("Not found item");
        return _noneItem;
    }

    public void Load(bool needToMakeList = false)
    {
        IsLoaded = true;

        var _itemsZero = Resources.LoadAll<Items>(PathItems);

        foreach (var e in _itemsZero)
        {
            var item = new ItemsInfo(e.name, e.Icon, e.GetDescription("ru"), e.GetDescription("en"), e.GetRare);
            _items.Add(item);
        }

        var noneItem = Resources.Load<Items>(NoneItem);
        _noneItem = new ItemsInfo(
            noneItem.name, noneItem.Icon, noneItem.GetDescription("ru"), noneItem.GetDescription("en"),
            noneItem.GetRare);

        if (needToMakeList)
        {
            foreach (var item in _items)
            {
                if (_dictionaryItems.ContainsKey(item.GetRare) == false)
                {
                    _dictionaryItems[item.GetRare] = new List<ItemsInfo>();
                }

                _dictionaryItems[item.GetRare].Add(item);
            }
        }
    }
}
