using UnityEngine;

public class AccureMoneyModel : XnodeModel, IStorageModel
{
    [SerializeField] private int _money;

    public int Value => _money;
}
