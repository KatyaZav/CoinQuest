using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemsLoader
{
    private const string PathItems = "Items";
    private const string NoneItem = "Closed";

    private List<Items> _items;
    private Items _noneItem;

    public bool IsLoaded { get; private set;}

    public Items GetNullItem() => _noneItem;

    public Items GetItemByName(string name)
    {
        foreach (var item in _items)
        {
            if (item.Name == name)
                return item;
        }

        Debug.LogError("Not found item");
        return _noneItem;
    }

    public void Load()
    {
        IsLoaded = true;

        _items = Resources.LoadAll(PathItems, typeof(Items)).Cast<Items>().ToList();
        _noneItem = Resources.Load<Items>(NoneItem);
    }
}
