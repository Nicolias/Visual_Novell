using UnityEngine;

[CreateAssetMenu(fileName = "Superlocation", menuName = "Location/Superlocation")]
public class Superlocation : ScriptableObject, IDataForCell
{
    [field: SerializeField] public string Name { get; private set; }

    public override string ToString()
    {
        return Name;
    }
}