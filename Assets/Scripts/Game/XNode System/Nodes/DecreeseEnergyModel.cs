using UnityEngine;

public class DecreeseEnergyModel : XnodeModel, IStorageModel
{
    [SerializeField] private int _money;

    public int Value => _money;

    public override void Accept(ICommanderVisitor visitor)
    {
        visitor.Visit(this);
    }
}