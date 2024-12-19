using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
[CreateAssetMenu(fileName = "ScoreThreshold", menuName = "Config/Score/ScoreThreshold", order = 1)]
public class ScoreThreshold : ScriptableObject
{
    [SerializeField] private BaseAction _onAchieved;
    
    [field: SerializeField] public int MinScore { get; private set; } 
    [field: SerializeField] public string Rus { get; private set; } 
    [field: SerializeField] public string En { get; private set; }
    
    public string GetText(string lang)
    {
        if (lang == "ru")
            return Rus;

        if (lang == "en")
            return En;

        throw new System.ArgumentNullException("Not found language");
    }
}
