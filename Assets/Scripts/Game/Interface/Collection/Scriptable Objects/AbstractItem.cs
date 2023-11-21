using UnityEngine;

public abstract class AbstractItem : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
}