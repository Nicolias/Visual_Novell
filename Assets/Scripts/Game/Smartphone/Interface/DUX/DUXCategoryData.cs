using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Category/Category")]
public class DUXCategoryData : ScriptableObject
{
    [TextArea(5, 15)]
    [SerializeField] private string _discriptoin;
    [field: SerializeField] public string CategoryName { get; private set; }
    [field: SerializeField] public List<DUXCategoryData> Subcategories { get; private set; }
    public string Discription => _discriptoin;

    public virtual void Accept(IDUXVisitor visitor)
    {
    }
}
