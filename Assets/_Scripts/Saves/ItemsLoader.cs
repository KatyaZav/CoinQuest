using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemsLoader
{
    private const string PathItems = "Items";

    private List<ItemsConfig> _items = new List<ItemsConfig>();

    public bool IsLoaded { get; private set;}
    public List<ItemsConfig> GetItemsList() => _items;

    public void Load(bool needToMakeList = false)
    {
        IsLoaded = true;

        _items = Resources.LoadAll<ItemsConfig>(PathItems).ToList();
    }
}


