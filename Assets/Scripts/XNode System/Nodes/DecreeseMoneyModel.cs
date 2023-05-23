using UnityEngine;

public class DecreeseMoneyModel : XnodeModel, IStorageModel
{
    [SerializeField] private int _money;

    public int Value => _money;
}
