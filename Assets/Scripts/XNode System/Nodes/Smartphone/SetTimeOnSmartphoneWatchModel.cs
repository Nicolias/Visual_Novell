using UnityEngine;

public class SetTimeOnSmartphoneWatchModel : XnodeModel
{
    [field : SerializeField] public int Hour { get; private set; }
    [field: SerializeField] public int Minute { get; private set; }
}