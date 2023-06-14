using UnityEngine;

public class AccureEnergyModel : XnodeModel, IStorageModel
{
    [SerializeField] private int _energy;

    public int Value => _energy;

    public override void Accept(ICommanderVisitor visitor)
    {
        visitor.Visit(this);
    }
}
