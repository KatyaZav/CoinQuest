using UnityEngine;

namespace Events
{
    [CreateAssetMenu(fileName = "EventData", menuName = "Configs/EventData", order = 1)]
    public class EventData : ScriptableObject
    {
        [SerializeField] private string _russianDescription;
        [SerializeField] private string _englishDescription;

        [SerializeField] private Sprite _icon;

        public Sprite GetIcon()
        {
            if (_icon == null)
                throw new System.ArgumentNullException($"Not found sprite in {name} event!");

            return _icon;
        }

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
