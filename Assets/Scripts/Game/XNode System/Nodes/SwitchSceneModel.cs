using UnityEngine;

public class SwitchSceneModel : XnodeModel
{
    [field: SerializeField] public int SceneNumber { get; private set; }

    public override void Accept(ICommanderVisitor visitor)
    {
        visitor.Visit(this);
    }
}