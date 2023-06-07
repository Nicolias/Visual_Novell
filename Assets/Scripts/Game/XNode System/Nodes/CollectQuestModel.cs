using System.Collections.Generic;
using UnityEngine;

public class CollectQuestModel : XnodeModel
{
    [field: SerializeField] public List<ItemForCollection> ItemForCollections { get; private set; }

    public override void Accept(ICommanderVisitor visitor)
    {
        visitor.Visit(this);
    }
}