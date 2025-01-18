using UnityEngine;

public abstract class CatConfig : ScriptableObject
{
}


[CreateAssetMenu(fileName = "CatData", menuName = "Configs/Cat/Part", order = 1)]
public class CatPartConfig : CatConfig
{
    [field: SerializeField] public CatPart PartType { get; private set; }
    [field: SerializeField] public Sprite PartSprite { get; private set; }
    [field: SerializeField] public float Cost { get; private set; } = 0;
}

public enum CatPart
{
    eye = 1,
    paw = 2,
    body = 4,
    leg = 8,
}
