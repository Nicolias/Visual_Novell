using UnityEngine;

public class AddSympathyModel : XnodeModel
{
    [field: SerializeField] public int Points { get; private set; }
    [field: SerializeField] public CharacterType Character { get; private set; }
}