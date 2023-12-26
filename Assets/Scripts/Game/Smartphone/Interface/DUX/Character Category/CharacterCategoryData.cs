using Characters;
using UnityEngine;

[CreateAssetMenu(menuName = "Category/Character")]
public class CharacterCategoryData : DUXCategoryData
{
    [field: SerializeField] public Sprite CharacterSprite { get; private set; }
    [field: SerializeField] public CharacterType CharacterType { get; private set; }

    [SerializeField] private int _levelLimit;

    public sealed override bool IsBlocked(CharactersLibrary charactersLibrary)
    {
        ICharacter character = charactersLibrary.GetCharacter(CharacterType);

        if (character.IsMeetingWithPlayer & character.SympathyLevel >= _levelLimit)
            return false;
        else
            return true;
    }

    public sealed override void Accept(IDUXVisitor visitor)
    {
        visitor.Visit(this);
    }
}
