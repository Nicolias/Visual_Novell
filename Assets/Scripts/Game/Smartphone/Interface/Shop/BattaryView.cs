using UnityEngine;

public class BattaryView : StorageView
{
    [SerializeField] private Battery _battery;

    protected override IStorageView GetStorage()
    {
        return _battery;
    }
}