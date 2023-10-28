using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CharacterPortraitView : MonoBehaviour, ICharacterPortraitView, ISaveLoadObject
{
    [Inject] private SaveLoadServise _saveLoadServise;

    [SerializeField] private Image _prefab;
    [SerializeField] private Transform[] _positions;

    private List<CharacterPortraitData> _charactersList;
    private Color[] _colors;

    private const string _saveKey = "CharacterPortrait";

    public event Action Complite;

    private void Awake()
    {
        _colors = new Color[2] { new Color(1, 1, 1, 1), new Color(1, 1, 1, 0) };
    }

    private void OnEnable()
    {
        _charactersList = new List<CharacterPortraitData>();

        if (_saveLoadServise.HasSave(_saveKey))
            Load();
    }

    private void OnDisable()
    {
        Save();
    }

    public void Show(ICharacterPortraitModel character)
    {
        if (IsCharacterExist(character.CharacterType, out CharacterPortraitData characterData))
        {
            DOTween.Sequence()
                .Append(characterData.Image.DOColor(new Color(1, 1, 1, 0), 0.2f))
                .AppendCallback(() =>
                {
                    characterData.Image.sprite = character.Sprite;
                    characterData.SetPosition(character.PositionType);
                    characterData.Image.transform.SetParent(_positions[(int)characterData.Position]);
                })
                .Append(characterData.Image.DOColor(new Color(1, 1, 1, 1), 0.2f))
                .AppendCallback(() => Complite?.Invoke())
                .Play();
        }
        else
        {
            characterData = Create(character);

            DOTween.Sequence()
                .Append(characterData.Image.DOColor(new Color(1, 1, 1, 0), 0))
                .Append(characterData.Image.DOColor(new Color(1, 1, 1, 1), 0.5f))
                .AppendCallback(() => Complite?.Invoke())
                .Play();
        }
    }

    public void Delete(ICharacterPortraitModel character)
    {
        if (IsCharacterExist(character.CharacterType, out CharacterPortraitData existCharacter))
        {
            DOTween.Sequence()
                .Append(existCharacter.Image.DOColor(new Color(1, 1, 1, 0), 0.5f))
                .AppendCallback(() => Complite?.Invoke())
                .Play();

            _charactersList.Remove(existCharacter);
            Destroy(existCharacter.Image.gameObject);
        }
    }

    private bool IsCharacterExist(CharacterType characterType, out CharacterPortraitData exist)
    {
        exist = null;

        foreach (var character in _charactersList.Where(character => character.CharacterType == characterType))
        {
            exist = character;
            return true;
        }

        return false;
    }

    private CharacterPortraitData Create(ICharacterPortraitModel character)
    {
        Image newCharacterImage; 
        
        if (character.PositionType == CharacterPortraitPosition.FreePosition)
            newCharacterImage = Instantiate(character.Sprite, character.PositionOffset, character.ScaleOffset);
        else
            newCharacterImage = Instantiate(character.PositionType);
        
        newCharacterImage.name = character.Name;
        newCharacterImage.sprite = character.Sprite;
        newCharacterImage.color = _colors[0];
        
        CharacterPortraitData newCharacterPortraitData = new CharacterPortraitData(character, newCharacterImage);
        _charactersList.Add(newCharacterPortraitData);

        return newCharacterPortraitData;
    }

    private Image Instantiate(CharacterPortraitPosition positionType)
    {
        Image newCharacterImage = Instantiate(_prefab, _positions[(int)positionType]);
        return newCharacterImage;
    }
    
    private Image Instantiate(Sprite sprite, Vector2 positionOffset, Vector3 scaleOffset)
    {
        Image newCharacterImage = Instantiate(_prefab, transform);

        RectTransform newCharacterTransform = newCharacterImage.rectTransform;
        newCharacterTransform.localPosition = positionOffset;
        newCharacterTransform.localScale = scaleOffset;
        newCharacterTransform.sizeDelta = new Vector2(sprite.rect.width, sprite.rect.height);

        return newCharacterImage;
    }

    public void Save()
    {
        _saveLoadServise.Save(_saveKey, new SaveData.IntData()
        {
            Int = _charactersList.Count
        });

        for (int i = 0; i < _charactersList.Count; i++)
        {
            var portraitData = _charactersList[i];

            _saveLoadServise.Save($"{_saveKey}/{i}", new SaveData.CharactersPortraiteData()
            {
                CharacterType = portraitData.CharacterType,
                Sprite = portraitData.Image.sprite,
                Position = portraitData.Position,
                Name = portraitData.Name,
                PositionOffset = portraitData.PositionOffset,
                ScaleOffset = portraitData.ScalseOffset
            });
        }
    }

    public void Load()
    {
        int characterCount = _saveLoadServise.Load<SaveData.IntData>(_saveKey).Int;

        for (int i = 0; i < characterCount; i++)
        {
            var character = _saveLoadServise.Load<SaveData.CharactersPortraiteData>($"{_saveKey}/{i}");

            CharacterModel characterModel = new CharacterModel()
                {
                    CharacterType = character.CharacterType,
                    Name = character.Name,
                    Sprite = character.Sprite,
                    PositionType = character.Position,
                    PositionOffset = character.PositionOffset,
                    ScaleOffset = character.ScaleOffset
                };

            Show(characterModel);
        }
    }

    private class CharacterModel : ICharacterPortraitModel
    {
        public CharacterType CharacterType { get; set; }
        public string Name { get; set; }
        public Sprite Sprite { get; set; }
        public CharacterPortraitPosition PositionType { get; set; }

        public Vector2 PositionOffset { get; set; }

        public Vector3 ScaleOffset { get; set; }
    }
}
