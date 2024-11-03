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
        Item = new ItemsInfo(item);
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

public class ItemsInfo
{
    private const string NotFoundLanguageError = "Language not founded";

    private string _name;
    private Sprite _icon;
    private string _rusDesccription;
    private string _enDesccription;
    private Rare _rare = Rare.usual;

    public Rare GetRare => _rare;
    public Sprite Icon => _icon;
    public string Name => _name;

    [JsonConstructor]
    public ItemsInfo(Items item)
    {
        _name = item.Name;
        _icon = item.Icon;
        _rusDesccription = item.GetDescription("ru");
        _enDesccription = item.GetDescription("en");
        _rare = item.GetRare;
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
