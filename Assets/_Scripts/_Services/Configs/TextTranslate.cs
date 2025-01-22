using UnityEngine;

[CreateAssetMenu(fileName = "TranslateData", menuName = "Configs/System/TranslateData", order = 1)]
public class TextTranslate : ScriptableObject
{
    [SerializeField] private string _russian, _english;

    public string GetText(string language)
    {
        switch (language)
        {
            case "ru":
                return _russian;
            case "en":
                return _english;
            default:
                throw new System.Exception($"Language {language} not found in {name}");
        }
    }
}
