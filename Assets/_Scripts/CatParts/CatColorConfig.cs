using UnityEngine;

[CreateAssetMenu(fileName = "ColorData", menuName = "Configs/Cat/Color", order = 1)]
public class CatColorConfig : CatConfig
{
    [field: SerializeField] public Color32 PartColor { get; private set; }
    [field: SerializeField] public float Cost { get; private set; } = 0;
}
