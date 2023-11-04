using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterViewFactory : MonoBehaviour
{
    [SerializeField] private Transform _containerForFreePosition;
    [SerializeField] private Transform[] _positions;

    [SerializeField] private CharacterViewForMeeting _characterViewForMeetingPrefab;
    [SerializeField] private CharacterViwe _characterViewPrefab;

    [SerializeField] private Meeting _meeting;

    private Color[] _colors = new Color[2] { new Color(1, 1, 1, 1), new Color(1, 1, 1, 0) };

    public IEnumerable<Transform> Positions => _positions;

    public CharacterPortraitData Create(ICharacterPortraitModel character)
    {
        Image newCharacterImage;

        newCharacterImage = character.Location == null ? 
            Instantiate(character, _characterViewPrefab) : Instantiate(character, _characterViewForMeetingPrefab);

        newCharacterImage.name = character.Name;
        newCharacterImage.sprite = character.Sprite;
        newCharacterImage.color = _colors[0];

        CharacterPortraitData newCharacterPortraitData = new CharacterPortraitData(character, newCharacterImage);

        return newCharacterPortraitData;
    }

    private Image Instantiate(ICharacterPortraitModel character, ICharacterView characterPrefab)
    {
        ICharacterView newCharacterView;

        if (character.PositionType == CharacterPortraitPosition.FreePosition)
        {
            newCharacterView = Instantiate(characterPrefab.GameObject, _containerForFreePosition).GetComponent<ICharacterView>();

            RectTransform newCharacterTransform = newCharacterView.Image.rectTransform;
            newCharacterTransform.localPosition = character.PositionOffset;
            newCharacterTransform.localScale = character.ScaleOffset;
            newCharacterTransform.sizeDelta = new Vector2(character.Sprite.rect.width, character.Sprite.rect.height);
        }
        else
        {
            newCharacterView = Instantiate(characterPrefab.GameObject, _positions[(int)character.PositionType]).GetComponent<ICharacterView>();
        }

        newCharacterView.Initialize(character.CharacterType, _meeting, character.Location);

        return newCharacterView.Image;
    }
}