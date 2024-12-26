using UnityEngine;

namespace Events
{
    [CreateAssetMenu(fileName = "EventData", menuName = "Data/EventData", order = 1)]
    public class EventData : ScriptableObject
    {
        [SerializeField] private string _russianDescription;
        [SerializeField] private string _englishDescription;

        public string GetDescription(string language)
        {
            switch (language)
            {
                case "ru":
                    return _russianDescription;
                case "en":
                    return _englishDescription;
                default:
                    throw new System.ArgumentException($"Language {language} not found!");
            }
        }
    }
}
