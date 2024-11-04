using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Scripting;

public class ItemsData
{
    public ItemsInfo Item;

    private bool _isGetted;
    private bool _isSaw;
 
    public ItemsData(Items item)
    {
        Item = new ItemsInfo(item.ID, item.Icon, item.GetDescription("ru"), item.GetDescription("en"), item.GetRare);
        _isGetted = false;
        _isSaw = false;
    }

    [JsonConstructor]
    public ItemsData(ItemsInfo item)
    {
        Item = item;
        _isGetted = false;
        _isSaw = false;
    }

    public bool IsSaw => _isSaw;
    public bool IsGetted => _isGetted;

    public void Get()
    {
        _isGetted = true;
    } 

    public void See()
    {
        _isSaw = true;
    }
}

[System.Serializable]
public class ItemsInfo
{
    private const string NotFoundLanguageError = "Language not founded";

    [Newtonsoft.Json.JsonIgnore]
    private Sprite _icon;

    private int _id;
    private string _rusDesccription;
    private string _enDesccription;
    private Rare _rare = Rare.usual;

    public Rare GetRare => _rare;
    public int ID => _id;

    [Newtonsoft.Json.JsonIgnore]
    public Sprite Icon => _icon;


    [JsonConstructor]
    public ItemsInfo(int id, Sprite icon, string rusDesccription, string enDesccription, Rare rare)
    {
        _id = id;
        _icon = icon;
        _rusDesccription = rusDesccription;
        _enDesccription = enDesccription;
        _rare = rare;
    }

    public string GetDescription(string lang)
    {
        switch (lang)
        {
            case "en":
                return _enDesccription;
            case "ru":
                return _rusDesccription;
            default:
                Debug.LogError(NotFoundLanguageError);
                return NotFoundLanguageError;
        }
    }
}
