using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemsLoader
{
    private const string PathItems = "Items";
    private const string NoneItem = "Closed";

    private List<Items> _items = new List<Items>();
    private Items _noneItem;

    private Dictionary<Rare, List<Items>> _dictionaryItems = new Dictionary<Rare, List<Items>>();

    public bool IsLoaded { get; private set;}

    public Items GetNullItem() => _noneItem;
    public List<Items> GetItemsList() => _items;
    public List<Items> GetListByRare(Rare rare) => _dictionaryItems[rare];

    public Items GetItemByID(int id)
    {
        foreach (var item in _items)
        {
            if (item.ID == id)
                return item;
        }

        Debug.LogError("Not found item");
        return _noneItem;
    }

    public void Load(bool needToMakeList = false)
    {
        IsLoaded = true;

        _items = Resources.LoadAll<Items>(PathItems).ToList();
        _noneItem = Resources.Load<Items>(NoneItem);

        _items = _items.OrderBy(item => item.Place).ToList();

        if (needToMakeList)
        {
            foreach (var item in _items)
            {
                if (_dictionaryItems.ContainsKey(item.GetRare) == false)
                {
                    _dictionaryItems[item.GetRare] = new List<Items>();
                }

                _dictionaryItems[item.GetRare].Add(item);
            }
        }
    }
}
