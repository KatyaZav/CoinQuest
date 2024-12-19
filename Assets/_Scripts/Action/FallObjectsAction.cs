using UnityEngine;

[CreateAssetMenu(fileName = "PrintMessageAction", menuName = "Config/Achievement/Actions/PrintMessage")]
public class FallObjectsAction : BaseAction
{
    [SerializeField] private Sprite _object;
    [SerializeField] private int _count = 1;

    public override void Activate()
    {
        
    }
}
