using Characters;
using UnityEngine;

[CreateAssetMenu(menuName = "Category/Character/Story")]
public class CharacterStoryCategoryData : CharacterCategoryData
{
    [SerializeField] private int _levelLimit;

    public override bool IsBlocked(CharactersLibrary charactersLibrary)
    {
        Character character = charactersLibrary.GetCharacter(CharacterType);

        if (character.IsMeetingWithPlayer & character.SympathyLevel >= _levelLimit)
            return false;
        else
            return true;
    }
}
