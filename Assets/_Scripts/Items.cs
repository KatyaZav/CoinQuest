using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Items", order = 1)]
public class Items : ScriptableObject
{
    private const string NotFoundLanguageError = "Language not founded";

    [SerializeField] private Sprite _icon;
    [SerializeField] private string _rusDesccription;
    [SerializeField] private string _enDesccription;

    [SerializeField] private Rare _rare = Rare.usual;

    public Rare GetRare => _rare;
    public Sprite Icon => _icon;
    public string GetDescription(Language lang)
    {
        switch (lang)
        {
            case Language.English:
                return _enDesccription;
            case Language.Russian:
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
