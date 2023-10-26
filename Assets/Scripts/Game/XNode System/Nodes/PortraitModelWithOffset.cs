using UnityEngine;

public class PortraitModelWithOffset : PortraitModel
{
    [Header(("Position Setting"))]
    [field: SerializeField] public Vector2 PositionOffset { get; private set; }
    [field: SerializeField] public Vector3 ScaleOffset { get; private set; }
}