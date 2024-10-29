using UnityEngine;
using YG;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Items", order = 1)]
public class Items : ScriptableObject
{
    private const string NotFoundLanguageError = "Language not founded";

    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _rusDesccription;
    [SerializeField] private string _enDesccription;

    [SerializeField] private Rare _rare = Rare.usual;

    public Rare GetRare => _rare;
    public Sprite Icon => _icon;
    public string Name => _name;

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

    public enum Rare
    {
        usual = 1,
        normal = 2,
        rare = 4,
        legendary = 8
    }

    public enum Language
    {
        Russian,
        English
    }
}
