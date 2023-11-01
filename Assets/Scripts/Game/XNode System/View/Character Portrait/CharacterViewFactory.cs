using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CharacterViewFactory : MonoBehaviour
{
    [SerializeField] private Transform _containerForFreePosition;
    [SerializeField] private CharacterView _prefab;
    [SerializeField] private Transform[] _positions;

    public IEnumerable<Transform> Positions => _positions;

    private Quiz _quiz;

    private Color[] _colors = new Color[2] { new Color(1, 1, 1, 1), new Color(1, 1, 1, 0) };

    [Inject]
    public void Construct(Quiz quiz)
    {
        _quiz = quiz;
    }

    public CharacterPortraitData Create(ICharacterPortraitModel character)
    {
        Image newCharacterImage = Instantiate(character);

        newCharacterImage.name = character.Name;
        newCharacterImage.sprite = character.Sprite;
        newCharacterImage.color = _colors[0];

        CharacterPortraitData newCharacterPortraitData = new CharacterPortraitData(character, newCharacterImage);

        return newCharacterPortraitData;
    }

    private Image Instantiate(ICharacterPortraitModel character)
    {
        CharacterView newCharacterView;

        if (character.PositionType == CharacterPortraitPosition.FreePosition)
        {
            newCharacterView = Instantiate(_prefab, _containerForFreePosition);

            RectTransform newCharacterTransform = newCharacterView.Image.rectTransform;
            newCharacterTransform.localPosition = character.PositionOffset;
            newCharacterTransform.localScale = character.ScaleOffset;
            newCharacterTransform.sizeDelta = new Vector2(character.Sprite.rect.width, character.Sprite.rect.height);
        }
        else
        {
            newCharacterView = Instantiate(_prefab, _positions[(int)character.PositionType]);
        }

        newCharacterView.Initialize(character.CharacterType, _quiz);

        return newCharacterView.Image;
    }
}