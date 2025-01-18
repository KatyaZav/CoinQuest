using UnityEngine;

[CreateAssetMenu(fileName = "CatData", menuName = "Configs/Cat/Part", order = 1)]
public class CatPartConfig : ScriptableObject
{
    [field: SerializeField] public CatPart PartType { get; private set; }
    [field: SerializeField] public Sprite PartSprite { get; private set; }
    [field: SerializeField] public float Cost { get; private set; } = 0;
}

[CreateAssetMenu(fileName = "ColorData", menuName = "Configs/Cat/Color", order = 1)]
public class CatColorConfig : ScriptableObject
{
    [field: SerializeField] public Color32 PartColor { get; private set; }
    [field: SerializeField] public float Cost { get; private set; } = 0;
}

public enum CatPart
{
    eye = 1,
    paw = 2,
    body = 4,
    leg = 8,
}
