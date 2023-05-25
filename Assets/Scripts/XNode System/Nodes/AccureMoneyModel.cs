using UnityEngine;

public class AccureMoneyModel : XnodeModel, IStorageModel
{
    [SerializeField] private int _money;

    public int Value => _money;

    public override void Accept(ICommanderVisitor visitor)
    {
        visitor.Visit(this);
    }
}
