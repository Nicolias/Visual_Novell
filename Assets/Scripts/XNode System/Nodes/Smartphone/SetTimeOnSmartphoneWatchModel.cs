using System;
using UnityEngine;

public class SetTimeOnSmartphoneWatchModel : XnodeModel
{
    [field : SerializeField] public string Hour { get; private set; }
    [field : SerializeField] public string Minute { get; private set; }
}
