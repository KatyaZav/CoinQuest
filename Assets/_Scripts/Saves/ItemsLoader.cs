using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemsLoader
{
    private const string PathItems = "Items";

    private List<Items> _items = new List<Items>();

    public bool IsLoaded { get; private set;}
    public List<Items> GetItemsList() => _items;

    public void Load(bool needToMakeList = false)
    {
        IsLoaded = true;

        _items = Resources.LoadAll<Items>(PathItems).ToList();
    }
}
