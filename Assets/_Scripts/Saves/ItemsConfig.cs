using UnityEngine;
using YG;

[CreateAssetMenu(fileName = "Scrimmer", menuName = "Config/Scrimmer", order = 1)]
public class ItemsConfig : ScriptableObject
{
    private const string NotFoundLanguageError = "Language not founded";

    [SerializeField] private Sprite _icon;
    [SerializeField] private AudioClip _sound;

    public AudioClip Clip => _sound;
    public Sprite Sprite => _icon;
    public int ID => GetInstanceID();

}
