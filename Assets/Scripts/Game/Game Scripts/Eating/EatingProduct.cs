using System;
using UnityEngine;

[Serializable]
public class EatingProduct : IDataForCell
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public int Price { get; private set; }
    [field: SerializeField] public int SympathyPointsBonus { get; private set; }

    public override string ToString()
    {
        return $"{Name} - {Price}M + {SympathyPointsBonus}ед";
    }
}