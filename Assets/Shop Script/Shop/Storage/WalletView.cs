using UnityEngine;

public class WalletView : StorageView
{
    [SerializeField] private Wallet _wallet;

    protected override IStorageView GetStorage()
    {
        return _wallet;
    }
}