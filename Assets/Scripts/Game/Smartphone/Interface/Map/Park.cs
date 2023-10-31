using Characters;
using UnityEngine;

[CreateAssetMenu(fileName = "New location", menuName = "Location/Park")]
public class Park : Location
{
    [Header("Настройки положения персонажа на локации")]
    [SerializeField] private Vector2 _characterPosition;
    [SerializeField] private Vector3 _characterScale;

    protected override CharacterOnLocationData Get(Character character)
    {
        return new CharacterOnLocationData
                (character.Type, character.Name, GetCharacterSpriteInLocation(character.Images),
                CharacterPortraitPosition.FreePosition, _characterPosition, _characterScale);
    }

    private Sprite GetCharacterSpriteInLocation(CharacterImages characterImages)
    {
        return characterImages.SittingBench;
    }
}
