using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Configs/Items", order = 1)]
public class Items : ScriptableObject
{
    private const string NotFoundLanguageError = "Language not founded";

    public Room Place;

    [SerializeField] private Sprite _icon;
    [SerializeField] private string _rusDesccription;
    [SerializeField] private string _enDesccription;

    [SerializeField] private Rare _rare = Rare.Usual;

    public Rare GetRare => _rare;
    public Sprite Icon => _icon;
    public int ID => GetInstanceID();

    public string GetDescription(string lang)
    {
        switch (lang)
        {
            case "en":
                return _enDesccription;
            case "ru":
                return _rusDesccription;
            default:
                Debug.LogError($"{NotFoundLanguageError} named {lang} in {this.name}");
                return NotFoundLanguageError;
        }
    }

}
public enum Rare
{
    Usual = 1,
    Rare = 2,
    Legendary = 4,
}

public enum Room
{
    Hallway = 1,
    Bedroom = 2,
    LivingRoom = 4,
    Bathroom = 8,
    Closet = 16,
}
