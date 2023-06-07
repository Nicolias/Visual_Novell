using UnityEngine;

public class DecreeseMoneyModel : XnodeModel, IStorageModel
{
    [SerializeField] private int _money;

    public int Value => _money;

    public override void Accept(ICommanderVisitor visitor)
    {
        visitor.Visit(this);
    }
}
